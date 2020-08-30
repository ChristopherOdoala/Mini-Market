using Mini_Market_Place.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.ViewModels
{
    public class ItemCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public static implicit operator ItemCategoryViewModel(ItemCategory model)
        {
            return model == null ? null : new ItemCategoryViewModel
            {
                Id = model.Id,
                Category = model.Category,
                Description = model.Description
            };
        }
    }
}
