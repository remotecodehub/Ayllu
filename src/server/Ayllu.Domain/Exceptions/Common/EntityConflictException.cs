namespace Ayllu.Domain.Exceptions.Common;

public sealed class EntityConflictException(string propertyName, string message, Exception? inner = null!) : Exception(message, inner)
{
    public string PropertyName { get; } = propertyName;
}
