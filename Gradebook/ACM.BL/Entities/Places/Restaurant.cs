using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Common;

namespace ACM.BL
{
    public partial class Restaurant : EntityBase
    {

        public int ID { get; set; }
        [Required, StringLength(80)]
        public string Name { get; set; }
        [Required]
        public Address Address { get; set; }
        public CuisineType Cuisine { get; set; }

        public Restaurant()
        {
            this.Address = new Address();
        }
        public override bool Validate()
        {
            return true;
        }
    }


}
