using Godot;

namespace DungeonDefender;

public partial class GoldLabel : Label
{
	[Export]
	private GoldComponent GoldComponent { get; set; }

	public override void _Ready()
	{
		Require.NotNull(GoldComponent);
		GoldComponent.CurrentGoldChanged += SetGold;
	}

	public void SetGold(int gold)
	{
		Text = $"Gold: {gold,-5}";
	}
}
