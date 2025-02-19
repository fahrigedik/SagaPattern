using MassTransit;
using SagaPattern.Shared;

namespace SagaPattern.OrderService
{
    public class OrderConsumer : IConsumer<PaymentResult>
    {
        public Task Consume(ConsumeContext<PaymentResult> context)
        {
            //sipariş durumunu işlem başarılı ya da başarısıza göre değiştirebilir.

            //Product kuyruğuna işlem başarılı ise stock'tan düşebilir.

            Console.WriteLine(" Ödeme İşlemi Başarılı");

            return Task.CompletedTask;
        }

        private async Task HandleSuccessfulPayment(ConsumeContext<PaymentResult> context)
        {
            var orderId = context.Message.OrderId;
            var productId = context.Message.ProductId;

            // Sipariş durumunu "Ödeme Tamamlandı" olarak güncelle
            Console.WriteLine($"Sipariş (ID: {orderId}) için ödeme başarıyla tamamlandı.");

            // Stok düşme işlemi için event yayınla
            await context.Publish(new StockReservationRequest
            {
                OrderId = orderId,
                ProductId = productId
            });

            Console.WriteLine($"Ürün (ID: {productId}) için stok düşme talebi gönderildi.");
        }

        private Task HandleFailedPayment(ConsumeContext<PaymentResult> context)
        {
            var orderId = context.Message.OrderId;

            // Sipariş durumunu "Ödeme Başarısız" olarak güncelle
            Console.WriteLine($"Sipariş (ID: {orderId}) için ödeme başarısız oldu!");
            Console.WriteLine("Sipariş iptal ediliyor...");

            // Siparişi iptal et ve kullanıcıya bildirim gönder
            Console.WriteLine("Kullanıcıya ödeme başarısız bildirimi gönderildi.");

            return Task.CompletedTask;
        }

    }


    public class StockReservationRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }

}
