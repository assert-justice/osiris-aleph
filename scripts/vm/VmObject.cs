using System.Collections.Generic;
using Jint;
using Jint.Native;

namespace Osiris.Vm;

public class VmObject(Engine engine, string name)
{
    public readonly string Name = name;
    public readonly Engine Engine = engine;
    public readonly Dictionary<string, JsValue> Children = [];
    public void AddObject(string name, object obj)
    {
        var jsValue = JsValue.FromObject(Engine, obj);
        Children[name] = jsValue;
    }
    public void AddValue(string name, JsValue value)
    {
        Children[name] = value;
    }
    public void AddVmObject(VmObject vmObject)
    {
        Children[vmObject.Name] = vmObject.ToJsObject();
    }
    public JsValue ToJsObject()
    {
        JsObject res = new(Engine);
        foreach (var (name, val) in Children)
        {
            res.Set(name, val);
        }
        return res;
    }
}
