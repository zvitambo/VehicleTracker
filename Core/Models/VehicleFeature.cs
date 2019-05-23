using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspAng.Core.Models;

namespace AspAng.Core.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }

        public int FeatureId { get; set; }

        public Vehicle Vehicle { get; set; }

        public Feature  Feature { get; set; }
    }
}