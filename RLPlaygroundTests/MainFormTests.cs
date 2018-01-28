using NUnit.Framework;
using RLPlayground;

namespace RLPlaygroundTests
{
    [TestFixture]
    public class MainFormTests
    {
        private MainForm mainForm;

        static object[] getPprobabilityTestCases = 
        {
            //new object[] { new State(1,1), Actions.Up, new State(1,1), 0f },
            new object[] { new State(1, 1), Actions.Up, new State(2, 1), 0f },
            new object[] { new State(1, 1), Actions.Up, new State(0, 1), 0.8f },
            new object[] { new State(1, 1), Actions.Up, new State(1, 0), 0.1f },
            new object[] { new State(1, 1), Actions.Up, new State(1, 2), 0.1f },

            //new object[] { new State(1,1), Actions.Right, new State(1,1), 0f },
            new object[] { new State(1, 1), Actions.Right, new State(1, 0), 0f },
            new object[] { new State(1, 1), Actions.Right, new State(1, 2), 0.8f },
            new object[] { new State(1, 1), Actions.Right, new State(2, 1), 0.1f },
            new object[] { new State(1, 1), Actions.Right, new State(0, 1), 0.1f },

            //new object[] { new State(1,1), Actions.Down, new State(1,1), 0f },
            new object[] { new State(1, 1), Actions.Down, new State(0, 1), 0f },
            new object[] { new State(1, 1), Actions.Down, new State(2, 1), 0.8f },
            new object[] { new State(1, 1), Actions.Down, new State(1, 2), 0.1f },
            new object[] { new State(1, 1), Actions.Down, new State(1, 0), 0.1f },

            //new object[] { new State(1,1), Actions.Left, new State(1,1), 0f },
            new object[] { new State(1, 1), Actions.Left, new State(1, 2), 0f },
            new object[] { new State(1, 1), Actions.Left, new State(1, 0), 0.8f },
            new object[] { new State(1, 1), Actions.Left, new State(2, 1), 0.1f },
            new object[] { new State(1, 1), Actions.Left, new State(0, 1), 0.1f },
        };
                
        [SetUp]
        public void Setup()
        {
            mainForm = new MainForm();
        }

        [Test, TestCaseSource(nameof(getPprobabilityTestCases))]        
        public void GetProbability_ReturnsCorrectValue(State state, Actions action, State targetState, float expectedProbability)
        {
            var result = mainForm.GetProbability(state, action, targetState);
            Assert.AreEqual(expectedProbability, result);
        }
    }
}
