using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrder>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Total).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CreationDate).GreaterThanOrEqualTo(new DateTime(2023, 01, 01));
        }
    }
}
