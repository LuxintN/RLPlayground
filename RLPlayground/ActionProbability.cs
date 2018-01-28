namespace RLPlayground
{
    public class ActionProbability
    {
        public Actions Action { get; }
        public float Probability { get; }

        public ActionProbability(Actions action, float probability)
        {
            Action = action;
            Probability = probability;
        }
    }
}
