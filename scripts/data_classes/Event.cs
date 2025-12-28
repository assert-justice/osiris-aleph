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
    public readonly string Name;
    public readonly DateTime Timestamp;
    public readonly PrionNode Payload;
    public Event(Guid userId, Guid targetId, string name, PrionNode payload)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TargetId = targetId;
        Name = name;
        Timestamp = DateTime.UtcNow;
        Payload = payload;
    }
    Event(Guid id, Guid userId, Guid targetId, string name, DateTime timestamp, PrionNode payload)
    {
        Id = id;
        UserId = userId;
        TargetId = targetId;
        Name = name;
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
        if(!dict.TryGet("payload", out PrionNode payload)) return false;
        eventObj = new(eventId, userId, targetId, name, new((long)ts), payload);
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("event_id", Id);
        dict.Set("user_id", UserId);
        dict.Set("target_id", TargetId);
        dict.Set("name", Name);
        dict.Set("timestamp", (ulong)Timestamp.Ticks);
        dict.Set("payload", Payload);
        return dict;
    }
}
