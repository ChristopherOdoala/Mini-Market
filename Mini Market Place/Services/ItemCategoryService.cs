using Mini_Market_Place.Entities;
using Mini_Market_Place.Helper;
using Mini_Market_Place.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Market_Place.Services
{
    public interface IItemCategoryService
    {
        ResultModel<List<ItemCategoryViewModel>> GetAllItems();
        ResultModel<ItemCategoryViewModel> GetById(Guid Id);
        void DeleteItem(ItemCategory item);
        ResultModel<ItemCategoryViewModel> UpdateItem(ItemCategoryViewModel model);
        ResultModel<string> CreateItem(ItemCategoryViewModel model);
    }
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly DataContext _dataContext;
        public ItemCategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultModel<string> CreateItem(ItemCategoryViewModel model)
        {
            var resultModel = new ResultModel<string>();
            if (model is null)
            {
                resultModel.AddError("Model cannot be null");
                return resultModel;
            }

            try
            {
                _dataContext.ItemCategories.Add(model);
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

        public void DeleteItem(ItemCategory item)
        {
            _dataContext.ItemCategories.Remove(item);
        }

        public ResultModel<List<ItemCategoryViewModel>> GetAllItems()
        {
            var resultModel = new ResultModel<List<ItemCategoryViewModel>>();
            resultModel.Data = _dataContext.ItemCategories.Select(x => (ItemCategoryViewModel)x).ToList();
            return resultModel;
        }

        public ResultModel<ItemCategoryViewModel> GetById(Guid Id)
        {
            var resultModel = new ResultModel<ItemCategoryViewModel>();
            resultModel.Data = _dataContext.ItemCategories.Where(x => x.Id == Id).FirstOrDefault();
            return resultModel;
        }

        public ResultModel<ItemCategoryViewModel> UpdateItem(ItemCategoryViewModel model)
        {
            var resultModel = new ResultModel<ItemCategoryViewModel>();
            if (model.Id == Guid.Empty)
            {
                resultModel.AddError("Item Id cannot be null");
                return resultModel;
            }
            try
            {
                _dataContext.ItemCategories.Update(model);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message + " \b" + ex.InnerException == null ? "" : ex.InnerException.Message);
                return resultModel;
            }

            return resultModel;
        }
    }
}
