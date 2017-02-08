using Lager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lager.Models
{
    public class Part
    {
        /// <summary>
        /// uniquie id that mongo give to each document
        /// </summary>
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        /// <summary>
        /// Name of the part
        /// </summary>
        
        [BsonElement("Name")]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [BsonElement("Category")]
        [Required]
        public string Category { get; set; }
        /// <summary>
        /// The ID of the part
        /// </summary>
        [BsonElement("PartID")]
        public int PartId { get; set; }

        /// <summary>
        /// If the part is active or not.  Default is true
        /// </summary>
        [BsonElement("IsActive")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The date that the part was added
        /// </summary>
        [BsonElement("DateAdded")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        /// <summary>
        /// cost of the part
        /// </summary>
        [Required]
        [BsonElement("Cost")]
        public double Cost { get; set; }

        /// <summary>
        /// name of the vendor
        /// </summary>
        [Required]
        [BsonElement("Vendor")]
        public String Vendor { get; set; }


        [BsonElement("Holder")]
        public string Holder { get; set; }

        /// <summary>
        /// A short description of the part
        /// </summary>
        [Required]
        [StringLength(100)]
        [BsonElement("Description")]
        public string Description { get; set; }

        [Url]
        [Required]
        [BsonElement("PurchaseUrl")]
        public string PurchaseUrl { get; set; }



    }
}
