
using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.BaseRepository
{
    public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public Task Create<VM>(VM VMModel)
        {
            throw new NotImplementedException();
        }
        public Task<T> Update<VM>(VM VMModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            bool isDeleted = false;
            var row = await _context.Set<T>().FindAsync(id);

            if (row is null) return isDeleted;

            _context.Set<T>().Remove(row);

            int effectedRows = await _context.SaveChangesAsync();
            if (effectedRows > 0)
            {
                isDeleted = true;
                //DeleteCover(game.Cover);
            }

            return isDeleted;
        }
        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
