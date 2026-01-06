namespace Mini_Twitter.Domain.Entities
{
    public class RoleAccessModule
    {
        public int Id { get; set; }
        public IdentityRole Role { get; set; }
        public string RoleId { get; set; }
        public Module Module { get; set; }
        public int ModuleId { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
