using System;
using Spottigus.ErrorHandling.Contracts;

namespace Spottigus.ErrorHandling.Implementations
{
    public class ErrorResult<T> : IResult, IResult<T>
    {
        public T Value 
        { 
            get
            {
                throw new InvalidOperationException("Value Result is in error.");
            }
        }
        
        public bool IsSuccessful => false;
        public bool IsFailure => true;
        public string Message => Exception.Message;
        public Exception Exception { get; }
        public ErrorResult(string message)
        {
            Exception = new Exception(message);
        }

        public ErrorResult(Exception ex)
        {
            Exception = ex;
        }
    }

    public class ErrorResult : ErrorResult<object>
    {
        public ErrorResult(string message) : base(message)
        {
            
        }

        public ErrorResult(Exception ex) : base (ex)
        {
        }
    }
}