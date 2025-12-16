using System;
using Jint;
using Jint.Native;

namespace Osiris.Vm
{
    public class Vm
    {
        readonly Engine JsEngine;
        public Vm()
        {
            // Init Engine
            JsEngine = new Engine(options =>
            {
                options.LimitMemory(4_000_000); // limit memory usage to 4 mb. pretty conservative, will likely need to go up
                options.TimeoutInterval(TimeSpan.FromMilliseconds(500)); // limit timeout to 500ms. seems reasonable
                options.Strict = true;
            });

            BindOsirisModule();

            string exampleSrc = OsirisSystem.ReadFile("scripts/vm/js_scripts/example.js");
            JsEngine.Modules.Add("example", exampleSrc);
            var exampleMod = JsEngine.Modules.Import("example");
            // exampleMod.Get("hello").Call();
        }
        void BindOsirisModule()
        {
            JsObject logging = new(JsEngine);
            var fn = JsValue.FromObject(JsEngine, new Action<string>(OsirisSystem.Log));
            logging.Set(new JsString("Log"), fn);
            fn = JsValue.FromObject(JsEngine, new Action<string>(OsirisSystem.ReportError));
            logging.Set(new JsString("LogError"), fn);
            JsEngine.Modules.Add("Osiris", mb =>
            {
                mb.ExportObject("Logging", logging);
            });
        }
    }
}
