using Ayllu.Domain.Entities.Dialectics;

namespace Ayllu.Domain.Exceptions.Dialetics;

public class InvalidDialecticTransitionException(
    DialecticStatus current,
    DialecticTransition transition) : Exception($"Transição inválida: {current} → {transition}")
{
}