namespace Hawk.Domain.Interfaces;

using Hawk.Domain.Entities;

public interface IPersonRepository
{
    Task CreateAsync(PersonEntity entity, CancellationToken cancellationToken = default);
}
