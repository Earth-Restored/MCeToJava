// <copyright file="ErrorCodeError.cs" company="BitcoderCZ">
// Copyright (c) BitcoderCZ. All rights reserved.
// </copyright>

using FluentResults;

namespace MCeToJava.Exceptions;

/// <summary>
/// Error class for <see cref="MCeToJava.ErrorCode"/>.
/// </summary>
public sealed class ErrorCodeError : Error
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ErrorCodeError"/> class.
	/// </summary>
	/// <param name="message">Error message.</param>
	/// <param name="errorCode">Error code.</param>
	public ErrorCodeError(string message, int errorCode)
		: base(message)
	{
		if (errorCode == MCeToJava.ErrorCode.Success)
		{
			throw new ArgumentException($"{nameof(errorCode)} cannot be {nameof(MCeToJava.ErrorCode)}.{nameof(MCeToJava.ErrorCode.Success)}.", nameof(errorCode));
		}

		ErrorCode = errorCode;
	}

	/// <summary>
	/// Gets the error code.
	/// </summary>
	/// <value>The error code, see: <see cref="MCeToJava.ErrorCode"/>.</value>
	public int ErrorCode { get; }
}
