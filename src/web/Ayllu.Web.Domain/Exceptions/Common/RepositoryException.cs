namespace Ayllu.Web.Domain.Exceptions.Common;

public class RepositoryException(string message, Exception? inner = null!) : Exception(message, inner)
{
}
