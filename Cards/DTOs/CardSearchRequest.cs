using Cards.Models;

namespace Cards.DTOs
{
    public class CardSearchRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public CardStatus? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string SortBy { get; set; } = string.Empty;
    }
}
