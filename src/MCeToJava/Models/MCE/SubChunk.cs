// <copyright file="SubChunk.cs" company="BitcoderCZ">
// Copyright (c) BitcoderCZ. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using BitcoderCZ.Maths.Vectors;

namespace MCeToJava.Models.MCE;

internal record SubChunk([property: JsonPropertyName("block_palette")] List<PaletteEntry> BlockPalette, int[] Blocks, int3 Position);
