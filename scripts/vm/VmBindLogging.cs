using System;
using System.Collections.Generic;
using System.Linq;
using Jint.Native;

namespace Osiris.Scripting;

public static class VmBindLogging
{
    public static void Bind(Vm vm, Dictionary<string, JsValue> module)
    {
        VmObject logging = new(vm);
        logging.AddObject("log", new Action<JsValue[]>(Log));
        logging.AddObject("logError", new Action<JsValue[]>(LogError));
        module.Add("Logging", logging.ToJsObject());
    }
    public static void Log(params JsValue[] messages)
    {
        OsirisSystem.Log([.. messages.Select(m => m.ToString())]);
    }
    public static void LogError(params JsValue[] messages)
    {
        OsirisSystem.ReportError([.. messages.Select(m => m.ToString())]);
    }
}
