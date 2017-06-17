namespace Task04
{
	using System;
	using System.Collections.Generic;

	public class Game
	{
		private Map map;
		private List<MapObject> objects;
		private Player player;

		public Game(Map map)
		{
			this.map = map;
			objects = new List<MapObject>();
			for(int i = 0; i < map.Height; i++)
			{
				for (int j = 0; j < map.Width; j++)
				{
					if (map[i,j] is IMovable)
					{
						objects.Add(map[i, j]);
					}

					if (map[i,j] is Player)
					{
						player = (Player)map[i, j];
					}
				}
			}
		}

		public void SetPlayerDirection(Direction direction)
		{
			player.SetDirection(direction);
		}

		public void MoveObjects()
		{
			foreach (IMovable obj in objects)
            {
                obj.Move();
            }
		}

		private bool IsMapContainsPickUps()
		{
			throw new NotImplementedException();
		}

		public bool IsWinner
		{
            get { return IsMapContainsPickUps() && IsPlayerAlive; }
		}

		public bool IsPlayerAlive
		{
            get { return player.IsAlive; }
		}
	}
}
