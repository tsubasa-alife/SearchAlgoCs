using Ch06;

Console.WriteLine("Hello, Ch06 !");
double firstPlayerWinRate = 0;
int gameNumber = 100;
for (int i = 0; i < gameNumber; i++)
{
	var state = new SimultaneousMazeState(Constants.SEED);
	var battleAI = new BattleAI(Constants.SEED);
	var primitiveMonteCarloAI = new PrimitiveMonteCarloAI(Constants.SEED);
	Console.WriteLine(state.ToString());

	while (!state.IsDone())
	{
		state.Advance(primitiveMonteCarloAI.GetPrimitiveMonteCarloAction(state, 0, 500), battleAI.randomAction(state, 1));
	}

	double winRatePoint = state.GetFirstPlayerScoreForWinRate();
	if (winRatePoint >= 0)
	{
		Console.WriteLine(state.ToString());
	}

	firstPlayerWinRate += winRatePoint;
	
	Console.WriteLine($"i {i} w {firstPlayerWinRate / (i + 1)}");
}

firstPlayerWinRate /= (double)gameNumber;
Console.WriteLine($"Winning rate of MonteCarlo to Random:\t{firstPlayerWinRate}");



Console.WriteLine("Game End !!");
