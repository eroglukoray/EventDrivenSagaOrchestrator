using MassTransit;
using Shared.Events;
using System;

namespace OrderService.Sagas
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State WaitingForPayment { get; private set; }
        public State WaitingForStock { get; private set; }
        public State Completed { get; private set; }
        public State Canceled { get; private set; }

        public Event<OrderCreated> OrderSubmitted { get; private set; }
        public Event<PaymentSucceeded> PaymentReceived { get; private set; }
        public Event<PaymentFailed> PaymentFailed { get; private set; }
        public Event<StockReserved> StockConfirmed { get; private set; }
        public Event<StockFailed> StockFailed { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentReceived, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentFailed, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => StockConfirmed, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => StockFailed, x => x.CorrelateById(m => m.Message.OrderId));

            Initially(
                When(OrderSubmitted)
                    .Then(context => Console.WriteLine($"[Saga] Order Created: {context.Data.OrderId}"))
                    .TransitionTo(WaitingForPayment)
            );

            During(WaitingForPayment,
                When(PaymentReceived)
                    .Then(context => Console.WriteLine($"[Saga] Payment Successful for Order {context.Data.OrderId}"))
                    .TransitionTo(WaitingForStock),
                When(PaymentFailed)
                    .Then(context => Console.WriteLine($"[Saga] Payment Failed for Order {context.Data.OrderId}. Cancelling order..."))
                    .TransitionTo(Canceled)
            );

            During(WaitingForStock,
                When(StockConfirmed)
                    .Then(context => Console.WriteLine($"[Saga] Stock Reserved for Order {context.Data.OrderId}"))
                    .TransitionTo(Completed),
                When(StockFailed)
                    .Then(context => Console.WriteLine($"[Saga] Stock Reservation Failed for Order {context.Data.OrderId}. Cancelling order..."))
                    .TransitionTo(Canceled)
            );

            SetCompletedWhenFinalized();
        }
    }
}
