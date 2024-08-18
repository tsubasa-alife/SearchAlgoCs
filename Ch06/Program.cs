using Ch06;

Console.WriteLine("Hello, Ch06 !");

var state = new SimultaneousMazeState(Constants.SEED);
var battleAI = new BattleAI(Constants.SEED);
var primitiveMonteCarloAI = new PrimitiveMonteCarloAI(Constants.SEED);
Console.WriteLine(state.ToString());

while (!state.IsDone())
{
	state.Advance(primitiveMonteCarloAI.GetPrimitiveMonteCarloAction(state, 0, 500), battleAI.randomAction(state, 1));
	Console.WriteLine(state.ToString());
}

Console.WriteLine("Game End !!");