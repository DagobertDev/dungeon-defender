using System.Linq;
using Godot;

namespace DungeonDefender;

public partial class TowerBuilder : Area2D
{
	private PackedScene _towerScene;

	[Export]
	public Node TowerParent { get; private set; }

	[Export]
	public Sprite2D Sprite { get; private set; }

	[Export]
	public CollisionShape2D CollisionShape { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(TowerParent);
		Require.NotNull(Sprite);
		Require.NotNull(CollisionShape);
		SetEnabled(false);
	}

	public void StartBuildMode(PackedScene towerScene)
	{
		_towerScene = towerScene;
		var ghost = towerScene.Instantiate<Tower>();
		Sprite.Texture = ghost.Texture;
		Sprite.Transform = ghost.Transform;
		var ghostShape = ghost.CollisionBody.GetChildren().Cast<CollisionShape2D>().Single().Shape;
		CollisionShape.Shape = ghostShape;
		ghost.QueueFree();
		SetEnabled(true);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed(InputAction.MouseclickLeft) && CanBuild())
		{
			var tower = _towerScene.Instantiate<Tower>();
			tower.GlobalPosition = GlobalPosition;
			TowerParent.AddChild(tower);
			SetEnabled(false);
		}
		else if (@event.IsActionPressed(InputAction.MouseclickRight))
		{
			SetEnabled(false);
		}
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();
		Modulate = CanBuild() ? Colors.White : Colors.Red;
	}

	private void SetEnabled(bool enabled)
	{
		SetProcess(enabled);
		SetProcessUnhandledInput(enabled);

		if (!enabled)
		{
			Sprite.Texture = null;
		}
	}

	private bool CanBuild()
	{
		return !HasOverlappingAreas() && !HasOverlappingBodies();
	}
}
