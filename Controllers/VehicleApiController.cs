using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspAng.Controllers.Resources;
using AspAng.Persistence;
using AspAng.Core;
using AspAng.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspAng.Controllers {
    [Route ("/api/vehicle/")]
    public class VehicleApiController : Controller {
        private AspAngDbContext _context;
        private readonly IVehicleRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public VehicleApiController (AspAngDbContext context, IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
            this._context = context;

        }

        [HttpGet ("makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes () {

            var makes = await _context.Makes.Include (m => m.Models).ToListAsync ();

            return mapper.Map<List<Make>, List<MakeResource>> (makes);

        }

        [HttpGet ("models")]
        public async Task<IEnumerable<KeyValuePairResource>> GetModels () {

            var models = await _context.Models.ToListAsync ();

            return mapper.Map<List<Model>, List<KeyValuePairResource>> (models);

        }

        [HttpGet ("features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures () {

            var features = await _context.Features.ToListAsync ();

            return mapper.Map<List<Feature>, List<KeyValuePairResource>> (features);

        }

        [HttpPost ("createVehicle")]
        public async Task<IActionResult> CreateVehicle ([FromBody] SaveVehicleResource vehicleResource) {

         // throw new Exception();

            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            var model = await _context.Models.FindAsync (vehicleResource.ModelId);
            if (model == null) {
                ModelState.AddModelError ("Modeld", "Invalid Model Id");
                return BadRequest (ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle> (vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add (vehicle);
            await unitOfWork.CompleteAsync ();
            vehicle = await repository.GetVehicle (vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource> (vehicle);
            return Ok (vehicle);
        }

        [HttpPut ("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle (int id, [FromBody] SaveVehicleResource vehicleResource) {

            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            //  var model = await _context.Vehicles.Include (v => v.Features).SingleOrDefaultAsync (v => v.Id == id);

            var vehicle = await repository.GetVehicle (id);

            // if (model == null) {
            //     ModelState.AddModelError ("Modeld", "Invalid Model Id");
            //     return BadRequest (ModelState);
            // }

            vehicle = await _context.Vehicles.FindAsync (id);

            if (vehicle == null)
                return NotFound ();
            vehicle = await repository.GetVehicle (vehicle.Id);
            mapper.Map<SaveVehicleResource, Vehicle> (vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync ();

            var result = mapper.Map<Vehicle, SaveVehicleResource> (vehicle);
            return Ok (vehicle);
        }

        [HttpDelete ("DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle (int id) {

            var vehicle = await repository.GetVehicle (id, includeRelated : false);

            if (vehicle == null)
                return NotFound ();

            repository.Remove (vehicle);
            await unitOfWork.CompleteAsync ();

            return Ok (id);

        }

        [HttpGet ("GetVehicle/{id}")]
        public async Task<IActionResult> GetVehicle (int id)
         {

            var vehicle = await repository.GetVehicle (id);

            if (vehicle == null)
                return NotFound ();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource> (vehicle);

            return Ok (vehicleResource);

        }
        [HttpGet("GetVehicles")]
        public async Task<IEnumerable<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource , VehicleQuery>(filterResource);
           var vehicles = await repository.GetVehicles(filter);

           return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);

        }

    }

}