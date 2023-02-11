using BookStore.Persistance.Validation;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                await next();
            }

            var errors = _validators
                        .Select(v => v.Validate(request))
                        .SelectMany(vr => vr.Errors)
                        .Where(vf => vf is not null)
                        .Distinct()
                        .ToArray();

            if (errors.Any())
            {
                throw new BookStoreException(errors.First().ErrorMessage);
            }

            return await next();
        }
    }
}
