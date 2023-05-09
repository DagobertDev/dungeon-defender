using Godot;

namespace DungeonDefender;

public partial class TowerBuilder : Sprite2D
{
	private PackedScene _towerScene;

	[Export]
	public Node TowerParent { get; set; }

	public void StartBuildMode(PackedScene towerScene, Texture2D ghostTexture)
	{
		Texture = ghostTexture;
		_towerScene = towerScene;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed(InputAction.MouseclickLeft))
		{
			var tower = _towerScene.Instantiate<Tower>();
			tower.GlobalPosition = GlobalPosition;
			TowerParent.AddChild(tower);
			Texture = null;
		}
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();
	}
}
