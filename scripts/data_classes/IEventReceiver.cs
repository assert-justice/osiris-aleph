using System;

namespace Osiris.DataClass;

public interface IEventReceiver<T> where T : class
{
    static Action<T, Event> EventHandler{get; set;}
    public static void SetEventHandler(Action<T, Event> eventHandler)
    {
        EventHandler = eventHandler;
    }
    public static void InvokeEvent(T target, Event eventObj)
    {
        if(EventHandler is not null) EventHandler(target, eventObj);
        else OsirisSystem.ReportError($"Attempted to invoke event on '{target.GetType()}', which has no event handler");
    }
}
