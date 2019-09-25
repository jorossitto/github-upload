using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace ACM.BL
{
    public partial class Restaurant : EntityBase
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public CuisineType Cuisine { get; set; }

        public override bool Validate()
        {
            return true;
        }
    }


}
