using System;
using Prion.Node;

namespace Osiris.DataClass;

public readonly struct Event
{
    public readonly Guid Id;
    public readonly string Name;
    public readonly DateTime Timestamp;
    public readonly PrionNode Payload;
    public readonly Action<PrionNode> Callback;
    public Event(string name, PrionNode payload, Action<PrionNode> callback = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Timestamp = DateTime.UtcNow;
        Payload = payload;
        Callback = callback ?? (p => {});
    }
    Event(Guid id, string name, DateTime timestamp, PrionNode payload, Action<PrionNode> callback = null)
    {
        Id = id;
        Name = name;
        Timestamp = timestamp;
        Payload = payload;
        Callback = callback ?? (p => {});
    }
    public static bool TryFromNode(PrionNode prionNode, out Event eventObj)
    {
        eventObj = default;
        if(!prionNode.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("event_id", out Guid id)) return false;
        if(!dict.TryGet("name", out string name)) return false;
        if(!dict.TryGet("timestamp", out ulong ts)) return false;
        if(!dict.TryGet("payload", out PrionNode payload)) return false;
        eventObj = new(id, name, new((long)ts), payload);
        return true;
    }
    public PrionNode ToNode()
    {
        PrionDict dict = new();
        dict.Set("event_id", Id);
        dict.Set("name", Name);
        dict.Set("timestamp", (ulong)Timestamp.Ticks);
        dict.Set("payload", Payload);
        return dict;
    }
}
