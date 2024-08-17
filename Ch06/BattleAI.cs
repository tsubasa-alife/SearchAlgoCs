namespace Ch06;

public class BattleAI
{
	private Random _rand;

	public BattleAI(int seed)
	{
		_rand = new Random(seed);
	}
	
	public int randomAction(SimultaneousMazeState state, int playerId)
	{
		var legalActions = state.GetLegalActions(playerId);
		return legalActions[_rand.Next() % legalActions.Count];
	}
}