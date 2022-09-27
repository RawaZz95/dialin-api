using DialInApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DialInApi.Data
{
    public class SqlDialInRepository : IDialInRepository
    {
        private readonly DialInDbContext _context;

        public SqlDialInRepository(DialInDbContext context)
        {
            _context = context;
        }

        public async Task CreateDialInAsync(DialIn dialIn)
        {
            if (dialIn == null) 
            {
                throw new ArgumentNullException();
            }

            await _context.DialIns.AddAsync(dialIn);
        }

        public void DeleteDialInAsync(DialIn dialIn)
        {
            if (dialIn == null) 
            {
                throw new ArgumentNullException();
            }

            _context.DialIns.Remove(dialIn);
        }

        public async Task<IEnumerable<DialIn>> GetAllDialInsAsync()
        {
            return await _context.DialIns.ToListAsync();
        }

        public async Task<DialIn?> GetDialInByIdAsync(string dialInId)
        {
            return await _context.DialIns.FirstOrDefaultAsync(di => di.DialInId == dialInId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}