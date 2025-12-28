using System;
using System.Text.Json.Nodes;
using Jint.Native;
using Osiris.DataClass;
using Prion.Node;

namespace Osiris.Scripting;

public abstract class VmDataWrapper<T>(T data)
{
    protected T Data = data;
    // protected bool CanSet(string message)
    // {
    //     OsirisSystem.ReportError(message);
    //     return true;
    // }
    bool InHandler = false;
    protected bool InEventHandler
    {
        get => InHandler;
    }
    static Action<VmDataWrapper<T>, Event> EventHandlerInternal;
    protected static Action<VmDataWrapper<T>, Event> EventHandler
    {
        get => EventHandlerInternal;
    }
    public static void SetEventHandler(Action<VmDataWrapper<T>, Event> action)
    {
        // TODO: only allow if in "init" function?
        EventHandlerInternal = action;
    }
    protected void ApplyEventInternal(Guid targetId, string targetType, JsValue payloadJs)
    {
        var payloadJson = JsonNode.Parse(OsirisSystem.Vm.ToJsonString(payloadJs));
        if(!PrionNode.TryFromJson(payloadJson, out PrionNode payload, out string error))
        {
            OsirisSystem.ReportError(error);
            return;
        }
        Event e = new(OsirisSystem.UserId, targetId, targetType, payload);
        if(!OsirisSystem.Session.TryApplyEvent(e)) return; // TODO: log when this happens?
        InHandler = true;
        EventHandler(this, e);
        InHandler = false;
    }
    public abstract void applyEvent(JsValue payload);
}
