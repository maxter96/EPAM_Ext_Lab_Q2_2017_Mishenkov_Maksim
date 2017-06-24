namespace Task04
{
	using System;

	public abstract class MapObject
	{
		private int x;
		private int y;
		protected Map map;

		public MapObject(int x, int y, Map map)
		{
			this.x = x;
			this.y = y;
			this.map = map;
            IsAlive = true;
		}

		public int X
		{
			get { return x; }
		}

		public int Y
		{
			get { return y; }
		}

		public virtual void Kill()//todo pn "убей себя" чтоли (молодец, если так)? если нет, то входные параметры нужны
		{
            // да, вызов метода убивает этот объект
			throw new NotImplementedException();
		}

		public virtual void Collide(MapObject obj)
		{
			throw new NotImplementedException();
		}

        public bool IsAlive { get; set; }
	}
}
