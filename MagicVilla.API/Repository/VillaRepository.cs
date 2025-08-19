using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla.API.Repository
{
    public class VillaRepository : IVillaRepository
    {
        // use context here
        private readonly ApplicationDbContext db;
        public VillaRepository(ApplicationDbContext _db)
        {
            db= _db;
        }
        public async Task Create(Villa entity)
        {
            await db.AddAsync(entity);
            await Save();
        }

        public async Task<Villa> Get(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = db.Villas;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAll(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = db.Villas;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            return await query.ToListAsync();
        }

        public async Task Remove(Villa entity)
        {
            db.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
