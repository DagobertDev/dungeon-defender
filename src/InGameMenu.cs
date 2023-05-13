using Godot;

namespace DungeonDefender;

public partial class InGameMenu : Control
{
	// Needs to be a string to avoid cyclic references.
	[Export(PropertyHint.File, "*.tscn")]
	public string StartMenu { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(StartMenu);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed(InputAction.Escape))
		{
			if (GetTree().Paused)
			{
				ContinueGame();
			}
			else
			{
				PauseGame();
			}
		}
	}

	public void PauseGame()
	{
		GetTree().Paused = true;
		Visible = true;
	}

	public void ContinueGame()
	{
		GetTree().Paused = false;
		Visible = false;
	}

	public void GoToStartMenu()
	{
		GetTree().ChangeSceneToFile(StartMenu);
		GetTree().Paused = false;
	}

	public void CloseGame()
	{
		GetTree().Quit();
	}
}
