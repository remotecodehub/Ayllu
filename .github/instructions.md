# AI implementation instructions

- Act as a Senior .NET 10.0 software engineer.
- Keep one project per architectural layer (Application, Domain, Infrastructure) in both client and server solutions.
- The only project outside this concern is `Ayllu.Sdk`.
- Use Mediator.Net 5.0.0 for application message dispatching. Do not introduce MediatR.

## Architecture

### Client

MAUI Presentation -> Ayllu.Application -> Ayllu.Infrastructure -> Ayllu.Sdk / external services.

Commands and requests must be handled through Mediator.Net. Keep domain rules in `Ayllu.Domain`; keep HTTP and external integrations in Infrastructure.

### Server

ASP.NET Core Presentation (Controllers + Razor/Blazor) -> Ayllu.Application -> Ayllu.Infrastructure -> Ayllu.Domain.

Controllers and Razor pages must dispatch application commands/requests through `Mediator.Net.IMediator`. Application handlers own orchestration and depend on application abstractions; Infrastructure implements those abstractions.

## Mediator.Net rules

- Use `Mediator.Net.Contracts.IRequest<TResponse>` for request/response messages.
- Use `Mediator.Net.Contracts.ICommand` for commands without a response.
- Use `Mediator.Net.Contracts.IEvent` for notifications/events.
- Use `Mediator.Net.Contracts.IRequestHandler<TRequest,TResponse>` for request handlers.
- Handlers receive `IReceiveContext<TMessage>` and `CancellationToken`.
- Use `RequestAsync<TRequest,TResponse>` for `IRequest<TResponse>` messages.
- Use `SendAsync` only for commands.
- Use `PublishAsync` for events.
- Register handlers from the Application assembly through `Mediator.Net.MicrosoftDependencyInjection`.
- Cross-cutting concerns such as validation must be implemented as Mediator.Net pipe specifications, not MediatR `IPipelineBehavior`.
- Never add a MediatR package, namespace, interface, handler or pipeline behavior.

## Validation

Use FluentValidation 12.x. Validators belong to Application. Validation failures must continue to use the existing `ApplicationValidationException` and global exception handling semantics.

## Client build policy

The MAUI client has a matrix build in GitHub Actions. Do not use the client matrix as the acceptance criterion for server tasks unless explicitly requested.

## Server build policy

For server changes, the authoritative CI criterion is the server build job. Always inspect the `build-web`/server job and its logs when validating a change. Do not treat client matrix failures as failures of a server-only change.

## Coding standards

- Prefer file-scoped namespaces.
- One Razor directive per line.
- Prefer primary constructors when appropriate.
- Do not add stubs, TODO implementations, or `NotImplementedException`.
- Public APIs require complete XML documentation in en-US.
- Preserve existing behavior and public HTTP contracts when refactoring infrastructure/framework dependencies.
- Keep exception propagation compatible with the global RFC Problem Details handler.
