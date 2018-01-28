using System.Collections.Generic;

namespace RLPlayground
{
    public class Transition
    {
        public Actions TargetAction { get; }
        public List<ActionProbability> ActionProbabilities { get; }

        public Transition(Actions targetAction, List<ActionProbability> actionProbabilities)
        {
            TargetAction = targetAction;
            ActionProbabilities = actionProbabilities;
        }
    }
}
