using System;
using System.Linq;
using Jint.Native;

namespace Osiris.Vm;

public static class VmBindLogging
{
    public static void Bind(VmObject module)
    {
        VmObject logging = new(module.Engine, "Logging");
        logging.AddObject("Log", new Action<JsValue[]>(Log));
        logging.AddObject("LogError", new Action<JsValue[]>(LogError));
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
