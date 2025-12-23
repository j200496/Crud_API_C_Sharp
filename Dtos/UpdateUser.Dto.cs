namespace Crud_Api.Dtos
{
    public class UpdateUser
    {
        public string UserName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
