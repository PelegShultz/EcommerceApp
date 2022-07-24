using System.ComponentModel.DataAnnotations;

namespace BalanceService.Entities
{
    public class BalanceEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int Balance { get; set; }

    }
}
