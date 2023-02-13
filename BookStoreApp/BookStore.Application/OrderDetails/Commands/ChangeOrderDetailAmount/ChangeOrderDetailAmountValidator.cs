using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount
{
    public class ChangeOrderDetailAmountValidator : AbstractValidator<ChangeOrderDetailAmount>
    {
        public ChangeOrderDetailAmountValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
