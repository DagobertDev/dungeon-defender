using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender;

public partial class Lane : Line2D
{
	[Signal]
	public delegate void EnemyReachedEndEventHandler(Enemy enemy);

	[Export]
	public Path2D Path { get; private set; }

	[Export]
	public Area2D EndArea { get; private set; }

	[Export]
	public Area2D Area { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(Path);
		Require.NotNull(EndArea);
		Require.NotNull(Area);

		for (var i = 0; i < Points.Length - 1; i++)
		{
			var point = Points[i];
			var nextPoint = Points[i + 1];

			Path.Curve.AddPoint(point);

			var newShape = new CollisionShape2D();
			Area.AddChild(newShape);
			var rect = new RectangleShape2D();
			newShape.Position = (point + nextPoint) / 2;
			newShape.Rotation = point.AngleToPoint(nextPoint);
			var length = point.DistanceTo(nextPoint);
			rect.Size = new Vector2(length, Width);
			newShape.Shape = rect;
		}

		var lastPoint = Points[^1];
		Path.Curve.AddPoint(lastPoint);
		EndArea.Position = lastPoint;
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
