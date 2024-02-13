using System.Text.Json.Serialization;

namespace Cards.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public CardStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign key to associate the card with its creator
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
