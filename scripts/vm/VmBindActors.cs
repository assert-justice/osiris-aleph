using System;
using System.Linq;
using Osiris.DataClass;

namespace Osiris.Scripting;

public static class VmBindActors
{
    public static void Bind(VmObject module)
    {
        VmObject actorModule = new(module.Engine, "Actor");
        actorModule.AddObject("listActors", new Func<ActorData[]>(ListActors));
        module.AddVmObject(actorModule);
    }
    public static ActorData[] ListActors()
    {
        return [..SessionData.Session.Actors.Values];
    }
}
