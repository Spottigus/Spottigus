using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spottigus.ErrorHandling.Contracts;

namespace Spottigus.DataRepository.Contracts
{
    public interface IGenericDataRepository<T> where T : class
    {
        Task<IResult<T>> GetById(Guid idToGet);
        Task<IResult<List<T>>> GetAll();
        Task<IResult> Update(T itemToUpdate);
        Task<IResult> Create(T itemToCreate);
        Task<IResult> Delete(Guid idToDelete);
    }
}