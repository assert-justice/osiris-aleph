using System;
using System.Text.Json.Nodes;
using Osiris.Scripting;
using Prion.Node;

namespace Osiris.DataClass;

public class Event
{
    public readonly Guid Id;
    public readonly Guid UserId;
    public readonly Guid TargetId;
    public readonly string TargetType;
    public readonly DateTime Timestamp;
    public readonly VmObject Payload;
    public Event(Guid userId, Guid targetId, string targetType, VmObject payload)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TargetId = targetId;
        TargetType = targetType;
        Timestamp = DateTime.UtcNow;
        Payload = payload;
    }
    Event(Guid id, Guid userId, Guid targetId, string targetType, DateTime timestamp, VmObject payload)
    {
        Id = id;
        UserId = userId;
        TargetId = targetId;
        TargetType = targetType;
        Timestamp = timestamp;
        Payload = payload;
    }
    public static bool TryFromNode(PrionNode prionNode, out Event eventObj)
    {
        eventObj = default;
        if(!prionNode.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("event_id", out Guid eventId)) return false;
        if(!dict.TryGet("user_id", out Guid userId)) return false;
        if(!dict.TryGet("target_id", out Guid targetId)) return false;
        if(!dict.TryGet("name", out string name)) return false;
        if(!dict.TryGet("timestamp", out ulong ts)) return false;
        if(!dict.TryGet("payload", out PrionNode payloadNode)) return false;
        var payloadValue = OsirisSystem.Vm.ParseJson(payloadNode.ToJson().ToJsonString());
        VmObject payload = new (OsirisSystem.Vm, payloadValue);
        eventObj = new(eventId, userId, targetId, name, new((long)ts), payload);
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("event_id", Id);
        dict.Set("user_id", UserId);
        dict.Set("target_id", TargetId);
        dict.Set("timestamp", (ulong)Timestamp.Ticks);
        PrionNode.TryFromJson(JsonNode.Parse(Payload.ToJsonString()), out PrionNode payload, out string _);
        dict.Set("payload", payload);
        return dict;
    }
}
