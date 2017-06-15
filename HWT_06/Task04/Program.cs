namespace Task04
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Map map = MapLoader.LoadMap("level1");
			Game game = new Game(map);
		}
	}
}
