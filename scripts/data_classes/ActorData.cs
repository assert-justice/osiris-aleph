using System;
using System.Collections.Generic;
using System.Linq;
using Prion;

namespace Osiris
{
    public class ActorData : IBaseData
    {
        public readonly Guid Id;
        public string DisplayName = "[Mysterious Figure]";
        public HashSet<Guid> ControlledBy = [];
        public string PortraitFilename = "";
        public string TokenFilename = "";
        public PrionDict Stats = new();
        public string Description = "They are very mysterious.";

        public ActorData()
        {
            Id = Guid.NewGuid();
        }
        public ActorData(Guid guid)
        {
            Id = guid;
        }

        static bool IBaseData.TryFromNodeInternal<T>(PrionNode node, out T data)
        {
            data = default;
            if(!node.TryAs(out PrionDict prionDict)) return false;
            if(!prionDict.TryGet("actor_id", out Guid guid)) return false;
            ActorData actor = new(guid)
            {
                DisplayName = prionDict.GetDefault("display_name?", "[Mysterious Figure]"),
                PortraitFilename = prionDict.GetDefault("portrait_filename?", ""),
                TokenFilename = prionDict.GetDefault("token_filename?", ""),
                Description = prionDict.GetDefault("description?", "They are very mysterious."),
            };
            if(!prionDict.TryGetGuidHashSet("controlled_by", out actor.ControlledBy)) return false;
            if(!prionDict.TryGet("stats", out actor.Stats)) return false;
            data = actor as T;
            return true;
        }
        public PrionNode ToNode()
        {
            PrionDict prionDict = new();
            prionDict.Set("actor_id", Id);
            prionDict.Set("display_name?", DisplayName);
            prionDict.Set("portrait_filename?", PortraitFilename);
            prionDict.Set("token_filename?", TokenFilename);
            prionDict.Set("description?", Description);
            prionDict.Dict["controlled_by"] = new PrionArray([.. ControlledBy.Select(o => new PrionGuid(o))]);
            prionDict.Dict["stats"] = Stats;
            return prionDict;
        }
    }
}