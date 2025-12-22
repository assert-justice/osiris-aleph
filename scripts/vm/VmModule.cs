using System;
using Jint;
using Jint.Native.Object;

namespace Osiris.Scripting;

public class VmModule(ObjectInstance obj)
{
    public ObjectInstance Object = obj;
    public bool TryCall(string functionName, object[] args = null)
    {
        try
		{
			if(args is null)Object.Get(functionName).Call();
			else OsirisSystem.ReportError("Calling with arguments not yet supported");
			return true;
		}
		catch (Exception e)
		{
			string message = e.ToString();
			OsirisSystem.ReportError(message);
			return false;
		}
    }
}