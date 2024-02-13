using Cards.DTOs;
using Cards.Models;
using Cards.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Cards.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var card = await _cardService.CreateCardAsync(userId, request);
                return Ok(card);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("cardId")]
        public async Task<IActionResult> GetCard([FromQuery, Required] Guid cardId)
        {
            try
            {
                if(cardId == Guid.Empty)
                {
                    return BadRequest("Invalid cardId");
                }

                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userRole = (UserRole)Enum.Parse(typeof(UserRole), User.FindFirst(ClaimTypes.Role)?.Value);

                var card = await _cardService.GetCardByIdAsync(userId, cardId, userRole);

                if (card == null)
                {
                    return NotFound("Card not found");
                }

                if (card.UserId != userId)
                {
                    return Unauthorized("You are not authorized to access this card.");
                }

                return Ok(card);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCardsAsync()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userRole = (UserRole)Enum.Parse(typeof(UserRole), User.FindFirst(ClaimTypes.Role)?.Value);

                var cards = await _cardService.GetCardsAsync(userId, userRole);

                return Ok(cards);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }      
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCards([FromQuery] CardSearchRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userRole = (UserRole)Enum.Parse(typeof(UserRole), User.FindFirst(ClaimTypes.Role)?.Value);

                var cards = await _cardService.SearchCardsAsync(userId, request, userRole);

                return Ok(cards);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{cardId}")]
        public async Task<IActionResult> UpdateCard(Guid cardId, [FromBody] UpdateCardRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var card = await _cardService.UpdateCardAsync(userId, cardId, request);

                if(card == null)
                {
                    return NotFound("Card not found");
                }

                return Ok(card);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{cardId}/status")]
        public async Task<IActionResult> UpdateCardStatus(Guid cardId, [FromBody] UpdateCardStatusRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var card = await _cardService.UpdateCardStatusAsync(userId, cardId, request);

                if (card == null)
                {
                    return NotFound("Card not found");
                }

                return Ok(card);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(Guid cardId)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var result = await _cardService.DeleteCardAsync(userId, cardId);

                if(result == false)
                {
                    return NotFound("Card not found");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
