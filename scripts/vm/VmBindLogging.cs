using System;
using System.Linq;
using Jint.Native;

namespace Osiris.Scripting;

public static class VmBindLogging
{
    public static void Bind(VmObject module)
    {
        VmObject logging = new(module.Engine, "Logging");
        logging.AddObject("log", new Action<JsValue[]>(Log));
        logging.AddObject("logError", new Action<JsValue[]>(LogError));
        module.AddVmObject(logging);
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
