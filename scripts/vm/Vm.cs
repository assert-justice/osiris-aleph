using System;
using Jint;

namespace Osiris.Vm
{
	public partial class Vm
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
			VmObject osiris = new(JsEngine, "Osiris");
			VmBindLogging.Bind(osiris);
			AddModule(osiris);

			string exampleSrc = OsirisSystem.ReadFile("scripts/vm/js_scripts/example.js");
			JsEngine.Modules.Add("example", exampleSrc);
			var exampleMod = JsEngine.Modules.Import("example");
			// exampleMod.Get("hello").Call();
		}
		void AddModule(VmObject module)
		{
			JsEngine.Modules.Add(module.Name, mb =>
			{
				foreach (var (n, mod) in module.Children)
				{
					mb.ExportValue(n, mod);
				}
			});
		}
	}
}
