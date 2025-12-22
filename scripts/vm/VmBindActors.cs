using System;
using System.Collections.Generic;
using System.Linq;
using Jint;
using Jint.Native;
using Osiris.DataClass;

namespace Osiris.Scripting;

public static class VmBindActors
{
    public static void Bind(Vm vm, Dictionary<string, JsValue> module)
    {
        VmObject actorModule = new(vm);
        actorModule.AddObject("listActors", new Func<ActorData[]>(ListActors));
        module.Add("Actor", actorModule.ToJsObject());
    }
    public static ActorData[] ListActors()
    {
        return [..OsirisSystem.Session.Actors.Values];
    }
}
