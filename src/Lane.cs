using Godot;

namespace DungeonDefender;

public partial class Lane : Line2D
{
	[Signal]
	public delegate void EnemyReachedEndEventHandler(Enemy enemy);

	[Export]
	public Path2D Path { get; set; }

	[Export]
	public Area2D EndArea { get; set; }

	public override void _Ready()
	{
		foreach (var point in Points)
		{
			Path.Curve.AddPoint(point);
		}

		EndArea.Position = Points[^1];
	}

	public void AddEnemy(Enemy enemy)
	{
		Path.AddChild(enemy);
	}

	public void OnObjectReachedEnd(Area2D obj)
	{
		if (obj.GetParent() is Enemy enemy)
		{
			EmitSignal(SignalName.EnemyReachedEnd, enemy);
		}
	}
}
