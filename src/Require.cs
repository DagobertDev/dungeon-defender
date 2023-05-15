using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DungeonDefender;

public static class Require
{
	public static void NotNull<T>(T argument, [CallerArgumentExpression(nameof(argument))] string parameterName = null)
	{
		ArgumentNullException.ThrowIfNull(argument, parameterName);
	}

	public static void MoreThanZero<T>(T argument,
		[CallerArgumentExpression(nameof(argument))] string parameterName = null) where T : INumber<T>
	{
		if (argument <= T.Zero)
		{
			throw new ArgumentOutOfRangeException(parameterName);
		}
	}

	public static void ZeroOrMore<T>(T argument,
		[CallerArgumentExpression(nameof(argument))] string parameterName = null) where T : INumber<T>
	{
		if (argument < T.Zero)
		{
			throw new ArgumentOutOfRangeException(parameterName);
		}
	}
}
