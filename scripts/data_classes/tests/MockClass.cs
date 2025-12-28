using System;
using System.Collections.Generic;
using Godot;
using Prion.Node;

namespace Osiris.DataClass.Tests;

public static class MockClass
{
    public static ActorData MockActor()
    {
        ActorData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            PortraitFilename = MockData.GetRandomIdent(),
            TokenFilename = MockData.GetRandomIdent(),
            Description = MockData.GetRandomIdent()
        };
        int numEditors = MockData.Rng.Next(0, 3);
        for (int idx = 0; idx < numEditors; idx++)
        {
            data.Editors.Add(Guid.NewGuid());
        }
        int numViewers = MockData.Rng.Next(0, 10);
        for (int idx = 0; idx < numViewers; idx++)
        {
            data.Viewers.Add(Guid.NewGuid());
        }
        data.Security = (BlobData.SecurityStatus)MockData.Rng.Next(4);
        return data;
    }
    public static AssetLogData MockAssetLog()
    {
        int numOwners = MockData.Rng.Next(5, 10);
        List<Guid> owners = new(numOwners);
        for (int idx = 0; idx < numOwners; idx++)
        {
            owners.Add(Guid.NewGuid());
        }
        int numFiles = MockData.Rng.Next(10, 20);
        List<string> files = new(numFiles);
        for (int idx = 0; idx < numFiles; idx++)
        {
            files.Add(MockData.GetRandomIdent());
        }
        AssetLogData data = new();
        foreach (var file in files)
        {
            int numFileOwners = MockData.Rng.Next(1, numOwners);
            for (int idx = 0; idx < numFileOwners; idx++)
            {
                data.Add(file, MockData.GetRandomElement(owners));
            }
        }
        return data;
    }
    public static BlockerData MockBlocker()
    {
        BlockerData data = new(Guid.NewGuid())
        {
            Start = MockData.GetRandomVector2I(-100, 100, -100, 100),
            End = MockData.GetRandomVector2I(-100, 100, -100, 100),
            Status = (BlockerData.BlockerStatus)MockData.Rng.Next(4),
            IsOpaque = MockData.GetRandomBool(),
            IsSolid = MockData.GetRandomBool()
        };
        return data;
    }
    public static HandoutData MockHandout()
    {
        int numVisible = MockData.Rng.Next(5, 10);
        int numOwners = MockData.Rng.Next(4);
        HandoutData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            ImageFilename = MockData.GetRandomIdent(),
            Text = MockData.GetRandomText(1000),
            VisibleTo = MockData.GetRandomSet(Guid.NewGuid, numVisible),
            OwnedBy = MockData.GetRandomSet(Guid.NewGuid, numVisible),
            GMNotes = MockData.GetRandomText(1000),
        };
        return data;
    }
    public static LayerData MockLayer()
    {
        int numStamps = MockData.Rng.Next(20);
        LayerData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            IsVisible = MockData.GetRandomBool(),
            Stamps = MockData.GetRandomList(MockStampData, numStamps),
        };
        return data;
    }
    public static LightData MockLight()
    {
        LightData data = new(Guid.NewGuid())
        {
            Radius = MockData.GetRandomFloat(),
            Color = MockData.GetRandomColor()
        };
        return data;
    }
    public static MapData MockMap()
    {
        int numBlockers = MockData.Rng.Next(100, 200);
        int numLayers = MockData.Rng.Next(3, 16);
        int numTileGroups = MockData.Rng.Next(5, 20);
        Dictionary<Vector2I, ulong> tiles = [];
        for (int idx = 0; idx < numTileGroups; idx++)
        {
            ulong group = MockData.GetRandomUlong();
            int numTiles = MockData.Rng.Next(50);
            for (int f = 0; f < numTiles; f++)
            {
                tiles[MockData.GetRandomVector2I(-100, 100, -100, 100)] = group;
            }
        }
        MapData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            Size = MockData.GetRandomVector2I(0, 100, 0, 100),
            CellWidth = MockData.GetRandomFloat(32, 256),
            UsersPresent = MockData.GetRandomSet(Guid.NewGuid, MockData.Rng.Next(16)),
            LightingEnabled = MockData.GetRandomBool(),
            BackgroundColor = MockData.GetRandomColor(),
            BorderColor = MockData.GetRandomColor(),
            GridVisible = MockData.GetRandomBool(),
            GridColor = MockData.GetRandomColor(),
            GridLineWidth = MockData.GetRandomFloat(1, 16),
            Blockers = MockData.GetRandomList(MockBlocker, numBlockers),
            Tiles = tiles,
            Layers = MockData.GetRandomList(MockLayer, numLayers),
        };
        return data;
    }
    public static SessionData MockSession()
    {
        int numUsers = MockData.Rng.Next(20);
        int numActors = MockData.Rng.Next(20, 50);
        int numHandouts = MockData.Rng.Next(20);
        int numMaps = MockData.Rng.Next(3, 5);
        SessionData data = new()
        {
            AssetLog = MockAssetLog()
        };
        foreach (var item in MockData.GetRandomList(MockActor, numActors))
        {
            data.Actors.Add(item.Id, item);
        }
        foreach (var item in MockData.GetRandomList(MockHandout, numHandouts))
        {
            data.Handouts.Add(item.Id, item);
        }
        foreach (var item in MockData.GetRandomList(MockMap, numMaps))
        {
            data.Maps.Add(item.Id, item);
        }
        foreach (var item in MockData.GetRandomList(MockUser, numUsers))
        {
            data.Users.Add(item.Id, item);
        }
        return data;
    }
    public static StampData MockStampData()
    {
        var pos = MockData.GetRandomVector2I(-100, 100, -100, 100);
        var size = MockData.GetRandomVector2I(1, 5, 1, 5);
        bool hasImage = MockData.Rng.Next(2) == 0;
        bool hasText = MockData.Rng.Next(2) == 0;
        bool hasToken = MockData.Rng.Next(2) == 0;
        int numLights = MockData.Rng.Next(0, 10);
        StampDataImage image = hasImage ? MockStampDataImage() : null;
        StampDataText text = hasText ? MockStampDataText() : null;
        StampDataToken token = hasToken ? MockStampDataToken() : null;
        StampData data = new(Guid.NewGuid())
        {
            ControlledBy = MockData.GetRandomSet(Guid.NewGuid, MockData.Rng.Next(4)),
            Rect = new(pos, size),
            Angle = MockData.GetRandomFloat() * (float)Math.PI * 2,
            VisionRadius = MockData.GetRandomFloat(5, 50),
            HasVision = MockData.GetRandomBool(),
            Lights = MockData.GetRandomList(MockLight, numLights),
            ImageData = image,
            TextData = text,
            TokenData = token
        };
        return data;
    }
    static StampDataImage MockStampDataImage()
    {
        StampDataImage data = new()
        {
            ImageFilename = MockData.GetRandomIdent(),
            StretchMode = (StampDataImage.ImageStretchMode)MockData.Rng.Next(6)
        };
        return data;
    }
    static StampDataText MockStampDataText()
    {
        StampDataText data = new()
        {
            Text = MockData.GetRandomText(1000),
            FontFilename = MockData.GetRandomIdent(),
            FontSize = MockData.GetRandomIdent(),
            Margins = [
                MockData.GetRandomIdent(),
                MockData.GetRandomIdent(),
                MockData.GetRandomIdent(),
                MockData.GetRandomIdent(),
            ],
            WrapMode = (StampDataText.TextWrapMode)MockData.Rng.Next(4)
        };
        return data;
    }
    static StampDataToken MockStampDataToken()
    {
        bool hasStats = MockData.Rng.Next(2) == 0;
        PrionDict stats = hasStats ? new() : null;
        StampDataToken data = new()
        {
            ActorId = Guid.NewGuid(),
            Stats = stats
        };
        return data;
    }
    public static TileGroupData MockTileGroup()
    {
        int numTiles = MockData.Rng.Next(200);
        TileGroupData data = new()
        {
            Bitfield = MockData.GetRandomUlong(),
            Tiles = MockData.GetRandomList(() => MockData.GetRandomVector2I(-100, 100, -100, 100), numTiles)
        };
        return data;
    }
    public static UserData MockUser()
    {
        UserData data = new(Guid.NewGuid())
        {
            DisplayName = MockData.GetRandomIdent(),
            PfpFilename = MockData.GetRandomIdent()
        };
        return data;
    }
}
