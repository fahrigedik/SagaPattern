var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SagaPattern_OrderService>("sagapattern");

builder.AddProject<Projects.SagaPattern_PaymentService>("sagapattern-paymentservice");

builder.Build().Run();
