// <copyright file="ConvertDirCommand.cs" company="BitcoderCZ">
// Copyright (c) BitcoderCZ. All rights reserved.
// </copyright>

using CommandLineParser.Attributes;
using CommandLineParser.Commands;
using MCeToJava.Models;
using Serilog;
using Serilog.Core;

namespace MCeToJava.Cli.CliCommands;

[CommandName("convert-dir")]
[HelpText("Converts all Project Earth (Minecraft Earth) buildplates in a directory to a Java worlds.")]
internal sealed class ConvertDirCommand : ConsoleCommand
{
	[Required]
	[Argument("in-dir")]
	[HelpText("Path to the directory containing buildplate jsons, no files besides buildplates should be in this directory.")]
	public string InDir { get; set; } = string.Empty;

	[Argument("out-dir")]
	[HelpText("Path of the directory that the converted worlds will be placed into.")]
	public string OutDir { get; set; } = "converted";

	[Option("biome")]
	public string Biome { get; set; } = "minecraft:plains";

	[Option('t', "target")]
	[HelpText("The target to export to, additional files are generated depending on this setting.")]
	public ConvertTarget ConvertTarget { get; set; } = ConvertTarget.Vienna;

	[Option("world-name")]
	[HelpText("Name of the exported world.")]
	[DependsOn(nameof(ConvertTarget), ConvertTarget.Java)]
	public string WorldName { get; set; } = "Buildplate";

	public override int Run()
	{
		try
		{
			string[] files;
			try
			{
				files = Directory.GetFiles(InDir);
			}
			catch (Exception ex)
			{
				Log.Error($"Failed to get files: {ex}");
				return ErrorCode.UnknownError;
			}

			if (files.Length == 0)
			{
				Log.Information("No files in directory");
				return ErrorCode.Success;
			}

			var options = new Converter.Options(Logger.None, ConvertTarget, Biome, WorldName);

			return Converter.ConvertFiles(files, OutDir, options).ConfigureAwait(false).GetAwaiter().GetResult();
		}
		catch (Exception ex)
		{
			Log.Error($"Unhandled exception: {ex}");
			return ErrorCode.UnknownError;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}