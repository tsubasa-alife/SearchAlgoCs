using Ch06;

Console.WriteLine("Hello, Ch06 !");

var state = new SimultaneousMazeState(Constants.SEED);
var battleAI = new BattleAI(Constants.SEED);
Console.WriteLine(state.ToString());

while (!state.IsDone())
{
	state.Advance(battleAI.randomAction(state, 0), battleAI.randomAction(state, 1));
	Console.WriteLine(state.ToString());
}

Console.WriteLine("Game End !!");