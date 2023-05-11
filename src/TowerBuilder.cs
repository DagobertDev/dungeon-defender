using Godot;

namespace DungeonDefender;

public partial class TowerBuilder : Area2D
{
	private PackedScene _towerScene;

	[Export]
	public Node TowerParent { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	public override void _Ready()
	{
		Require.NotNull(TowerParent);
		Require.NotNull(Sprite);
		SetEnabled(false);
	}

	public void StartBuildMode(PackedScene towerScene, Texture2D ghostTexture)
	{
		Sprite.Texture = ghostTexture;
		_towerScene = towerScene;
		SetEnabled(true);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed(InputAction.MouseclickLeft) && CanBuild())
		{
			var tower = _towerScene.Instantiate<Tower>();
			tower.GlobalPosition = GlobalPosition;
			TowerParent.AddChild(tower);
			Sprite.Texture = null;
			SetEnabled(false);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();

		Modulate = CanBuild() ? Colors.White : Colors.Red;
	}

	private void SetEnabled(bool enabled)
	{
		SetProcess(enabled);
		SetProcessUnhandledInput(enabled);
	}

	private bool CanBuild()
	{
		return !HasOverlappingAreas() && !HasOverlappingBodies();
	}
}
