using BookStore.Application.Authors.Commands.CreateAuthor;
using BookStore.Application.Authors.Commands.DeleteAuthor;
using BookStore.Application.Authors.Commands.UpdateAuthor;
using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.DeleteBook;
using BookStore.Application.Books.Commands.UpdateBook;
using BookStore.Application.OrderDetails.Commands.AddOrderDetail;
using BookStore.Application.OrderDetails.Commands.ChangeOrderDetailAmount;
using BookStore.Application.OrderDetails.Commands.RemoveOrderDetail;
using BookStore.Application.Orders.Commands.CreateOrder;
using BookStore.Application.Orders.Commands.DeleteOrder;
using BookStore.Application.Orders.Commands.UpdateOrder;
using MassTransit;
using Microsoft.Extensions.Options;

namespace BookStore.WebApi.Extensions
{
    public static class BusRegistrationConfiguratorExtensions
    {
        public static void AddBookEventConsumers(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<CreateBookConsumer>();
            configurator.AddConsumer<UpdateBookConsumer>();
            configurator.AddConsumer<DeleteBookConsumer>();
        }

        public static void AddAuthorEventConsumers(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<CreateAuthorConsumer>();
            configurator.AddConsumer<UpdateAuthorConsumer>();
            configurator.AddConsumer<DeleteAuthorConsumer>();
        }

        public static void AddDetailEventConsumers(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<AddOrderDetailConsumer>();
            configurator.AddConsumer<ChangeOrderDetailAmountConsumer>();
            configurator.AddConsumer<RemoveOrderDetailConsumer>();
        }

        public static void AddOrderEventConsumers(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<CreateOrderConsumer>();
            configurator.AddConsumer<UpdateOrderConsumer>();
            configurator.AddConsumer<DeleteOrderConsumer>();
        }
    }
}
