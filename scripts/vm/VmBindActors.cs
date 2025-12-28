using System;
using System.Collections.Generic;
using System.Linq;
using Jint.Native;
using Osiris.DataClass;
using Prion.Node;

namespace Osiris.Scripting;

class ActorDataWrapper(ActorData data) : VmDataWrapper<ActorData>(data)
{
    // string StatsString;
    // TODO: authenticate user has access before using methods? Sounds annoying and might not be needed.
    public Guid getId(){return Data.Id;}
    public string getName(){return Data.DisplayName;}
    public void setName(string name)
    {
        // if(!CanSet("bla")) return;
        if(!InEventHandler) OsirisSystem.ReportError("Cannot set name of actor outside of an event handler.");
        Data.DisplayName = name;
    }
    public string getPortraitFilename(){return Data.PortraitFilename;}
    public void setPortraitFilename(string name)
    {
        if(!InEventHandler) OsirisSystem.ReportError("Cannot set portrait of actor outside of an event handler.");
        else Data.PortraitFilename = name;
    }
    public string getTokenFilename(){return Data.TokenFilename;}
    public void setTokenFilename(string name)
    {
        if(!InEventHandler) OsirisSystem.ReportError("Cannot set token of actor outside of an event handler.");
        else Data.PortraitFilename = name;
    }
    // public JsObject getStats()
    // {
    //     return (JsObject)OsirisSystem.Vm.ParseJson(StatsString ??= Data.Stats.ToJson().ToJsonString());
    // }
    // public void setStats(JsObject jsObject)
    // {
    //     if(!InEventHandler) OsirisSystem.ReportError("Cannot set stats of actor outside of an event handler.");
    //     else
    //     {
    //         if(!PrionNode.TryFromJson(OsirisSystem.Vm.ToJsonString(jsObject), out PrionDict prionNode, out string error))
    //         {
    //             OsirisSystem.ReportError(error);
    //         }
    //         else Data.Stats = prionNode;
    //     }
    // }
    // public string getState(string key){return Data.State[key];}
    // public void setState(string key, string value)
    // {
    //     if(!InEventHandler) OsirisSystem.ReportError("Cannot set state of actor outside of an event handler.");
    //     else Data.State[key] = value;
    // }

    public override void applyEvent(JsValue payload)
    {
        ApplyEventInternal(Data.Id, "actor", payload);
    }
}

public static class VmBindActors
{
    public static void Bind(Vm vm, Dictionary<string, JsValue> module)
    {
        VmObject actorModule = new(vm);
        actorModule.AddObject("listActors", new Func<ActorDataWrapper[]>(ListActors));
        actorModule.AddObject("getActor", new Func<Guid,ActorDataWrapper>(GetActor));
        // actorModule.AddObject("setEventHandler", new Action<Action<ActorDataWrapper, JsObject>>(fn =>
        // {
        //     ActorDataWrapper.SetEventHandler((a,e) =>
        //     {
        //         fn(a as ActorDataWrapper, e.Payload.ToJsObject());
        //     });
        // }));
        module.Add("Actor", actorModule.ToJsObject());
    }
    static ActorDataWrapper[] ListActors()
    {
        if(OsirisSystem.IsGm()) return [..OsirisSystem.Session.Actors.Values.Select(a => new ActorDataWrapper(a))];
        return [..OsirisSystem.Session.Actors.Values.Select(a => new ActorDataWrapper(a))];
    }
    static ActorDataWrapper GetActor(Guid actorId)
    {
        ActorData actor = OsirisSystem.Session.Actors[actorId];
        if(actor is null) return null;
        if(OsirisSystem.IsGm()) return new(actor);
        return null;
    }
}
