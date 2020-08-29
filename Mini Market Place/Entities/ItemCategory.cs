using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Entities
{
    public class ItemCategory
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
