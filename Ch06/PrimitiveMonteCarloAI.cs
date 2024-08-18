namespace Ch06;

public class PrimitiveMonteCarloAI
{
	private Random _rand;

	public PrimitiveMonteCarloAI(int seed)
	{
		_rand = new Random(seed);
	}

	public int GetPrimitiveMonteCarloAction(SimultaneousMazeState state, int playerId, int playOutNumber)
	{
		var myLegalActions = state.GetLegalActions(playerId);
		var oppLegalActions = state.GetLegalActions((playerId + 1) % 2);
		double bestValue = Double.NegativeInfinity;
		int bestActionIndex = -1;
		for (int i = 0; i < myLegalActions.Count; i++)
		{
			double value = 0;
			for (int j = 0; j < playOutNumber; j++)
			{
				SimultaneousMazeState nextState = state.DeepCopy();
				if (playerId == 0)
				{
					nextState.Advance(myLegalActions[i], oppLegalActions[_rand.Next() % oppLegalActions.Count]);
				}
				else
				{
					nextState.Advance(oppLegalActions[_rand.Next() % oppLegalActions.Count], myLegalActions[i]);
				}

				double playerAWinRate = PlayOut(nextState);
				double winRate = playerId == 0 ? playerAWinRate : 1.0 - playerAWinRate;
				value += winRate;
			}

			if (value > bestValue)
			{
				bestActionIndex = i;
				bestValue = value;
			}
		}

		return myLegalActions[bestActionIndex];
	}
	
	private int randomAction(SimultaneousMazeState state, int playerId)
	{
		var legalActions = state.GetLegalActions(playerId);
		return legalActions[_rand.Next() % legalActions.Count];
	}
	
	private double PlayOut(SimultaneousMazeState state)
	{
		switch (state.GetWinningStatus())
		{
			case WinningStatus.FIRST:
				return 1.0;
			case WinningStatus.SECOND:
				return 0.0;
			case WinningStatus.DRAW:
				return 0.5;
			default:
				var nextState = state.DeepCopy();
				nextState.Advance(randomAction(state, 0), randomAction(state, 1));
				return PlayOut(nextState);
		}
	}
	
}