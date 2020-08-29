using Mini_Market_Place.Entities;
using Mini_Market_Place.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.ViewModels
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public static implicit operator ItemViewModel(Item model)
        {
            return model == null ? null : new ItemViewModel
            {
                Category = model.Category.Category,
                Name = model.Name,
                Price = model.Price.FormatAmount(),
                Description = model.Description,
                Id = model.Id
            };
        }

    }
}
