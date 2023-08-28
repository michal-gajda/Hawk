namespace Hawk.Persistence.Models;

public class Person
{
    public virtual int KeyId { get; set; } = default!;
    public virtual Guid Id { get; set; } = Guid.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual int Age { get; set; } = default!;
}
