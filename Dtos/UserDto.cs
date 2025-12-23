namespace Crud_Api.Dtos
{
    public class UserDto
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
