namespace Cards.DTOs
{
    public class CardSearchResponse
    {
        public List<GetCardResponse> Cards { get; set; } = new List<GetCardResponse>();
        public int TotalCount { get; set; }
    }
}
