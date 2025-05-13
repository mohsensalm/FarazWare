using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarazWare.Application.Contracts.Services;
using FarazWare.Domain.Dto;

namespace FarazWare.Application.UseCases
{
    public class GetCardsUseCase
    {
        private readonly ICardClient _cards;

        public GetCardsUseCase(ICardClient cards)
        {
            _cards = cards;
        }

        public async Task<IEnumerable<CardDto>> ExecuteAsync(string token)
        {
            var listDto = await _cards.GetCardsAsync(token);
            return listDto.Cards;
        }
    }
}
