using System.Collections.Generic;
using System.Linq;
using Godot;
using Prion;

namespace Osiris.DataClass;

public class TileGroupData : IDataClass
{
	public string DisplayName = "";
	public readonly PrionUBigInt Bitfield = new();
	public readonly List<Vector2I> Tiles = [];
	public TileGroupData(){}
	public TileGroupData(string displayName, PrionUBigInt bitfield, List<Vector2I> tiles)
	{
		DisplayName = displayName;
		Bitfield = bitfield;
		Tiles = tiles;
	}
	static bool IDataClass.TryFromNodeInternal<T>(PrionNode node, out T data)
	{
		data = default;
		if(!node.TryAs(out PrionDict dict)) return false;
		string name = dict.GetDefault("display_name?", "");
		if(!dict.TryGet("bitfield", out PrionUBigInt bitfield)) return false;
		if(!dict.TryGet("tiles", out PrionArray tileArray)) return false;
		List<Vector2I> tiles = new(tileArray.Array.Count);
		foreach (var tile in tileArray.Array)
		{
			if(!tile.TryAs(out PrionVector2I v)) return false;
			tiles.Add(new(v.X, v.Y));
		}
		TileGroupData tileGroup = new(name, bitfield, tiles);
		data = tileGroup as T;
		return true;
	}

	public PrionNode ToNode()
	{
		PrionDict dict = new();
		dict.Set("display_name?", DisplayName);
		dict.Set("bitfield", Bitfield);
		dict.Set("tiles", new PrionArray([..Tiles.Select(v => new PrionVector2I(v.X, v.Y))]));
		return dict;
	}
}
