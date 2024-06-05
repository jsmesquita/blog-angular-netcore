using BuildingBlocks.Domain.Events;

namespace Blog.Domain.Domain.Posts;

public record PostPublishedEvent(Guid id, string title) : BaseEvent;