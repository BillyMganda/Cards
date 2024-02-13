using Cards.DTOs;
using Cards.Models;

namespace Cards.Repositories
{
    public interface ICardService
    {
        Task<Card> CreateCardAsync(Guid userId, CreateCardRequest request);
        Task<Card?> GetCardByIdAsync(Guid userId, Guid cardId, UserRole userRole);
        Task<List<Card>> GetCardsAsync(Guid userId, UserRole userRole);
        Task<List<Card>> SearchCardsAsync(Guid userId, CardSearchRequest request, UserRole userRole);
        Task<Card?> UpdateCardAsync(Guid userId, Guid cardId, UpdateCardRequest request);
        Task<Card?> UpdateCardStatusAsync(Guid userId, Guid cardId, UpdateCardStatusRequest request);
        Task<bool> DeleteCardAsync(Guid userId, Guid cardId);
    }
}
