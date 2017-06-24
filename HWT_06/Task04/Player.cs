namespace Task04
{
	using System;

	public class Player : MapObject, IMovable
	{
		private const int DefaultPower = 1;
		private const int DefaultHealth = 100;
		private const Direction DefaultDirection = Direction.None;
		private Direction direction;

		public Player(int x, int y, Map map) : base(x, y, map)
		{
			Power = DefaultPower;
			Health = DefaultHealth;
			direction = DefaultDirection;
		}

		public int Power { get; set; }

		public int Health { get; set; }

		public void SetDirection(Direction direction)
		{
			this.direction = direction;
		}

		public void Move()//todo pn хоть бы Direction в качестве входного передал
		{
            // движение будет происходить по направлению direction - поле класса
			throw new NotImplementedException();
		}

		public override void Collide(MapObject obj)
		{
			throw new NotImplementedException();
		}
	}
}
