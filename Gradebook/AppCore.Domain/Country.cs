﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Domain
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
