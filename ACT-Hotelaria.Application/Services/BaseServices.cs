using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Interface;
using ACT_Hotelaria.Domain.Notification;
using FluentValidation;
using FluentValidation.Results;

namespace ACT_Hotelaria.Application.Services;

public abstract class BaseServices
{
    private readonly INotification _notifier;

    public BaseServices(INotification notifier)
    {
        _notifier = notifier;
    }
    protected void Notify(ValidationResult validationResult)
    {
        foreach (var item in validationResult.Errors)
        {
            Notify(item.ErrorMessage);
        }
    }

    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    protected bool ExecuteNotification<TV, TE>(TV validation, TE entity)
        where TV : AbstractValidator<TE>
        where TE : BaseEntity
    {
        var validationResult = validation.Validate(entity);
        if (validationResult.IsValid)
        {
            return true;
        }
        Notify(validationResult);
        return false;
    }
}