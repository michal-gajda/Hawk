namespace Hawk.WebApi.Controllers;

using Hawk.Application.Commands;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]")]
public sealed class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> logger;
    private readonly ISender mediator;

    public PersonController(ILogger<PersonController> logger, ISender mediator) => (this.logger, this.mediator) = (logger, mediator);

    [HttpPost(Name = "CreatePerson")]
    public async Task<IActionResult> CreateAsync(CreatePerson request, CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken).ConfigureAwait(false);
        
        return await Task.FromResult(Ok()).ConfigureAwait(false);
    }
}
