using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RLPlayground
{
    public partial class MainForm : Form
    {
        private const int cellHeight = 100;
        private const int cellWidth = 100;
        
        // TODO: set these dynamically via GUI
        private float livingReward = -0.04f;
        private float goalReward = 1;
        private float dangerReward = -1;
        private float discountFactor = 1f;
        private int gridViewRowCount = 3;
        private int gridViewColumnCount = 4;
        
        Random random = new Random();

        private DataGridViewCell currentCell;

        private static List<Transition> transitionModel = new List<Transition>()
        {
            new Transition(Actions.Up, new List<ActionProbability>
            {
                new ActionProbability(Actions.Up, 0.8f),
                new ActionProbability(Actions.Right, 0.1f),
                new ActionProbability(Actions.Left, 0.1f)
            }),
            new Transition(Actions.Right, new List<ActionProbability>
            {
                new ActionProbability(Actions.Right, 0.8f),
                new ActionProbability(Actions.Up, 0.1f),
                new ActionProbability(Actions.Down, 0.1f)
            }),
            new Transition(Actions.Down, new List<ActionProbability>
            {
                new ActionProbability(Actions.Down, 0.8f),
                new ActionProbability(Actions.Left, 0.1f),
                new ActionProbability(Actions.Right, 0.1f)
            }),
            new Transition(Actions.Left, new List<ActionProbability>
            {
                new ActionProbability(Actions.Left, 0.8f),
                new ActionProbability(Actions.Down, 0.1f),
                new ActionProbability(Actions.Up, 0.1f)
            })
        };

        private List<float[][]> values;
        private States[][] states;
        private Actions[] actions;

        private int currentIteration;
        public int CurrentIteration
        {
            get => currentIteration;
            set 
            {
                currentIteration = value;
                iterationLabel.Text = $"Iteration: {value}";
            }
        }

        #region Construction and initialization

        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeStateTypeComboBox();

            InitializeGridView();

            ResetValues();
        }

        private void InitializeStateTypeComboBox()
        {
            stateTypeComboBox.Items.Add(new ComboBoxItemWithColor("Space", States.Space, Color.White));
            stateTypeComboBox.Items.Add(new ComboBoxItemWithColor("Wall", States.Wall, Color.Gray));
            stateTypeComboBox.Items.Add(new ComboBoxItemWithColor("Start position", States.StartPosition, Color.Blue));
            stateTypeComboBox.Items.Add(new ComboBoxItemWithColor("Goal (terminal)", States.Goal, Color.Green));
            stateTypeComboBox.Items.Add(new ComboBoxItemWithColor("Danger (terminal)", States.Danger, Color.Red));
            stateTypeComboBox.SelectedIndex = 0;

            stateTypeComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            stateTypeComboBox.DrawItem += StateTypeComboBox_DrawItem;
        }

        private void InitializeGridView()
        {
            var cellTemplate = new DataGridViewTextBoxCell();

            for (int i = 0; i < gridViewColumnCount; i++)
            {
                var column = new DataGridViewColumn(cellTemplate) { Width = cellWidth };
                gridView.Columns.Add(column);
            }

            for (int i = 0; i < gridViewRowCount; i++)
            {
                var row = new DataGridViewRow() { Height = cellHeight };
                gridView.Rows.Add(row);
            }

            gridView.Size = new Size(gridViewColumnCount * cellWidth, gridViewRowCount * cellHeight);
            gridView.ScrollBars = ScrollBars.None;

            gridView.ColumnHeadersVisible = false;
            gridView.RowHeadersVisible = false;
            gridView.AllowUserToResizeRows = false;
            gridView.AllowUserToResizeColumns = false;
            gridView.AllowUserToOrderColumns = false;
            gridView.AllowUserToAddRows = false;
            gridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            gridView.MultiSelect = false;

            gridView.CellMouseEnter += GridView_CellMouseEnter;
            gridView.CellMouseLeave += GridView_CellMouseLeave;
            gridView.CellMouseClick += GridView_CellMouseClick;
            gridView.MouseMove += GridView_MouseMove;
            gridView.SelectionChanged += GridView_SelectionChanged;

            for (int r = 0; r < gridView.RowCount; r++)
            {
                for (int c = 0; c < gridView.ColumnCount; c++)
                {
                    UpdateCell(GetCell(r, c), Color.White, States.Space);
                }
            }
        }

        #endregion Construction and initialization

        #region Event Handlers

        private void StateTypeComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }

            var item = stateTypeComboBox.Items[e.Index] as ComboBoxItemWithColor; 

            var isItemSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            e.Graphics.FillRectangle(new SolidBrush(isItemSelected ? SystemColors.ActiveCaption : stateTypeComboBox.BackColor), e.Bounds);
            e.Graphics.FillRectangle(new SolidBrush(item.Color), new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, 30, e.Bounds.Height - 2));
            e.Graphics.DrawString(item.Text, stateTypeComboBox.Font, new SolidBrush(Color.Black), 36, e.Bounds.Top + 1);
        }

        private void GridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = GetCell(e.RowIndex, e.ColumnIndex);
        }

        private void GridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            currentCell = null;
        }

        private void GridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                UpdateCell(currentCell);
            }
        }

        private void GridView_MouseMove(object sender, MouseEventArgs e)
        {
            // currentCell can be null when moving the mouse over the part of gridView not filled with cells
            if (currentCell != null && e.Button == MouseButtons.Left)
            {
                UpdateCell(currentCell);
            }
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            gridView.ClearSelection();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetValues();
        }
        
        private void RunButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < iterationCountNumericUpDown.Value; i++)
            {
                RunIteration(states, actions);
                CurrentIteration++;
            }
        }

        private void singleStepButton_Click(object sender, EventArgs e)
        {
            RunIteration(states, actions);
            CurrentIteration++;
        }

        #endregion Event Handlers

        private void ResetValues()
        {
            // start from 1 as we set start values (zeroes) for 0
            CurrentIteration = 1;

            for (int r = 0; r < gridView.RowCount; r++)
            {
                for (int c = 0; c < gridView.ColumnCount; c++)
                {
                    GetCell(r, c).Value = string.Empty;
                }
            }

            states = GetStatesFromGrid();
            actions = transitionModel.Select(t => t.TargetAction).ToArray();

            values = new List<float[][]>();

            // initialize the first values vector with zeroes
            var startValues = new float[states.Length][];
            for (int r = 0; r < states.Length; r++)
            {
                startValues[r] = new float[states[0].Length];
                for (int c = 0; c < startValues[r].Length; c++)
                {
                    var cellState = GetCellSate(r, c);
                    startValues[r][c] = cellState == States.Goal ? goalReward : cellState == States.Danger ? dangerReward : 0;
                }
            }
            values.Add(startValues);
        }

        private void RunIteration(States[][] states, Actions[] actions)
        {
            var valuesLayer = new float[states.Length][];
            var actionsLayer = new Actions[states.Length][];

            // calculate values for each state (a state is defined by a row&column pair)
            for (int r = 0; r < states.Length; r++)
            {
                valuesLayer[r] = new float[states[0].Length];
                actionsLayer[r] = new Actions[states[0].Length];

                for (int c = 0; c < states[r].Length; c++)
                {
                    var actionValues = new float[actions.Length];
                    var cellState = GetCellSate(r, c);

                    var maxActionValue = -999f;
                    var bestAction = Actions.None;

                    switch (cellState)
                    {
                        case States.Space:
                            // iterate through actions to calculate values and find the max one
                            for (int a = 0; a < actions.Length; a++)
                            {
                                actionValues[a] = CalculateQ(new State(r, c), actions[a]);

                                if (actionValues[a] > maxActionValue)
                                {
                                    maxActionValue = actionValues[a];
                                    bestAction = actions[a];
                                }
                            }
                            break;
                        case States.Goal:
                            maxActionValue = goalReward;
                            bestAction = Actions.Exit;
                            break;
                        case States.Danger:
                            maxActionValue = dangerReward;
                            bestAction = Actions.Exit;
                            break;
                        case States.Wall:
                        default:
                            maxActionValue = 0;
                            bestAction = Actions.None;
                            break;
                    }

                    valuesLayer[r][c] = maxActionValue;
                    actionsLayer[r][c] = bestAction;
                }
            }

            values.Add(valuesLayer);

            // this should be the optimal policy
            DisplayPolicy(states, valuesLayer, actionsLayer);
        }

        private float CalculateQ(State state, Actions action)
        {
            var q = 0f;
            var transition = transitionModel.First(t => t.TargetAction == action);

            foreach (var actionProbability in transition.ActionProbabilities)
            {
                var targetSate = GetTargetState(state, actionProbability.Action);

                var probability = actionProbability.Probability;
                var previousValue = values[CurrentIteration - 1][targetSate.Row][targetSate.Column];

                q += probability * (livingReward + discountFactor * previousValue);
            }

            return q;
        }

        private float GetReward(State state, Actions action, State targetSate)
        {
            var stateType = (States)GetCell(targetSate.Row,targetSate.Column).Tag;

            switch (stateType)
            {
                case States.Danger:
                    return dangerReward;
                case States.Goal:
                    return goalReward;
                default:
                    return livingReward;
            }
        }

        private State GetTargetState(State state, Actions action)
        {
            var gridCell = GetCell(state.Row,state.Column);

            bool canPerformAction = CanPerformAction(state, action);

            switch (action)
            {
                case Actions.Up:
                    return canPerformAction ? new State(state.Row - 1, state.Column) : state;
                case Actions.Right:
                    return canPerformAction ? new State(state.Row, state.Column + 1) : state;
                case Actions.Down:
                    return canPerformAction ? new State(state.Row + 1, state.Column) : state;
                case Actions.Left:
                    return canPerformAction ? new State(state.Row, state.Column - 1) : state;
            }

            // should never get here 
            throw new ArgumentException("Unknown action was provided");
        }

        // TODO: revise this (check if the method is even needed)
        public float GetProbability(State state, Actions action, State targetState)
        {
            var actionProbabilities = transitionModel.Find(t => t.TargetAction == action).ActionProbabilities;
            var actionToGetToState = GetRequiredAction(state, targetState);

            foreach (var actionProbability in actionProbabilities)
            {
                if (actionProbability.Action == actionToGetToState) return actionProbability.Probability;
            }

            return 0;
        }

        public Actions GetRequiredAction(State state, State targetState)
        {
            if (targetState.Column == state.Column && targetState.Row == state.Row - 1) return Actions.Up;
            if (targetState.Column == state.Column + 1 && targetState.Row == state.Row) return Actions.Right;
            if (targetState.Column == state.Column && targetState.Row == state.Row + 1) return Actions.Down;
            if (targetState.Column == state.Column - 1 && targetState.Row == state.Row) return Actions.Left;

            return Actions.None;
        }

        private bool CanPerformAction(State state, Actions action)
        {
            bool isPathBocked = false;

            switch (action)
            {
                case Actions.Up:
                    isPathBocked = state.Row == 0 || (States)GetCell(state.Row - 1, state.Column).Tag == States.Wall;
                    break;
                case Actions.Right:
                    isPathBocked = state.Column == gridView.ColumnCount - 1 || (States)GetCell(state.Row, state.Column + 1).Tag == States.Wall;
                    break;
                case Actions.Down:
                    isPathBocked = state.Row == gridView.RowCount - 1 || (States)GetCell(state.Row + 1, state.Column).Tag == States.Wall;
                    break;
                case Actions.Left:
                    isPathBocked = state.Column == 0 || (States)GetCell(state.Row, state.Column - 1).Tag == States.Wall;
                    break;
            }

            return !isPathBocked;
        }

        private Actions GetActualAction(Actions targetAction)
        {
            var randomValue = random.NextDouble();
            var transition = transitionModel.First(t => t.TargetAction == targetAction);

            if (randomValue < transition.ActionProbabilities[0].Probability) return transition.ActionProbabilities[0].Action;
            if (randomValue < transition.ActionProbabilities[0].Probability + transition.ActionProbabilities[1].Probability) return transition.ActionProbabilities[1].Action;
            return transition.ActionProbabilities[2].Action;
        }

        private States[][] GetStatesFromGrid()
        {
            var states = new States[gridView.RowCount][];

            for (int r = 0; r < gridView.RowCount; r++)
            {
                states[r] = new States[gridView.ColumnCount];
                for (int c = 0; c < gridView.ColumnCount; c++)
                {
                    states[r][c] = (States)GetCell(r, c).Tag;
                }
            }

            return states;
        }

        #region Helpers

        private void DisplayPolicy(States[][] states, float[][] valuesLayer, Actions[][] actionsLayer)
        {
            for (int r = 0; r < states.Length; r++)
            {
                for (int c = 0; c < states[r].Length; c++)
                {
                    GetCell(r, c).Value = $"{actionsLayer[r][c]} {Math.Round(valuesLayer[r][c], 2)}";
                }
            }
        }

        private void UpdateCell(DataGridViewCell cell)
        {
            var selectedItem = stateTypeComboBox.SelectedItem as ComboBoxItemWithColor;
            cell.Style.BackColor = selectedItem.Color;
            cell.Tag = selectedItem.State;
        }

        private void UpdateCell(DataGridViewCell cell, Color color, States state)
        {
            cell.Style.BackColor = color;
            cell.Tag = state;
        }

        private DataGridViewCell GetCell(int rowIndex, int columnIndex)
        {
            return gridView.Rows[rowIndex].Cells[columnIndex];
        }

        private States GetCellSate(int rowIndex, int columnIndex)
        {
            return (States)GetCell(rowIndex, columnIndex).Tag;
        }

        #endregion Helpers
    }
}
