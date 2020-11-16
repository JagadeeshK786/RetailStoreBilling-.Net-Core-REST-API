using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using StoreDAC.Repositories;
using StoreServices.Infrastructure;
using StoreDAC.Entities;

namespace StoreServices
{
    public class ProdCategoryService : IProdCategoryService
    {
        private readonly IProdCategoryRepository _prodCatRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public ProdCategoryService(IProdCategoryRepository ProdCatRepo, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _prodCatRepo = ProdCatRepo;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
        {
            //caching
            var categories = await _cache.GetOrCreateAsync(CacheKeys.ProdCategoryList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _prodCatRepo.ListAsync();
            });
            
            //output validation
            if (categories == null || !categories.Any())
                throw new Exception($"No Categories exist.");

            return categories;
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(long Id)
        {
            var category = await _prodCatRepo.FindByIdAsync(Id);

            if (category == null)
                throw new Exception($"No Categoy exists with Id: {Id}");

            return category;
        }


        //public async Task<TaskResponse> SaveAsync(Tasklist task)
        //{
        //    try
        //    {
        //        await _ProdCatRepo.AddAsync(task);
        //        await _unitOfWork.CompleteAsync();

        //        return new TaskResponse(task);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do some logging stuff
        //        return new TaskResponse($"An error occurred when saving the Task: {ex.Message}");
        //    }
        //}

        //public async Task<TaskResponse> UpdateAsync(string id, Tasklist task)
        //{
        //    var existingTask = await _ProdCatRepo.FindByIdAsync(id);

        //    if (existingTask == null)
        //        return new TaskResponse("Task not found.");

        //    existingTask.Title = task.Title;

        //    try
        //    {
        //        _ProdCatRepo.Update(existingTask);
        //        await _unitOfWork.CompleteAsync();

        //        return new TaskResponse(existingTask);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do some logging stuff
        //        return new TaskResponse($"An error occurred when updating the Task: {ex.Message}");
        //    }
        //}

        //public async Task<TaskResponse> DeleteAsync(string id)
        //{
        //    var existingTask = await _ProdCatRepo.FindByIdAsync(id);

        //    if (existingTask == null)
        //        return new TaskResponse("Task not found.");

        //    try
        //    {
        //        _ProdCatRepo.Remove(existingTask);
        //        await _unitOfWork.CompleteAsync();

        //        return new TaskResponse(existingTask);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do some logging stuff
        //        return new TaskResponse($"An error occurred when deleting the Task: {ex.Message}");
        //    }
        //}


        //public TeamTaskMetricsResponse ListTeamMetricsAsync()
        //{
        //    try
        //    {
        //        var teamTaskMetrics = _ProdCatRepo.ListTeamMetricsAsync();
        //        if (teamTaskMetrics != null && teamTaskMetrics.Count() > 0)
        //        {
        //            return new TeamTaskMetricsResponse(teamTaskMetrics);
        //        }
        //        else
        //        {
        //            return new TeamTaskMetricsResponse($"An error occurred when retrieving the teamTaskMetrics Count: No records found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Do some logging stuff
        //        return new TeamTaskMetricsResponse($"An error occurred when retrieving the Task Count: {ex.Message}");
        //    }

        //}

        //Task<ProductCategory> IProdCategoryService.GetByIdAsync(string Id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<ProductCategory>> IProdCategoryService.GetCategoriesAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
