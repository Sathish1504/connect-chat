using FluentValidation;
using Chat.Domain.Enums;

namespace Chat.Application.Features.Conversations.CreateConversation;

public class CreateConversationValidator
    : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationValidator()
    {
        RuleFor(x => x.ParticipantIds)
            .NotEmpty();

        RuleFor(x => x.Type)
            .IsInEnum();

        When(x => x.Type == ConversationType.Group, () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
        });
    }
}