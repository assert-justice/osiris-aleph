using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Godot;
using Osiris.DataClass;
using Osiris.Scripting;
using Prion.Node;
using Prion.Schema;

namespace Osiris;
public static class OsirisSystem
{
	static Func<string, string> FileReaderFn = filepath =>
	{
		using var file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
		if(file is null)
		{
			ReportError($"Could not open file at path '{filepath}'");
			return "";
		}
		return file.GetAsText();
	};
	static Action<string, string> FileWriterFn = (filepath, contents) =>
	{
		using var file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
		file.StoreString(contents);
	};
	static Func<string, bool> CheckFileExistsFn = FileAccess.FileExists;
	static Func<string, bool> CheckDirExistsFn = DirAccess.DirExistsAbsolute;
	static Func<string, string[]> DirListFilesFn = path =>
	{
		using var dir = DirAccess.Open(path);
		if(dir is null)
		{
			ReportError($"Failed to open directory at path '{path}'.");
			return [];
		}
		List<string> filenames = [];
		if (dir != null)
		{
			dir.ListDirBegin();
			string fileName = dir.GetNext();
			while (fileName != "")
			{
				if (!dir.CurrentIsDir())
				{
					filenames.Add(fileName);
				}
				fileName = dir.GetNext();
			}
		}
		return [..filenames];
	};
	// static readonly Dictionary<string, string> MockFilesystem = [];
	static readonly List<string> ErrorLog = [];
	static readonly List<string> MessageLog = [];
	static bool InTestMode = false;
	static Vm VmInternal;
	public static Vm Vm
	{
		get => VmInternal ??= new();
	}
	static SessionData SessionInternal;
	public static SessionData Session
	{
		get => SessionInternal ??= new();
	}
	static Guid CurrentUserId;
	public static Guid UserId
	{
		get => CurrentUserId;
		set{CurrentUserId = value;}
	}
	public static bool IsGm()
	{
		return Session.IsGm(UserId);
	}
	public static bool IsPlayer()
	{
		return Session.IsPlayer(UserId);
	}
	public static bool IsSpectator()
	{
		return Session.IsSpectator(UserId);
	}
	public static bool EmitEvent(Guid targetId, string name, PrionNode payload)
    {
        Event e = new(UserId, targetId, name, payload);
        return Session.TryApplyEvent(e);
    }

	public static void LoadAllSchemas()
	{
		(Type, string)[] schemaFiles = [
			(typeof(ActorData), "actor"),
			(typeof(AssetLogData), "asset_log"),
			(typeof(BlockerData), "blocker"),
			(typeof(HandoutData), "handout"),
			(typeof(LayerData), "layer"),
			(typeof(MapData), "map"),
			(typeof(SessionData), "session"),
			(typeof(StampData), "stamp"),
			(typeof(TileGroupData), "tile_group"),
			(typeof(UserData), "user"),
		];
		foreach (var (type, name) in schemaFiles)
		{
			AddSchema(type, name);
		}
	}
	public static bool TryLoadSchema(string path, out PrionSchema schema)
	{
		schema = default;
		if (!FileExists(path))
		{
			ReportError($"Invalid path '{path}'");
			return false;
		}
		string schemaSrc = ReadFile(path);
		var jsonNode = JsonNode.Parse(schemaSrc);
		if(!PrionNode.TryFromJson(jsonNode, out PrionNode prionNode, out string error))
		{
			ReportError($"Failed to parse json at path '{path}'. Error: {error}");
			return false;
		}
		if(!PrionSchema.TryFromPrionNode(prionNode, out schema, out error))
		{
			ReportError(error);
			return false;
		}
		return true;
	}
	static void AddSchema(Type type, string name)
	{
		string path = $"res://scripts/schemas/{name}_schema.json";
		if(!TryLoadSchema(path, out PrionSchema prionSchema)) return;
		PrionSchemaManager.RegisterSchema(prionSchema, type);
	}
	public static void EnterTestMode()
	{
		// MockFilesystem.Clear();
		ErrorLog.Clear();
		MessageLog.Clear();
		if(InTestMode) return;
		InTestMode = true;
		FileReaderFn = filepath =>
		{
			if(TestUtils.FileExists(ConvertPath(filepath))) return TestUtils.ReadFile(ConvertPath(filepath));
			// if(MockFilesystem.TryGetValue(filepath, out string contents)) return contents;
			return "";
		};
		FileWriterFn = (filepath, contents) =>
		{
			throw new NotImplementedException();
			// MockFilesystem.Add(filepath, contents);
		};
		CheckFileExistsFn = filepath =>
		{
			if(TestUtils.FileExists(ConvertPath(filepath))) return true;
			// if(MockFilesystem.ContainsKey(filepath)) return true;
			return false;
		};
		CheckDirExistsFn = path =>
		{
			if(TestUtils.DirExists(ConvertPath(path))) return true;
			return false;
		};
		DirListFilesFn = path =>
		{
			return TestUtils.DirGetFilenames(ConvertPath(path));
		};
		LoadAllSchemas();
	}
	public static string ConvertPath(string filepath)
	{
		if(filepath.StartsWith("res://")) filepath = filepath[6..];
		return filepath;
	}
	public static string ReadFile(string filename)
	{
		return FileReaderFn(filename);
	}
	public static void WriteFile(string filename, string contents)
	{
		FileWriterFn(filename, contents);
	}
	public static bool FileExists(string filename)
	{
		return CheckFileExistsFn(filename);
	}
	public static bool DirExists(string path)
	{
		return CheckDirExistsFn(path);
	}
	public static string[] DirListFiles(string path)
	{
		return DirListFilesFn(path);
	}
	public static void ReportError<T>(params T[] messages)
	{
		string message = "";
		foreach (var m in messages)
		{
			message += m;
		}
		ErrorLog.Add(message);
		if(!InTestMode) GD.PrintErr(message);
		// else Console.WriteLine(message);
	}
	public static bool HasErrors()
	{
		return ErrorLog.Count > 0;
	}
	public static string GetErrors()
	{
		return string.Join("\n", ErrorLog);
	}
	public static void Log<T>(params T[] messages)
	{
		string message = "";
		foreach (var m in messages)
		{
			message += m;
		}
		MessageLog.Add(message);
		if(!InTestMode) GD.Print(message);
		// else Console.WriteLine(message);
	}
	public static string GetLogs()
	{
		return string.Join("\n", ErrorLog);
	}
}
