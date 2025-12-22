using Jint.Native;

namespace Osiris.Scripting;

public class VmObject(Vm vm, JsObject jsObject = null)
{
    // public readonly string Name = name;
    public readonly Vm Vm = vm;
    public readonly JsObject Object = jsObject ?? new(vm.Engine);
    public void AddObject(string name, object obj)
    {
        var jsValue = JsValue.FromObject(Vm.Engine, obj);
        Object.Set(name, jsValue);
    }
    public void AddValue(string name, JsValue value)
    {
        Object.Set(name, value);
    }
    public JsObject ToJsObject()
    {
        return Object;
    }
    public JsValue ToJsValue()
    {
        return Object;
    }
    public string ToJsonString()
    {
        return Vm.ToJsonString(ToJsObject());
    }
}
