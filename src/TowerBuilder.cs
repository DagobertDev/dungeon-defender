using Godot;

namespace DungeonDefender;

public partial class TowerBuilder : Sprite2D
{
	private PackedScene _towerScene;

	[Export]
	public Node TowerParent { get; set; }

	public override void _Ready()
	{
		SetEnabled(false);
	}

	public void StartBuildMode(PackedScene towerScene, Texture2D ghostTexture)
	{
		Texture = ghostTexture;
		_towerScene = towerScene;
		SetEnabled(true);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed(InputAction.MouseclickLeft))
		{
			var tower = _towerScene.Instantiate<Tower>();
			tower.GlobalPosition = GlobalPosition;
			TowerParent.AddChild(tower);
			Texture = null;
			SetEnabled(false);
		}
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();
	}

	private void SetEnabled(bool enabled)
	{
		SetProcess(enabled);
		SetProcessUnhandledInput(enabled);
	}
}
