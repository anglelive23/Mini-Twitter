namespace Mini_Twitter.Domain.Entities
{
    public class Module : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
