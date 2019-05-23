using System.Threading.Tasks;
using System.Collections.Generic;
using AspAng.Core;
using AspAng.Core.Models;

namespace AspAng.Core
{
    public interface IVehicleRepository
    {
      Task<Vehicle> GetVehicle(int id, bool includeRelated = true);  

      Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery filter);

      void Add(Vehicle vehicle);

      void Remove(Vehicle vehicle);
    } 
    
}