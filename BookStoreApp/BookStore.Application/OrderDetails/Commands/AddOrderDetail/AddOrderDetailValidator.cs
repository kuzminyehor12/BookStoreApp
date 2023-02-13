using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.OrderDetails.Commands.AddOrderDetail
{
    public class AddOrderDetailValidator : AbstractValidator<AddOrderDetail>
    {
        public AddOrderDetailValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
