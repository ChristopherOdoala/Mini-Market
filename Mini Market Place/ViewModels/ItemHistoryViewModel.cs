using Mini_Market_Place.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.ViewModels
{
    public class ItemHistoryViewModel
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public ItemViewModel Item { get; set; }
        public int Count { get; set; }

        public static implicit operator ItemHistoryViewModel(ItemPurchaseHistory model)
        {
            return model == null ? null : new ItemHistoryViewModel
            {
                Count = model.Count,
                Item = model.Item,
                Id = model.Id
            };
        }
    }
}
