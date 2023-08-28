namespace Hawk.Domain.Entities;

public sealed class PersonEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = default!;
}
