using Blog.Domain.Application;
using Blog.Domain.Domain.Posts;
using Blog.Domain.Infrastructure.Repositories;
using BuildingBlocks.Application.Behaviours;
using BuildingBlocks.Application.Security;
using BuildingBlocks.Domain.Repositories;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

    config.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));

    //config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
});

AssemblyScanner
    .FindValidatorsInAssemblies(AppDomain.CurrentDomain.GetAssemblies())
    .ForEach(result => builder.Services.AddScoped(result.InterfaceType, result.ValidatorType));

builder.Services.AddScoped<IUser, Blog.Domain.Application.Secutiry.User>();
builder.Services.AddScoped<IIdentityService, Blog.Domain.Application.Secutiry.IdentityService>();

var app = builder.Build();

app.MapPost("/posts", async (CreatePostRequest request, IMediator mediator) =>
{

    var result = await mediator.Send(request);

    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Reasons);
});

app.Run();