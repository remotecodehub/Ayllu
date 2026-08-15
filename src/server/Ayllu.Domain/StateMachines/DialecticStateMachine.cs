using Ayllu.Domain.Entities.Dialectics;
using Ayllu.Domain.Exceptions.Dialetics;

namespace Ayllu.Domain.StateMachines;

public static class DialecticStateMachine
{
    private static readonly Dictionary<DialecticStatus, DialecticTransition[]> AllowedTransitions =
        new()
        {
            [DialecticStatus.Draft] =
            [
                DialecticTransition.PublishThesis,
                DialecticTransition.Delete
            ],

            [DialecticStatus.ThesisPublished] =
            [
                DialecticTransition.OpenAntithesis,
                DialecticTransition.Store
            ],

            [DialecticStatus.AntithesisOpen] =
            [
                DialecticTransition.CreateSynthesis,
                DialecticTransition.Close
            ],

            [DialecticStatus.SynthesisCreated] =
            [
                DialecticTransition.Close
            ],

            [DialecticStatus.Closed] =
            [
                DialecticTransition.Archive,
                DialecticTransition.Store
            ],

            [DialecticStatus.Stored] =
            [
                DialecticTransition.Archive
            ]
        };

    public static DialecticStatus Apply(
        DialecticStatus current,
        DialecticTransition transition)
    {
        if (!AllowedTransitions.TryGetValue(current, out var transitions) ||
            !transitions.Contains(transition))
        {
            throw new InvalidDialecticTransitionException(current, transition);
        }

        return transition switch
        {
            DialecticTransition.PublishThesis => DialecticStatus.ThesisPublished,
            DialecticTransition.OpenAntithesis => DialecticStatus.AntithesisOpen,
            DialecticTransition.CreateSynthesis => DialecticStatus.SynthesisCreated,
            DialecticTransition.Close => DialecticStatus.Closed,
            DialecticTransition.Store => DialecticStatus.Stored,
            DialecticTransition.Archive => DialecticStatus.Archived,
            DialecticTransition.Delete => DialecticStatus.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(transition))
        };
    }
}