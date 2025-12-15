using System;
using Jint;
using Jint.Native;
using Jint.Runtime.Modules;

namespace Osiris.Vm
{
    public class Vm
    {
        readonly Jint.Engine JsEngine;
        public Vm()
        {
            // Init Engine
            // string path = ProjectSettings.GlobalizePath("res://scripts/vm/js_scripts");
            Options options = new();
            options.LimitMemory(4_000_000); // limit memory usage to 4 mb. pretty conservative, will likely need to go up
            options.TimeoutInterval(TimeSpan.FromMilliseconds(500)); // limit timeout to 500ms. seems reasonable
            options.Strict = true;
            // options.EnableModules(ProjectSettings.GlobalizePath("res://scripts/vm/js_scripts"));
            JsEngine = new Engine(options);

            BindOsirisModule();

            string exampleSrc = OsirisSystem.ReadFile("scripts/vm/js_scripts/example.js");
            JsEngine.Modules.Add("example", exampleSrc);
            var exampleMod = JsEngine.Modules.Import("example");
            exampleMod.Get("hello").Call();
        }
        void BindOsirisModule()
        {
            // JsEngine.SetValue("log", new Action<string>(OsirisSystem.Log));
            // JsEngine.Modules.
            var fn = JsValue.FromObject(JsEngine, new Action<string>(OsirisSystem.Log));
            // JsObject obj = new(JsEngine);
            // Module mod = ModuleFactory.BuildJsonModule(JsEngine, obj, "temp");
            // var ns = Module.GetModuleNamespace(mod);
            // ns.Set(new JsString("log"), fn, mod);
            // JsEngine.SetValue<JsValue>("temp", mod);
            JsEngine.Modules.Add("temp", mb =>
            {
                mb.ExportObject("log", new Action<string>(OsirisSystem.Log));
                mb.ExportObject("logErr", new Action<string>(OsirisSystem.ReportError));
            });
            // JsEngine.SetValue()
        }
    }
}
