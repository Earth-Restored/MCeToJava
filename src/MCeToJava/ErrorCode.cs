// <copyright file="ErrorCode.cs" company="BitcoderCZ">
// Copyright (c) BitcoderCZ. All rights reserved.
// </copyright>

namespace MCeToJava;

/// <summary>
/// Conversion error codes.
/// </summary>
public static class ErrorCode
{
	/// <summary>
	/// The conversion was not succesfull.
	/// </summary>
	public const int Success = 0;

	/// <summary>
	/// Invalid cli arguments.
	/// </summary>
	public const int CliParseError = 1;

	/// <summary>
	/// Unknown error.
	/// </summary>
	public const int UnknownError = 2;

	/// <summary>
	/// A file was not found.
	/// </summary>
	public const int FileNotFound = 3;
}
