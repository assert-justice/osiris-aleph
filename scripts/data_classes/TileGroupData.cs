using System.Collections.Generic;
using System.Linq;
using Godot;
using Prion.Node;

namespace Osiris.DataClass;

public class TileGroupData : IDataClass<TileGroupData>
{
	public string DisplayName = "";
	public PrionUBigInt Bitfield = new();
	public List<Vector2I> Tiles = [];
	public TileGroupData(){}

	public static bool TryFromNode(PrionNode node, out TileGroupData data)
	{
		data = default;
		if(!node.TryAs(out PrionDict dict)) return false;
		data = new();
		if(dict.TryGet("display_name?", out string displayName)) data.DisplayName = displayName;
		if(!dict.TryGet("bitfield", out data.Bitfield)) return false;
		if(!dict.TryGet("tiles", out PrionArray tileArray)) return false;
		data.Tiles = new(tileArray.Value.Count);
		foreach (var tile in tileArray.Value)
		{
			if(!tile.TryAs(out PrionVector2I v)) return false;
			data.Tiles.Add(new(v.X, v.Y));
		}
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
