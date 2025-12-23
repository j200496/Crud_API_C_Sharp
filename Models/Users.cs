using System.ComponentModel.DataAnnotations;

namespace Crud_Api.Models
{
    public class Users
    {
        [Key]   
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
