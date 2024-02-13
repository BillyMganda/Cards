using Cards.Models;

namespace Cards.DTOs
{
    public class SingleCardResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public CardStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
