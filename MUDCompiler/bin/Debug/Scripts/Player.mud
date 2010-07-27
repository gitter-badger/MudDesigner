public class Player : BaseCharacter
{
	public Game _Game;

	public Player(Game game) : base(game)
	{
		_Game = game;
	}
}