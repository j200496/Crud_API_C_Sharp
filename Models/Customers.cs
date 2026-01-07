using System.ComponentModel.DataAnnotations;

namespace Crud_Api.Models
{
    public class Customers
    {
        [Key]
        public int IdCustomer { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public char Gender { get; set; }
        public int Age {  get; set; }
        public bool IsActive { get; set; }
    }
}
