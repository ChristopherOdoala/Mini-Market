using Mini_Market_Place.Entities;
using Mini_Market_Place.Helper;
using Mini_Market_Place.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Services
{
    public interface IItemPurchaseHistoryService
    {
        ResultModel<List<ItemHistoryViewModel>> GetAllItems();
        ResultModel<ItemHistoryViewModel> GetById(Guid Id);
        void DeleteItem(ItemPurchaseHistory item);
        ResultModel<string> CreateItemHistory(ItemPurchaseHistory model);
        ResultModel<string> UpdateItemHistory(ItemPurchaseHistory model);
    }
    public class ItemPurchaseHistoryService : IItemPurchaseHistoryService
    {
        private readonly DataContext _dataContext;
        public ItemPurchaseHistoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void DeleteItem(ItemPurchaseHistory item)
        {
            _dataContext.ItemPurchaseHistories.Remove(item);
        }

        public ResultModel<List<ItemHistoryViewModel>> GetAllItems()
        {
            var resultModel = new ResultModel<List<ItemHistoryViewModel>>();
            resultModel.Data = _dataContext.ItemPurchaseHistories.Select(x => (ItemHistoryViewModel)x).ToList();
            return resultModel;
        }

        public ResultModel<ItemHistoryViewModel> GetById(Guid Id)
        {
            var resultModel = new ResultModel<ItemHistoryViewModel>();
            resultModel.Data = _dataContext.ItemPurchaseHistories.Where(x => x.Id == Id).FirstOrDefault();
            return resultModel;
        }

        public ResultModel<string> UpdateItemHistory(ItemPurchaseHistory model)
        {
            var resultModel = new ResultModel<string>();
            if (model.Item is null)
            {
                resultModel.AddError("Model cannot be null");
                return resultModel;
            }
            var history = GetAllItems().Data.Where(x => x.ItemId == model.Item.Id).FirstOrDefault();
            if(history is null)
            {
                resultModel.AddError("History for Item does not exist");
                return resultModel;
            }

            history.Count += 1;

            try
            {
                _dataContext.ItemPurchaseHistories.Update(history);
                _dataContext.SaveChanges();
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message + " \b" + ex.InnerException == null ? "" : ex.InnerException.Message);
                return resultModel;
            }

            resultModel.Data = "Successfully Updated";
            return resultModel;
        }

        public ResultModel<string> CreateItemHistory(ItemPurchaseHistory model)
        {
            var resultModel = new ResultModel<string>();
            if (model is null)
            {
                resultModel.AddError("Model cannot be null");
                return resultModel;
            }
            if (model.Item is null)
            {
                resultModel.AddError("Model cannot be null");
                return resultModel;
            }
            try
            {
                _dataContext.ItemPurchaseHistories.Add(model);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message + " \b" + ex.InnerException == null ? "" : ex.InnerException.Message);
                return resultModel;
            }
            resultModel.Data = "Successfully Created";
            return resultModel;
        }
    }
}
