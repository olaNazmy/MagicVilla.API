// IVillaRepository.cs
using MagicVilla.API.Models;
using System;

namespace MagicVilla.API.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}

