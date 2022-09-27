using DialInApi.Models;

namespace DialInApi.Data
{
    public interface IDialInRepository
    {
        Task SaveChangesAsync();
        Task<DialIn?> GetDialInByIdAsync(string dialInId);
        Task<IEnumerable<DialIn>> GetAllDialInsAsync();
        Task CreateDialInAsync(DialIn dialIn);
        // Update?
        void DeleteDialInAsync(DialIn dialIn);
    }
}