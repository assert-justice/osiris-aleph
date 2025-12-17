using System;
using Jint.Native;

namespace Osiris.Vm;

public static class VmBindLogging
{
    public static void Bind(VmObject module)
    {
        VmObject logging = new(module.Engine, "Logging");
        logging.AddObject("Log", new Action<string>(OsirisSystem.Log));
        logging.AddObject("LogError", new Action<string>(OsirisSystem.ReportError));
        module.AddVmObject(logging);
    }
}
