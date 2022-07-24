using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMessageSession _messageSession;
        public AccountController(DataContext context, ITokenService tokenService, IMessageSession messageSession)
        {
            _tokenService = tokenService;
            _context = context;
            _messageSession = messageSession;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.userName)) return BadRequest("User already exists");
            using var hmac = new HMACSHA512();//This provide us our hasing algorithm

            var user = new AppUser
            {
                UserName = registerDto.userName.ToLower(), 
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                passwordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            UserDto userDto = new UserDto
            {
                userName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

            var command = new UserCreated
            {
                UserName = userDto.userName,
                UserId = user.Id
            };

            await _messageSession.Send(command)
               .ConfigureAwait(false);

            return userDto;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.userName);

            if(user == null)
            {
                return Unauthorized("Invalid username");
            }

            using var hmac = new HMACSHA512(user.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.passwordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }

            return new UserDto
            {
                userName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }


    }
}
