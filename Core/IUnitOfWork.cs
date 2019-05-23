using System.Threading.Tasks;

namespace AspAng.Core {

    
    public interface IUnitOfWork {
        Task CompleteAsync ();
    }
}