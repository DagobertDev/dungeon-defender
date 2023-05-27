using System;
using Godot;

namespace DungeonDefender.Components;

public partial class HealthWrapper : Node
{
	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	private int _maximumHealth;

	private IHealth _value;
	public IHealth Value => _value ?? throw new InvalidOperationException("Not initialized yet");

	public override void _Ready()
	{
		_value = new Health(_maximumHealth);
	}
}
