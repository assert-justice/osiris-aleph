using System;
using System.Collections.Generic;
using Jint;
using Jint.Native;
using Jint.Native.Json;
using Jint.Native.Object;

namespace Osiris.Scripting;
public class Vm
{
	readonly Engine EngineInternal;
	public Engine Engine
	{
		get => EngineInternal;
	}
	readonly JsonParser JsonParser;
	readonly JsonSerializer JsonSerializer;
	public Vm()
	{
		// Init Engine
		EngineInternal = new Engine(options =>
		{
			options.LimitMemory(4_000_000); // limit memory usage to 4 mb. pretty conservative, will likely need to go up
			options.TimeoutInterval(TimeSpan.FromMilliseconds(500)); // limit timeout to 500ms. seems reasonable
			options.Strict = true;
		});
		JsonParser = new(EngineInternal);
		JsonSerializer = new(EngineInternal);
		Dictionary<string, JsValue> osiris = [];
		VmBindLogging.Bind(this, osiris);
		VmBindActors.Bind(this, osiris);
		VmBindEvent.Bind(this, osiris);
		AddModule("Osiris", osiris);
	}
	public JsValue ParseJson(string jsonString)
	{
		return JsonParser.Parse(jsonString);
	}
	public string ToJsonString(JsValue jsValue)
	{
		return JsonSerializer.Serialize(jsValue).ToString();
	}
	void AddModule(string name, Dictionary<string, JsValue> module)
	{
		try
		{
			EngineInternal.Modules.Add(name, mb =>
			{
				foreach (var (n, mod) in module)
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
			EngineInternal.Modules.Add(moduleName, src);
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
			vmModule = new(EngineInternal.Modules.Import(name));
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
