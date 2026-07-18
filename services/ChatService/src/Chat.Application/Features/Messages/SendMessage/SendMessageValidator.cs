using Chat.Domain.Enums;
using FluentValidation;

namespace Chat.Application.Features.Messages.SendMessage;

public class SendMessageValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty();

        RuleFor(x => x.Type)
            .IsInEnum();

        When(x => x.Type == MessageType.Text, () =>
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(4000);
        });
    }
}