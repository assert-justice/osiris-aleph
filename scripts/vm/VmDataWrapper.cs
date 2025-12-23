using System;
using Jint.Native;
using Osiris.DataClass;

namespace Osiris.Scripting;

public abstract class VmDataWrapper<T>(T data)
{
    protected static Action<VmDataWrapper<T>, Event> EventHandler;
    public static void SetEventHandler(Action<VmDataWrapper<T>, Event> action)
    {
        EventHandler = action;
    }
    protected T Data = data;
    public abstract void applyEvent(JsValue payload);
}
