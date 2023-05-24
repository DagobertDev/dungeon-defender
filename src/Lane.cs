using System;
using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender;

public partial class Lane : Line2D
{
	[Export]
	public Path2D Path { get; private set; }

	[Export]
	public Area2D EndArea { get; private set; }

	[Export]
	public Area2D Area { get; private set; }

	public event Action<IEnemy> EnemyReachedEnd;

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

	public void AddEnemy(Node enemy)
	{
		Path.AddChild(enemy);
	}

	public void OnObjectReachedEnd(Area2D obj)
	{
		var node = obj.GetParent();

		if (node is IEnemy enemy)
		{
			EnemyReachedEnd?.Invoke(enemy);
		}
	}
}
