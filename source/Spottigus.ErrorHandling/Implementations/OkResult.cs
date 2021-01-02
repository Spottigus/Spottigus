using System;
using Spottigus.ErrorHandling.Contracts;

namespace Spottigus.ErrorHandling.Implementations
{
    public class OkResult<T> : IResult, IResult<T>
    {
        public T Value { get; }
        public bool IsSuccessful => true;
        public bool IsFailure => false;
        public string Message => "Success";

        public OkResult(T value)
        {
            Value = value;
        }
    }

    public class OkResult : OkResult<object>
    {
        public OkResult() : base(new object())
        {
            
        }
    }
}