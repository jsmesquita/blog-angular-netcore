using Blog.Domain.Domain.Posts;
using Blog.Domain.Domain.Users;
using BuildingBlocks.Application.Security;
using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Domain.Utility;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Blog.Domain.Application;

public record CreatePostRequest(string Title, string Content, List<string> Categories, List<string> Tags) : IRequest<Result<Guid>>;

public class CreatePostRequestValidation : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidation()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.Content)
            .NotEmpty();

        RuleFor(c => c.Categories)
            .NotEmpty();

        RuleFor(c => c.Tags)
        .NotEmpty();
    }
}

internal class CreatePostRequestHandler : IRequestHandler<CreatePostRequest, Result<Guid>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUser _user;

    public CreatePostRequestHandler(IPostRepository postRepository, IUnitOfWork unitOfWork, IUser user)
    {
        Ensure.NotNull(postRepository, nameof(postRepository));
        Ensure.NotNull(unitOfWork, nameof(unitOfWork));
        Ensure.NotNull(user, nameof(user));

        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }

    public async Task<Result<Guid>> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var post = Post.Create(request.Title, request.Content, User.Create(new EntityId<User>(new Guid(_user.Id)), _user.Name));

        request.Tags.ForEach(post.AddTag);

        request.Tags.ForEach(post.AddCategory);

        await _postRepository.CreateAsync(post);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(post.Id.Value);
    }
}