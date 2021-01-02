using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spottigus.DataRepository.Contracts;
using Spottigus.DataContext;
using Microsoft.EntityFrameworkCore;
using Spottigus.ErrorHandling.Contracts;
using Spottigus.ErrorHandling.Implementations;

namespace Spottigus.DataRepository.Implementation
{
    public class GenericEfDataRepository<T> : IGenericDataRepository<T> where T : class, IModel
    {
        private readonly GenericDataContext<T> _dbContext;

        public GenericEfDataRepository(GenericDataContext<T> dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IResult> Create(T itemToCreate)
        {
            _dbContext.DataSet.Add(itemToCreate);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<IResult> Delete(Guid idToDelete)
        {
            var itemToDeleteResult = await GetById(idToDelete);

            if (itemToDeleteResult.IsSuccessful)
            {
                _dbContext.Remove(itemToDeleteResult.Value); 
                await _dbContext.SaveChangesAsync();
                return new OkResult();   
            }
            else
            {
                return new ErrorResult(itemToDeleteResult.Message);
            }
        }

        public async Task<IResult<List<T>>> GetAll()
        {
            var result = await _dbContext.DataSet.ToListAsync();
            return new OkResult<List<T>>(result);
        }

        public async Task<IResult<T>> GetById(Guid idToGet)
        {
            var result = await _dbContext.DataSet.FirstOrDefaultAsync(p => p.Id == idToGet);

            if (result != null)
            {
                return new OkResult<T>(result);
            }
            else
            {
                return new ErrorResult<T>("Model not found.");
            }
        }

        public async Task<IResult> Update(T itemToUpdate)
        {
            _dbContext.Update(itemToUpdate);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }
    }
}