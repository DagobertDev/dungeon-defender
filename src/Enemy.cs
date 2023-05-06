using System;
using Godot;

namespace DungeonDefender;

public partial class Enemy : PathFollow2D
{
	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public float Speed { get; set; }

	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;

		if (Math.Abs(ProgressRatio - 1f) < float.Epsilon)
		{
			QueueFree();
		}
	}
}
