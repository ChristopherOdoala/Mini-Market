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
        ResultModel<string> UpdateItem(ItemViewModel model);
        ResultModel<string> CreateItem(CreateItemViewModel model);
        ResultModel<string> PurchaseItem(Guid itemId);
    }
    public class ItemService : IItemService
    {
        private readonly DataContext _dataContext;
        private readonly IItemPurchaseHistoryService _history;

        public ItemService(DataContext dataContext, IItemPurchaseHistoryService history)
        {
            _dataContext = dataContext;
            _history = history;
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

        public ResultModel<string> UpdateItem(ItemViewModel model)
        {
            var resultModel = new ResultModel<string>();
            if(model.Id == Guid.Empty)
            {
                resultModel.AddError("Item Id cannot be null");
                return resultModel;
            }
            try
            {
                _dataContext.Items.Update(model);
                _dataContext.SaveChanges();

                var history = new ItemPurchaseHistory
                {
                    Item = model,
                };

                _history.UpdateItemHistory(history);
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message + " \b" + ex.InnerException == null ? "" : ex.InnerException.Message);
                return resultModel;
            }
            resultModel.Data = "Item Updated successfully";
            return resultModel;
        }

        public ResultModel<string> CreateItem(CreateItemViewModel model)
        {
            var resultModel = new ResultModel<string>();
            if (model is null)
            {
                resultModel.AddError("Model cannot be null");
                return resultModel;
            }
            if (model.NoOfAvailableItems == 0)
            {
                resultModel.AddError("No Of Available Items cannot be Zero");
                return resultModel;
            }
            if (model.Category is null)
            {
                resultModel.AddError("Category Model cannot be null");
                return resultModel;
            }

            try
            {
                _dataContext.Items.Add(model);
                _dataContext.SaveChanges();

                var history = new ItemPurchaseHistory
                {
                    Item = model,
                    Count = model.NoOfAvailableItems,
                };

                _history.CreateItemHistory(history);
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message + " \b" + ex.InnerException == null ? "" : ex.InnerException.Message);
                return resultModel;
            }
            resultModel.Data = "Created Successfully";
            return resultModel;
        }

        public ResultModel<string> PurchaseItem(Guid itemId)
        {
            var resultModel = new ResultModel<string>();
            if(itemId == Guid.Empty)
            {
                resultModel.AddError("Item Id cannot be null");
                return resultModel;
            }
            var res = GetById(itemId);
            if(res.Data.NoOfAvailableItems < 1)
            {
                resultModel.AddError("Item is out of Stock");
                return resultModel;
            }

            res.Data.NoOfAvailableItems -= 1;

            try
            {
                _dataContext.Items.Update(res.Data);
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message + " \b" + ex.InnerException == null ? "" : ex.InnerException.Message);
                return resultModel;
            }
            resultModel.Data = "Item Purchased Successfully";
            return resultModel;
        }
    }
}
