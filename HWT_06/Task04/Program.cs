namespace Task04
{
	public class Program
	{
		private static void Main(string[] args)

			Map map = MapLoader.LoadMap("level1");//todo pn задавать хардкодом имя уровня или карты (не до конца понял, что это) нехорошо. Лучше вынести в enum.
			Game game = new Game(map);//todo pn игра для запущенного приложения мб только одна, поэтому стоит использовать паттерн синглтон для класса Game 
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
