using Godot;

namespace DungeonDefender;

public partial class PlaceTowerButton : BaseButton
{
	[Export]
	public PackedScene Tower { get; private set; }

	[Export]
	public TowerBuilder TowerBuilder { get; private set; }

	[Export]
	private GoldComponent GoldComponent { get; set; }

	public override void _Ready()
	{
		Require.NotNull(Tower);
		Require.NotNull(TowerBuilder);
		Require.NotNull(GoldComponent);
		GoldComponent.CurrentGoldChanged += OnCurrentGoldChanged;
	}

	public override void _Pressed()
	{
		TowerBuilder.StartBuildMode(Tower);
	}

	private void OnCurrentGoldChanged(int amount)
	{
		Disabled = !TowerBuilder.CanBuildTower(Tower);
	}
}
