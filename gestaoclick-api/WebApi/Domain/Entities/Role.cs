namespace Domain.Entities
{
    public class Role
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public IList<Usuario> Usuarios { get; set; }
    }
}