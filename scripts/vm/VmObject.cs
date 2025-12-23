using Jint.Native;

namespace Osiris.Scripting;

public class VmObject
{
    public readonly Vm Vm;
    public readonly JsObject Object;
    public VmObject(Vm vm, JsObject jsObject = null)
    {
        Vm = vm;
        Object = jsObject ?? new(vm.Engine);
    }
    public VmObject(Vm vm, JsValue jsValue)
    {
        Vm = vm;
        Object = (JsObject)jsValue;
    }
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
