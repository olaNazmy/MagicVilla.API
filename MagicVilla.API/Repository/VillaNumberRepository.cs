// VillaRepository.cs
using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db) : base(db)  
        {
            _db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            // If the entity isn’t tracked, attach it and mark as modified
            _db.VillaNumbers.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
