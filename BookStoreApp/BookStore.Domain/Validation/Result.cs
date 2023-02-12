using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Validation
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; private set; }

        protected Result()
        {

        }

        public static Result Create(ResultStatus resultStatus, string? message = default)
        {
            if(resultStatus == ResultStatus.Success)
            {
                return new Result { IsSuccess = true };
            }

            return new Result
            {
                IsSuccess = false,
                ErrorMessage = message
            };
        }

        public static Result Success()
        {
            return new Result { IsSuccess = true };
        }

        public static Result Failure(string? message = default)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = message
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
