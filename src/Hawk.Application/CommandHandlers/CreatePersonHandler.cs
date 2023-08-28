namespace Hawk.Application.CommandHandlers;

using Hawk.Application.Commands;
using Hawk.Domain.Entities;
using Hawk.Domain.Interfaces;

internal sealed class CreatePersonHandler : IRequestHandler<CreatePerson>
{
    private readonly ILogger<CreatePersonHandler> logger;
    private readonly IPersonRepository repository;

    public CreatePersonHandler(ILogger<CreatePersonHandler> logger, IPersonRepository repository) => (this.logger, this.repository) = (logger, repository);

    public async Task Handle(CreatePerson request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Creating person with id {Id}", request.Id);
        var entity = new PersonEntity
        {
            Id = request.Id,
            Name = request.Name,
            Age = request.Age,
        };
        
        await repository.CreateAsync(entity, cancellationToken).ConfigureAwait(false);
        this.logger.LogInformation("Created person with id {Id}", request.Id);
    }
}
