using Godot;

namespace DungeonDefender.Player;

public partial class GoldComponent : Node
{
	[Signal]
	public delegate void CurrentGoldChangedEventHandler(int health);

	[Signal]
	public delegate void ZeroHealthReachedEventHandler();

	private int _currentGold;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	private int InitialGold { get; set; }

	public int CurrentGold
	{
		get => _currentGold;
		set
		{
			_currentGold = value;
			EmitSignal(SignalName.CurrentGoldChanged, CurrentGold);
		}
	}

	public override void _Ready()
	{
		Require.ZeroOrMore(InitialGold);
		CurrentGold = InitialGold;
	}
}
