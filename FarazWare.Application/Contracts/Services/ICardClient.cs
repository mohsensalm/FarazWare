using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarazWare.Domain.Dto;

namespace FarazWare.Application.Contracts.Services
{
    public interface ICardClient
    {
        Task<CardListDto> GetCardsAsync(string token, CancellationToken ct = default);

    }
}
