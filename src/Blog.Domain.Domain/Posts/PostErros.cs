using FluentResults;

namespace Blog.Domain.Domain.Posts;

internal class PostErrors
{
    public static readonly Error AttemptPublishPostHasAlreadyPublished = new("Attempting to publish a post that has already been published");
    public static readonly Error AttemptDeleteNonExistentTag = new("Attempt to delete a non-existent tag");
    public static readonly Error AttemptDeleteNonExistentCategory = new("Attempt to delete a non-existent category");
    public static readonly Error AttemptDeleteNonExistentComment = new("Attempt to delete a non-existent comment");
    public static readonly Error AttemptApproveNonExistentComment = new("Attempt to approve a non-existent comment");
}