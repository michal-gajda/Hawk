namespace Hawk.Persistence.Services;

using Hawk.Domain.Entities;
using Hawk.Domain.Interfaces;
using Hawk.Persistence.Models;

internal sealed class PersonRepository : IPersonRepository
{
    private readonly ILogger<PersonRepository> logger;
    private readonly ISession session;

    public PersonRepository(ILogger<PersonRepository> logger, ISession session) => (this.logger, this.session) = (logger, session);

    public async Task CreateAsync(PersonEntity entity, CancellationToken cancellationToken = default)
    {
        this.logger.LogInformation("Creating person entity {@entity}", entity);
        using var transaction = this.session.BeginTransaction();
        
        var person = new Person
        {
            Id = entity.Id,
            Name = entity.Name,
            Age = entity.Age,
        };

        await session.SaveAsync(person, cancellationToken).ConfigureAwait(false);
        transaction.Commit();
        this.logger.LogInformation("Person entity created {@entity}", entity);
    }
}
