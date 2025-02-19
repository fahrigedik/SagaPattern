using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaPattern.Shared
{
    public class PaymentResult
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsDone { get; set; }
    }
}
