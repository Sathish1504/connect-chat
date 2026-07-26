using FluentValidation;

namespace Chat.Application.Features.Messages.MarkConversationRead;

public sealed class MarkConversationReadValidator
    : AbstractValidator<MarkConversationReadCommand>
{
    public MarkConversationReadValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty();
    }
}