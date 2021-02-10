namespace Asga.Auth.Dto
{
    public class UsersFilterModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public LoadOpts LoadOpts { get; set; }

    }
    public class LoadOpts
    {
        public int Skip { get; set; }
        public int Showing { get; set; }
    }

    public class UserListModel
    {
        public long Id { get; set; }
        public string LogonName { get; set; }
        public string email { get; set; }
        public string Role { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
