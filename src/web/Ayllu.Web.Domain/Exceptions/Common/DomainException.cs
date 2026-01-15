namespace Ayllu.Web.Domain.Exceptions.Common;

public abstract class DomainException(string message, Exception? inner = null) : Exception(message, inner)
{
}
