using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Itoil.DTO
{
    /// <summary>
    /// Класс с данными о результате операции
    /// </summary>
    public class BaseResult
    {
        public bool IsSuccess { get; set; }

        public String Message { get; set; }

        public static BaseResult Error(String message)
        {
            return new BaseResult { Message = message };
        }

        public static BaseResult Success()
        {
            return new BaseResult
            {
                IsSuccess = true
            };
        }
    }

    /// <summary>
    /// Класс с данными о результате операции
    /// <typeparamref name="TResult">Тип результата, который возвращает операция</typeparamref>
    /// </summary>
    public class BaseResult<TResult> : BaseResult
    {
        public TResult Result { get; set; }

        public BaseResult() { }

        public static new BaseResult<TResult> Error(String message)
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