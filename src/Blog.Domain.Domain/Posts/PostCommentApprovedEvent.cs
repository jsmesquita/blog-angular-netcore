using BuildingBlocks.Domain.Events;

namespace Blog.Domain.Domain.Posts;

public record PostCommentApprovedEvent(Guid id, string content) : BaseEvent;