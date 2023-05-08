using Godot;

namespace DungeonDefender;

public partial class Lane : Line2D
{
	[Export]
	public Path2D Path { get; set; }

	public override void _Ready()
	{
		foreach (var point in Points)
		{
			Path.Curve.AddPoint(point);
		}
	}

	public void AddEnemy(Enemy enemy)
	{
		Path.AddChild(enemy);
	}
}
