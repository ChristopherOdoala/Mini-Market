using Mini_Market_Place.Entities;
using Mini_Market_Place.Helper;
using Mini_Market_Place.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Services
{
    public interface IItemService
    {
        ResultModel<List<ItemViewModel>> GetAllItems();
        ResultModel<ItemViewModel> GetById(Guid Id);
        void DeleteItem(Item item);
    }
    public class ItemService : IItemService
    {
        private readonly DataContext _dataContext;

        public ItemService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultModel<List<ItemViewModel>> GetAllItems()
        {
            var resultModel = new ResultModel<List<ItemViewModel>>();
            resultModel.Data = _dataContext.Items.Select(x => (ItemViewModel)x).ToList();
            return resultModel;
        }

        public ResultModel<ItemViewModel> GetById(Guid Id)
        {
            var resultModel = new ResultModel<ItemViewModel>();
            resultModel.Data = _dataContext.Items.Where(x => x.Id == Id).FirstOrDefault();
            return resultModel;
        }

        public void DeleteItem(Item item)
        {
            _dataContext.Items.Remove(item);
        }
    }
}
