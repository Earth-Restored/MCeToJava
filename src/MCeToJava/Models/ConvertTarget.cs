// <copyright file="ConvertTarget.cs" company="BitcoderCZ">
// Copyright (c) BitcoderCZ. All rights reserved.
// </copyright>

namespace MCeToJava.Models;

/// <summary>
/// Target format of the conversion.
/// </summary>
public enum ConvertTarget
{
	/// <summary>
	/// Java worls.
	/// </summary>
	/// <remarks>
	/// Includes level.dat.
	/// </remarks>
	Java,

	/// <summary>
	/// Vienna buildplate.
	/// </summary>
	/// <remarks>
	/// Includes buildplate_metadata.json.
	/// </remarks>
	Vienna,
}
