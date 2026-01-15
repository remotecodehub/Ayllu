using Ayllu.Web.Domain.Entities.Dialectics;

namespace Ayllu.Web.Domain.Exceptions.Dialetics;

public class InvalidDialecticTransitionException(
    DialecticStatus current,
    DialecticTransition transition) : Exception($"Transição inválida: {current} → {transition}")
{
}