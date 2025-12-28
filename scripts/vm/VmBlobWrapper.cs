using System.Text.Json.Nodes;
using Jint.Native;
using Osiris.DataClass;
using Prion.Node;

namespace Osiris.Scripting;

public class VmBlobWrapper<T>(T data) where T : BlobData
{
    readonly T Data = data;
    string PrivateData_ = null;
    string PrivateData
    {
        get => PrivateData_ ??= Data.GetData().ToJson().ToJsonString();
        set {PrivateData_ = value;}
    }
    public bool canEdit() {return Data.CanEdit();}
    public bool canView() {return Data.CanView();}
    public JsValue getData()
    {
        if(!canView())
        {
            OsirisSystem.ReportError("Unauthorized access");
            return null;
        }
        return OsirisSystem.Vm.ParseJson(PrivateData);
    }
    public JsValue getPath(string path)
    {
        if(!canView())
        {
            OsirisSystem.ReportError("Unauthorized access");
            return null;
        }
        if(!Data.Data.TryGetPath(path, out PrionNode prionNode)) return null;
        return prionNode.ToJson().ToJsonString();
    }
    public void setData(JsValue jsValue)
    {
        if(!canEdit())
        {
            OsirisSystem.ReportError("Unauthorized access");
            return;
        }
        string str = OsirisSystem.Vm.ToJsonString(jsValue);
        var json = JsonNode.Parse(str);
        if(!PrionDict.TryFromJson(json, out PrionDict prionDict, out string error))
        {
            OsirisSystem.ReportError(error);
        }
        else
        {
            Data.Data = prionDict;
            PrivateData = null;
        }
    }
    public void setPath(string path, JsValue jsValue, bool canAdd = false, bool canChangeType = false)
    {
        if(!canEdit())
        {
            OsirisSystem.ReportError("Unauthorized access");
            return;
        }
        if(!PrionNode.TryFromJson(OsirisSystem.Vm.ToJsonString(jsValue), out PrionNode prionNode, out string error))
        {
            OsirisSystem.ReportError(error);
            return;
        }
        Data.Data.TrySetPath(path, prionNode, canAdd, canChangeType);
    }
}
