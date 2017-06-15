namespace Task04
{
	public class Map
	{
		private MapObject[,] objects;
		private int width;
		private int height;

		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
			objects = new MapObject[width, height];
		}

		public Map(MapObject[,] objects)
		{
			this.objects = objects;
			this.width = objects.GetLength(1);
			this.height = objects.GetLength(0);
		}

		public int Width
		{
			get { return width; }
		}

		public int Height
		{
			get { return height; }
		}

		public MapObject this[int i, int j]
		{
			get { return objects[i, j]; }

			set { objects[i, j] = value; }
		}
	}
}
