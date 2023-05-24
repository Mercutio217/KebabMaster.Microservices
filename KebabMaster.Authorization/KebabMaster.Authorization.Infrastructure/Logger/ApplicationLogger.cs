using KebabMaster.Authorization.Domain.Exceptions;
using KebabMaster.Authorization.Domain.Interfaces;

namespace KebabMaster.Authorization.Infrastructure.Logger;

public class ApplicationLogger : IApplicationLogger
{
    
    public void LogRegistrationStart(object request)
    {
        throw new NotImplementedException();
    }

    public void LogRegistrationEnd(object request)
    {
        throw new NotImplementedException();
    }

    public void LogGetStart(object request)
    {
        throw new NotImplementedException();
    }

    public void LogGetEnd(object request)
    {
        throw new NotImplementedException();
    }

    public void LogDeleteStart(object request)
    {
        throw new NotImplementedException();
    }

    public void LogDeleteEnd(object request)
    {
        throw new NotImplementedException();
    }

    public void LogException(Exception exception)
    {
        throw new NotImplementedException();
    }

    public void LogValidationException(ApplicationValidationException applicationValidationException)
    {
        throw new NotImplementedException();
    }
}