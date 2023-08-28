namespace Hawk.Application.Commands;

public sealed record CreatePerson : IRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public int Age { get; init;} = default!;
}
