using System;
using System.Collections.Generic;

namespace Osiris.DataClass;

public class BlobGroup<T> where T : BlobData
{
    Dictionary<string, T> Data = [];
    Action<T, Event> Action = (b, e)=>{};
}
