using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DTO
{
    public class BaseResult<TResult>
    {
        public TResult Result { get; set; }

        public bool IsSuccess { get; set; }

        public String Message { get; set; }

        public BaseResult() { }

        public static BaseResult<TResult> Error(String message)
        {
            return new BaseResult<TResult> { Message = message };
        }

        public static BaseResult<TResult> Error(String message, TResult result)
        {
            return new BaseResult<TResult> { Message = message, Result = result };
        }

        public static BaseResult<TResult> Success(TResult result)
        {
            return new BaseResult<TResult>
            {
                IsSuccess = true,
                Result = result
            };
        }
    }
}