Player.GetCountOfCreatedPlayers();
Player p1 = new Player("Alex", 1);
Player.GetCountOfCreatedPlayers();
Player p2 = new Player("Max", 15);
Player p3 = new Player("Stas", 6);
Player.GetCountOfCreatedPlayers();
p1.GetInfo();
p2.GetInfo();
p3.GetInfo();
Console.WriteLine(GameUtils.Attack(15, 61));
Console.WriteLine(GameUtils.Attack(15, 12));
	
class Player
{
	// declaring fields and properties
	private string _name;

	public string Name
	{
		get { return _name; }
		set { _name = value; }
	}

	private int _level;

	public int Level
	{
		get { return _level; }
		set { _level = value; }
	}

	// counter of created players
	private static int _createdPlayers = 0;

    // method for getting count of created players
    public static void GetCountOfCreatedPlayers()
	{
		Console.WriteLine($"Count of created players: {_createdPlayers}");
	}
	// class constructor 
    public Player(string name, int level)
    {
        _name = name;
        _level = level;
        _createdPlayers++;
		Console.WriteLine("Player has been created.");		
    }

	// method for getting information about player
	public void GetInfo()
	{
		Console.WriteLine($"Nickname: {this.Name}, level: {Level}");
	}
}

static class GameUtils
{
    public static int Attack(int damage, int protection)
	{
		if ((damage - protection) >= 0)
		{
            Console.WriteLine($"Received damage: {damage-protection}");
			return damage - protection;
		}
		else
		{
			Console.WriteLine("Error! Defense can't exceed than damage!");
			return -1;
		}
	}
}