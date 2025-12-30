using System;
using System.Collections.Generic;
using Prion.Node;

namespace Osiris.DataClass;

public class BlobData(Guid id)
{
    public enum SecurityStatus
    {
        None, // The blob is not listed or visible.
        Visible, // As above, plus the private data can be viewed
        Editable, // As above but the private data can be edited as well as viewed.
    }
    public readonly Guid Id = id;
    public HashSet<Guid> Editors = [];
    public HashSet<Guid> Viewers = [];
    public SecurityStatus Security = SecurityStatus.None;
    public PrionDict Data = new();
    Action<Event> EventHandler = e => {};
    public SecurityStatus GetSecurityStatus()
    {
        return Security;
    }
    public void SetSecurityStatus(SecurityStatus security)
    {
        if(!OsirisSystem.IsGm()) OsirisSystem.ReportError("Only GMs can set the security of blobs.");
        else Security = security;
    }
    public void AddEditor(Guid id)
    {
        if(!OsirisSystem.IsGm()) OsirisSystem.ReportError("Only GMs can add editors to blobs.");
        else Editors.Add(id);
    }
    public void BlockEditor(Guid id)
    {
        if(!OsirisSystem.IsGm()) OsirisSystem.ReportError("Only GMs can block editors from blobs.");
        else Editors.Remove(id);
    }
    public void AddViewer(Guid id)
    {
        if(!OsirisSystem.IsGm()) OsirisSystem.ReportError("Only GMs can add viewers to blobs.");
        else Viewers.Add(id);
    }
    public void BlockViewer(Guid id)
    {
        if(!OsirisSystem.IsGm()) OsirisSystem.ReportError("Only GMs can block viewers from blobs.");
        else Viewers.Remove(id);
    }
    public bool CanEdit()
    {
        if(OsirisSystem.IsGm()) return true;
        if(Security == SecurityStatus.Editable && OsirisSystem.IsPlayer()) return true;
        if(Editors.Contains(OsirisSystem.UserId)) return true;
        return false;
    }
    public bool CanView()
    {
        if(CanEdit()) return true;
        if(Security > SecurityStatus.None) return true;
        if(Viewers.Contains(OsirisSystem.UserId)) return true;
        return false;
    }
    public PrionDict GetData()
    {
        if (!CanView())
        {
            OsirisSystem.ReportError("Not authorized to read private data of this blob.");
            return default;
        }
        return Data;
    }
    public void SetData(PrionDict dict)
    {
        if (!CanEdit())
        {
            OsirisSystem.ReportError("Not authorized to write private data of this blob.");
            return;
        }
        Data = dict;
    }
    public virtual PrionDict ToNode()
    {
        PrionDict dict = new();
        dict.Set("id", Id);
        dict.Set("editors", Editors);
        dict.Set("viewers", Viewers);
        if(PrionEnum.TryFromOptions("none, partial, full, editable", (int)Security, out PrionEnum prionEnum, out string error))
        {
            dict.Set("security", prionEnum);
        }
        else OsirisSystem.ReportError(error);
        if(Data.Value.Count > 0) dict.Set("data?", Data);
        return dict;
    }
    public static bool TryFromNode(PrionNode prionNode, out BlobData data)
    {
        data = default;
        if(!prionNode.TryAs(out PrionDict dict)) return false;
        if(!dict.TryGet("id", out Guid id)) return false;
        data = new(id);
        if(!dict.TryGet("editors", out data.Editors)) return false;
        if(!dict.TryGet("viewers", out data.Viewers)) return false;
        if(!dict.TryGet("security", out PrionEnum prionEnum)) return false;
        data.Security = (SecurityStatus)prionEnum.Index;
        if(dict.TryGet("data?", out PrionDict prionDict)) data.Data = prionDict;
        return true;
    }
    public void ApplyEvent(Event eventObj)
    {
        EventHandler(eventObj);
    }
    public void SetEventHandler(Action<Event> action)
    {
        EventHandler = action;
    }
}
