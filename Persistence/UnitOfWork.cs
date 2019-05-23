using System.Threading.Tasks;
using AspAng.Core;

namespace AspAng.Persistence
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly AspAngDbContext _context;
        public UnitOfWork (AspAngDbContext context) {

            this._context = context;

        }
        public async Task CompleteAsync () {
            
            await _context.SaveChangesAsync();
        }

        
    }
}