using System;

namespace Spottigus.ErrorHandling.Contracts
{
    public interface IResult
    {
        bool IsSuccessful { get; }
        bool IsFailure { get; }
        string Message { get; } 
    }
    
    public interface IResult<T>
    {
        T Value { get; }
        bool IsSuccessful { get; }
        bool IsFailure { get; }
        string Message { get; }
    }
}
