using Cards.Data;
using Cards.DTOs;
using Cards.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.Repositories
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;
        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Card> CreateCardAsync(Guid userId, CreateCardRequest request)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found");

            var newCard = new Card
            {
                Name = request.Name,
                Description = request.Description,
                Color = request.Color,
                Status = CardStatus.ToDo,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.Cards.Add(newCard);
            await _context.SaveChangesAsync();

            return newCard;
        }

        public async Task<Card?> GetCardByIdAsync(Guid userId, Guid cardId, UserRole userRole)
        {
            if(userRole == UserRole.Admin)
            {
                var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == userId);

                if (card == null)
                {
                    return null;
                }

                return card;
            }
            else
            {
                var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);

                if (card == null)
                {
                    return null;
                }

                return card;
            }            
        }

        public async Task<List<Card>> GetCardsAsync(Guid userId, UserRole userRole)
        {
            if(userRole == UserRole.Admin)
            {
                var cards = await _context.Cards.ToListAsync();
                return cards;
            }
            else
            {
                var cards = await _context.Cards
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                return cards;
            }            
        }

        public async Task<List<Card>> SearchCardsAsync(Guid userId, CardSearchRequest request, UserRole userRole)
        {
            IQueryable<Card> query;

            if (userRole == UserRole.Admin)
            {
                query = _context.Cards.AsQueryable();
            }
            else
            {
                query = _context.Cards.Where(c => c.UserId == userId);
            }


            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(c => c.Name.Contains(request.Name));

            if (!string.IsNullOrWhiteSpace(request.Color))
                query = query.Where(c => c.Color == request.Color);

            if (request.Status != null)
                query = query.Where(c => c.Status == request.Status);

            if (request.CreatedAt != null)
                query = query.Where(c => c.CreatedAt.Date == request.CreatedAt.Value.Date);

            if (request.SortBy == "name")
                query = query.OrderBy(c => c.Name);
            else if (request.SortBy == "color")
                query = query.OrderBy(c => c.Color);
            else if (request.SortBy == "status")
                query = query.OrderBy(c => c.Status);
            else if (request.SortBy == "createdAt")
                query = query.OrderBy(c => c.CreatedAt);
            else
                query = query.OrderBy(c => c.CreatedAt); // Default sorting by createdAt

            if (request.Page != null && request.Size != null)
                query = query.Skip((request.Page.Value - 1) * request.Size.Value).Take(request.Size.Value);
            else if (request.Offset != null && request.Limit != null)
                query = query.Skip(request.Offset.Value).Take(request.Limit.Value);

            return await query.ToListAsync();
        }

        public async Task<Card?> UpdateCardAsync(Guid userId, Guid cardId, UpdateCardRequest request)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);

            if (card == null)
                return null;

            if (!string.IsNullOrWhiteSpace(request.Name))
                card.Name = request.Name;

            if (request.Description != null)
                card.Description = request.Description;

            if (!string.IsNullOrWhiteSpace(request.Color))
                card.Color = request.Color;

            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<Card?> UpdateCardStatusAsync(Guid userId, Guid cardId, UpdateCardStatusRequest request)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);

            if (card == null)
                return null;

            card.Status = request.Status;

            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<bool> DeleteCardAsync(Guid userId, Guid cardId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId && c.UserId == userId);

            if (card == null)
                return false;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
