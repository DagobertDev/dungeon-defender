using System;
using Godot;

namespace DungeonDefender;

public partial class StartMenu : Node
{
	[Export]
	public PackedScene LevelScene { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(LevelScene);
	}

	public void StartGame()
	{
		var result = GetTree().ChangeSceneToPacked(LevelScene);

		if (result != Error.Ok)
		{
			throw new InvalidOperationException($"Could not load scene '{LevelScene?.ResourcePath}'");
		}
	}

	public void ExitGame()
	{
		GetTree().Quit();
	}
}
