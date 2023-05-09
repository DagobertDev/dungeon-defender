using Godot;

namespace DungeonDefender;

public partial class PlaceTowerButton : BaseButton
{
	[Export]
	public PackedScene Tower { get; set; }

	[Export]
	public Texture2D Ghost { get; set; }

	[Export]
	public TowerBuilder TowerBuilder { get; set; }

	public override void _Pressed()
	{
		TowerBuilder.StartBuildMode(Tower, Ghost);
	}
}
