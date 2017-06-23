namespace Task04
{
	using System;

	public class Monster : MapObject, IMovable
	{
		private const int DefaultPower = 2;
		private const int DefaultHealth = 50;
		private const Direction DefaultDirection = Direction.None;
		private Direction direction;

		public Monster(int x, int y, Map map) : base(x, y, map)
		{
			Power = DefaultPower;
			Health = DefaultHealth;
			direction = DefaultDirection;
		}

		public int Power { get; set; }

		public int Health { get; set; }

		public void Move()
		{
            FindPathToPlayer();
            
            if (map[X,Y] != null)
            {
                Collide(map[X, Y]);
            }

            if (IsAlive)
            {
                // движение по направлению Direction
            }
        }

		protected virtual Direction FindPathToPlayer()
		{
			throw new NotImplementedException();
		}

		public override void Collide(MapObject obj)
		{
			throw new NotImplementedException();
		}
	}
}
