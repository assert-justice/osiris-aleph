using System;
using System.Collections.Generic;
using Jint;
using Jint.Native.Object;

namespace Osiris.Scripting;
public partial class Vm
{
	static Vm _Engine;
	public static Vm Engine
	{
		get => _Engine;
	}
	public static void InitEngine()
	{
		_Engine = new();
	}
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
	}
	void AddModule(VmObject module)
	{
		try
		{
			JsEngine.Modules.Add(module.Name, mb =>
			{
				foreach (var (n, mod) in module.Children)
				{
					mb.ExportValue(n, mod);
				}
			});
		}
		catch (Exception e)
		{
			string message = e.ToString();
			OsirisSystem.ReportError(message);
		}
	}
	public bool TryAddModule(string moduleName, string src)
	{
		try
		{
			JsEngine.Modules.Add(moduleName, src);
			return true;
		}
		catch (Exception e)
		{
			string message = e.ToString();
			OsirisSystem.ReportError(message);
			return false;
		}
	}
	public bool TryImportModule(string name, out VmModule vmModule)
	{
		vmModule = default;
		try
		{
			vmModule = new(JsEngine.Modules.Import(name));
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
