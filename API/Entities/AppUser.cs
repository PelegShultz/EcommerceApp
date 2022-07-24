using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    [Index(nameof(UserName), IsUnique = true)]
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public byte[]? passwordHash { get; set; }
        public byte[]? passwordSalt { get; set; }
    }
}
