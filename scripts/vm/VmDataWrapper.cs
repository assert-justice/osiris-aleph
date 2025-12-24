using System;
using Jint.Native;
using Osiris.DataClass;

namespace Osiris.Scripting;

public abstract class VmDataWrapper<T>(T data)
{
    protected T Data = data;
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
    protected void ApplyEventInternal(Guid targetId, string targetType, JsValue payload)
    {
        Event e = new(OsirisSystem.UserId, targetId, targetType, OsirisSystem.Vm.GetVmObject(payload));
        if(!OsirisSystem.Session.TryApplyEvent(e)) return; // TODO: log when this happens?
        InHandler = true;
        EventHandler(this, e);
        InHandler = false;
    }
    public abstract void applyEvent(JsValue payload);
}
