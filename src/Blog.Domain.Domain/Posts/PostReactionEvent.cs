using BuildingBlocks.Domain.Events;

namespace Blog.Domain.Domain.Posts;

public record PostReactionEvent(string reactionType) : BaseEvent;