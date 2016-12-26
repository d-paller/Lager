using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lager.Models
{
    public class Part : IPart
    {
        /// <summary>
        /// Name of the part
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the part
        /// </summary>
        public int PartId { get; set; }

        /// <summary>
        /// If the part is active or not.  Default is true
        /// </summary>

        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The date that the part was added
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// A short description of the part
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public string PurchaseUrl { get; set; }

    }
}
