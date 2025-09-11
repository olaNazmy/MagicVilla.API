// IVillaRepository.cs
using MagicVilla.API.Models;
using System;

namespace MagicVilla.API.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}

