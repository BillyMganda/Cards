namespace Cards.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public UserRole UserRole { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
