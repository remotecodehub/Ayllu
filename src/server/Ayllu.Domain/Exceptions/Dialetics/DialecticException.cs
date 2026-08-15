using Ayllu.Domain.Entities.Dialectics;

namespace Ayllu.Domain.Exceptions.Dialetics;

public sealed class DialecticException(string message, Exception? inner = null!)
    : Exception(message, inner)
{
    public static void ThrowIfAlreadyExists(Dialectic? storedDialectic = null)
    {
        if (storedDialectic is null)
            return;
        throw new DialecticException("Usuário já possui dialética com o mesmo título");
    }

    public static void ThrowIfNotExists(Dialectic? dialectic)
    {
        if (dialectic is null) 
            throw new DialecticException("Dialética não encontrada");
        return;
    }
}
