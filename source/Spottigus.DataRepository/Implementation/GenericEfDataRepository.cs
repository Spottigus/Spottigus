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

        public async Task<IResult> Delete(T itemToDelete)
        {
            try
            {
                _dbContext.Remove(itemToDelete); 
                var affectedRecordCount = await _dbContext.SaveChangesAsync();

                if (affectedRecordCount == 1)
                {
                    return new OkResult();
                }
                else
                {
                    return new ErrorResult($"Error deleting record. Number of records deleted was {0}.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex);
            }
        }

        public async Task<IResult<List<T>>> GetAll()
        {
            try
            {
                var result = await _dbContext.DataSet.ToListAsync();
                return new OkResult<List<T>>(result);
            }
            catch(Exception ex)
            {
                return new ErrorResult<List<T>>(ex);
            }
        }

        public async Task<IResult<T>> GetById(Guid idToGet)
        {
            try
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
            catch (Exception ex)
            {
                return new ErrorResult<T>(ex);
            }
        }

        public async Task<IResult> Update(T itemToUpdate)
        {
            try
            {
                _dbContext.Update(itemToUpdate);
                var affectedRecordCount = await _dbContext.SaveChangesAsync();

                if (affectedRecordCount == 1)
                {
                    return new OkResult();
                }
                else
                {
                    return new ErrorResult($"Error updating record. Number of records updated was {0}.");
                }

            }
            catch(Exception ex)
            {
                return new ErrorResult(ex);
            }
        }
    }
}