using System;
using System.Collections.Generic;
using System.Linq;
using Prion.Node;

namespace Osiris.DataClass;
public class ActorData(Guid id) : BlobData(id), ITryFromNode<ActorData>
{
    public string DisplayName = "[Mysterious Figure]";
    public string PortraitFilename = "";
    public string TokenFilename = "";
    public string Description = "They are very mysterious.";
    public static bool TryFromNode(PrionNode node, out ActorData data)
    {
        data = default;
        if(!TryFromNode(node, out BlobData blobData)) return false;
        data = blobData as ActorData;
        return true;
    }

    public override PrionDict ToNode()
    {
        PrionDict dict = base.ToNode();
        dict.Set("display_name?", DisplayName);
        dict.Set("portrait_filename?", PortraitFilename);
        dict.Set("token_filename?", TokenFilename);
        dict.Set("description?", Description);
        return dict;
    }
}
