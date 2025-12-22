using System;
using Osiris.Scripting;
using Prion.Node;

namespace Osiris.DataClass;

public class Event
{
    public readonly Guid Id;
    public readonly Guid TargetId;
    public readonly string TargetType;
    public readonly DateTime Timestamp;
    public readonly VmObject Payload;
    public Event(Guid targetId, string targetType, VmObject payload)
    {
        Id = Guid.NewGuid();
        TargetId = targetId;
        TargetType = targetType;
        Timestamp = DateTime.UtcNow;
        Payload = payload;
    }
    Event(Guid id, Guid targetId, string targetType, VmObject payload)
    {
        Id = id;
        TargetId = targetId;
        TargetType = targetType;
        Timestamp = DateTime.UtcNow;
        Payload = payload;
    }
    // public static bool TryFromNode(PrionNode prionNode, out Event eventObj)
    // {
    //     eventObj = default;
    //     if(!prionNode.TryAs(out PrionDict dict)) return false;
    //     if(!dict.TryGet("event_id", out Guid id)) return false;
    //     if(!dict.TryGet("name", out string name)) return false;
    //     if(!dict.TryGet("timestamp", out ulong ts)) return false;
    //     if(!dict.TryGet("payload", out PrionNode payloadNode)) return false;
    //     OsirisSystem.Vm.
    //     eventObj = new(id, name, new((long)ts), );
    //     return true;
    // }
    // public PrionNode ToNode()
    // {
    //     PrionDict dict = new();
    //     dict.Set("event_id", Id);
    //     dict.Set("name", Name);
    //     dict.Set("timestamp", (ulong)Timestamp.Ticks);
    //     dict.Set("payload", Payload);
    //     return dict;
    // }
}
