namespace Task04
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Map map = MapLoader.LoadMap("level1");
			Game game = new Game(map);
            string message = "";

            while(game.IsPlayerAlive)
            {
                game.MoveObjects();
                if (game.IsWinner)
                {
                    message = "You are winner!";
                    break;
                }
            }

            if (!game.IsWinner)
            {
                message = "You are loser!";
            }
		}
	}
}
