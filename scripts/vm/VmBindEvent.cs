// using System;
// using System.Collections.Generic;
// using Jint.Native;
// using Osiris.DataClass;
// using Prion.Node;

// namespace Osiris.Scripting;

// public static class VmBindEvent
// {
//     public static void Bind(Vm vm, Dictionary<string, JsValue> module)
//     {
//         VmObject eventModule = new(vm);
//         Action<Guid,string,JsObject> dispatch = (id, type, obj) =>
//         {
//             DispatchEvent(id, type, new (vm, obj));
//         };
//         Action<string, Action<string, JsObject>> setEventHandler = SetEventHandler;
//         eventModule.AddObject("dispatchEvent", dispatch);
//         eventModule.AddObject("setEventHandler", setEventHandler);
//         // eventModule.AddObject("logError", new Action<JsValue[]>(LogError));
//         module.Add("Event", eventModule.ToJsObject());
//     }
//     public static void DispatchEvent(Guid targetId, string targetType, VmObject val)
//     {
//         Event e = new(targetId, targetType, val);
//         OsirisSystem.Session.DispatchEvent(targetId, targetType, e);
//     }
//     public static void SetEventHandler(string targetType, Action<string, JsObject> action)
//     {
//         switch (targetType)
//         {
//             case "actor":
//                 Action<ActorData, Event> actorFn = (a, e) =>
//                 {
//                     action(a.Id.ToString(), e.Payload.ToJsObject());
//                 };
//                 IEventReceiver<ActorData>.EventHandler = actorFn;
//                 break;
//             default:
//                 break;
//         }
//     }
// }
