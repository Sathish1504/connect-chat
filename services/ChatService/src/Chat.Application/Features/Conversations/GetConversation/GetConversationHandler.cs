using Chat.Application.Interfaces;
using MediatR;

namespace Chat.Application.Features.Conversations.GetConversations;

public sealed class GetConversationsHandler
    : IRequestHandler<GetConversationsQuery, IReadOnlyList<ConversationSummaryResponse>>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly ICurrentUserService _currentUser;

    public GetConversationsHandler(
        IConversationRepository conversationRepository,
        ICurrentUserService currentUser)
    {
        _conversationRepository = conversationRepository;
        _currentUser = currentUser;
    }

    public async Task<IReadOnlyList<ConversationSummaryResponse>> Handle(
        GetConversationsQuery request,
        CancellationToken cancellationToken)
    {
        return await _conversationRepository.GetUserConversationsAsync(
            _currentUser.UserId,
            cancellationToken);
    }
}