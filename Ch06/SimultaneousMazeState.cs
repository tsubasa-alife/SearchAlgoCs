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
			_points = new List<List<int>>(Constants.H);
			for (int h = 0; h < Constants.H; h++)
			{
				_points.Add(new List<int>(new int[Constants.W]));
			}
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

		public bool IsDone()
		{
			return _turn == Constants.END_TURN;
		}
		
		public new string ToString()
		{
			var sb = new StringBuilder();
			// ターン表示
			sb.Append($"turn:\t{_turn}\n");
			// 得点表示
			for (int playerId = 0; playerId < 2; playerId++)
			{
				sb.Append($"score({playerId}):\t{_characters[playerId]._gameScore}\n");
			}
			// 盤面の表示
			for (int y = 0; y < Constants.H; y++)
			{
				for (int x = 0; x < Constants.W; x++)
				{
					bool isPlayer = false;

					for (int playerId = 0; playerId < 2; playerId++)
					{
						if (_characters[playerId]._y == y && _characters[playerId]._x == x)
						{
							isPlayer = true;
							var str = playerId == 0 ? "A" : "B";
							sb.Append(str); // note:盤面にBしかいない場合は重なっている
							break;
						}
					}

					if (!isPlayer)
					{
						if (_points[y][x] == 0)
						{
							sb.Append(" .");
						}
						else
						{
							sb.Append(_points[y][x]);
						}
					}
				}
				sb.Append("\n");
			}

			return sb.ToString();
		}

	}
}