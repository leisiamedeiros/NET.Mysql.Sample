using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NET.Mysql.Sample.WebApi.Transport
{
    public class ErrorResponse
    {
        public IEnumerable<Error> Errors { get; set; }

        public ErrorResponse(IEnumerable<Error> errors)
        {
            Errors = errors;
        }

        public static ErrorResponse GetErrorResponseFromError(params Error[] errors)
        {
            return new ErrorResponse(errors);
        }

        public static ErrorResponse GetErrorResponseFromValidation(IEnumerable<ValidationFailure> failures)
        {
            var errors = failures.Select(
                error => new Error { Code = error.ErrorCode, Title = error.PropertyName, Detail = error.ErrorMessage }
            );

            return new ErrorResponse(errors);
        }

        public static ErrorResponse GetErrorResponseFromModelState(ModelStateDictionary modelState)
        {
            var stateErrors = modelState.ToDictionary(
                keySelector => keySelector.Key,
                keySelector => keySelector.Value.Errors.Select(err => err.ErrorMessage)
            );

            var errors = stateErrors.SelectMany(
                element => element.Value.Select(
                    error => new Error { Code = "Status400BadRequest", Title = element.Key, Detail = error }
                )
            );

            return new ErrorResponse(errors);
        }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}
