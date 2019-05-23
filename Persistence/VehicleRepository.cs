using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspAng.Core;
using AspAng.Core.Models;
using AspAng.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AspAng.Persistence {
    public class VehicleRepository : IVehicleRepository {
        private AspAngDbContext _context;

        public VehicleRepository (AspAngDbContext context) {
            _context = context;
        }
        public async Task<Vehicle> GetVehicle (int id, bool includeRelated = true) {

            if (!includeRelated)
                return await _context.Vehicles.FindAsync (id);

            return await _context.Vehicles
                .Include (v => v.Features)
                .ThenInclude (vf => vf.Feature)
                .Include (v => v.Model)
                .ThenInclude (m => m.Make.Models)
                .SingleOrDefaultAsync (v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles (VehicleQuery queryObj) {

            var query = _context.Vehicles
                .Include (v => v.Model)
                .ThenInclude (vm => vm.Make)
                .Include (v => v.Features)
                .ThenInclude (f => f.Feature)
                .AsQueryable ();

            if (queryObj.MakeId.HasValue)
                query = query.Where (v => v.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where (v => v.ModelId == queryObj.ModelId.Value);

            var ColumnsMap = new Dictionary<string, Expression<Func<Vehicle, object>> > () {
                    ["make"] = v => v.Model.Make.Name, ["model"] = v => v.Model.Name, ["contactName"] = v => v.ContactName, ["id"] = v => v.Id
                };

            query = query.ApplySorting(queryObj, ColumnsMap);
            
            return await query.ToListAsync ();
        }

       

        public void Add (Vehicle vehicle) {
            _context.Vehicles.Add (vehicle);
        }

        public void Remove (Vehicle vehicle) {
            _context.Remove (vehicle);
        }
    }
}

//  if(queryObj.SortBy == "make")
//                 query = queryObj.IsSortAscending ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);

//                 if(queryObj.SortBy == "model")
//                 query = queryObj.IsSortAscending ? query.OrderBy(v => v.Model.Name) : query.OrderByDescending(v => v.Model.Name);

//                 if(queryObj.SortBy == "contactName")
//                 query = queryObj.IsSortAscending ? query.OrderBy(v => v.ContactName) : query.OrderByDescending(v => v.ContactName);

//                 if(queryObj.SortBy == "id")
//                 query = queryObj.IsSortAscending ? query.OrderBy(v => v.Id) : query.OrderByDescending(v => v.Id);

//                 switch(queryObj.SortBy){
//                     case "make":
//                       break;
//                        case "model":
//                       break;

//                         case "contactName":
//                       break;
//                        case "id":
//                       break;                      
//                  }