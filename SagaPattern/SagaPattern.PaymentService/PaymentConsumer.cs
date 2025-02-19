using MassTransit;
using SagaPattern.Shared;

namespace SagaPattern.PaymentService
{
    public class PaymentConsumer(IPublishEndpoint publishEndpoint) : IConsumer<CreateOrder>
    {
        public async Task Consume(ConsumeContext<CreateOrder> context)
        {
            //Ödeme başarılı ya da başarısız durumuna göre işlemler yapılır.

            var result = new PaymentResult
            {
                OrderId = context.Message.Id,
                ProductId = context.Message.ProductId,
                IsDone = true
            };

            await publishEndpoint.Publish(result);
        }
    }
}
