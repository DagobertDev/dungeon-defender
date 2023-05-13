using Godot;

namespace DungeonDefender;

public partial class PlaceTowerButton : BaseButton
{
	[Export]
	public PackedScene Tower { get; private set; }

	[Export]
	public TowerBuilder TowerBuilder { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(Tower);
		Require.NotNull(TowerBuilder);
	}

	public override void _Pressed()
	{
		TowerBuilder.StartBuildMode(Tower);
	}
}
