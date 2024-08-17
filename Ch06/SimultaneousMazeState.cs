using System.Text;

namespace Ch06
{
	public class SimultaneousMazeState
	{
		private List<List<int>> _points;
		private int _turn;
		private List<Character> _characters;

		public SimultaneousMazeState(int seed)
		{
			_points = new List<List<int>>();
			_turn = 0;
			_characters = new List<Character>()
			{
				new Character(Constants.H / 2, (Constants.W / 2) - 1),
				new Character(Constants.H / 2, (Constants.W / 2) + 1)
			};

			var rand = new Random(seed);

			for (int y = 0; y < Constants.H; y++)
			{
				for (int x = 0; x < Constants.W; x++)
				{
					int ty = y;
					int tx = x;
					int point = rand.Next(0, 10);

					if (_characters[0]._y == y && _characters[0]._x == x)
					{
						continue;
					}

					if (_characters[1]._y == y && _characters[0]._x == x)
					{
						continue;
					}

					_points[ty][tx] = point;
					tx = Constants.W - 1 - x;
					_points[ty][tx] = point;
				}
			}
		}

		public void Advance(int actionA, int actionB)
		{
			{
				var character = _characters[0];
				character._x += Constants.dx[actionA];
				character._y += Constants.dy[actionA];
				var point = _points[character._y][character._x];
				if (point > 0)
				{
					character._gameScore += point;
				}
			}
			
			{
				var character = _characters[1];
				character._x += Constants.dx[actionB];
				character._y += Constants.dy[actionB];
				var point = _points[character._y][character._x];
				if (point > 0)
				{
					character._gameScore += point;
				}
			}

			foreach (var character in _characters)
			{
				_points[character._y][character._x] = 0;
			}

			_turn++;
		}
		
		public List<int> GetLegalActions(int playerId)
		{
			var actions = new List<int>();

			var character = _characters[playerId];
			for (int action = 0; action < 4; action++)
			{
				int ty = character._y + Constants.dy[action];
				int tx = character._x + Constants.dx[action];
				if (ty >= 0 && ty < Constants.H && tx >= 0 && tx < Constants.W)
				{
					actions.Add(action);
				}
			}

			return actions;
		}
		
		public new string ToString()
		{
			var sb = new StringBuilder();
			for (int y = 0; y < Constants.H; y++)
			{
				for (int x = 0; x < Constants.W; x++)
				{
					bool found = false;
					foreach (var character in _characters)
					{
						if (character._y == y && character._x == x)
						{
							sb.Append("C");
							found = true;
							break;
						}
					}

					if (!found)
					{
						sb.Append(_points[y][x]);
					}
				}
				sb.Append("\n");
			}

			return sb.ToString();
		}

	}
}