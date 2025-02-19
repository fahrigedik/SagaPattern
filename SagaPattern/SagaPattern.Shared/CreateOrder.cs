namespace SagaPattern.Shared;
public class CreateOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProductId { get; set; } = Guid.NewGuid();

    public string CardNumber { get; set; } = "11111";
}

