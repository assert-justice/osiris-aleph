
using Godot;

public static class Bitfield
{
    public static bool Get(ulong field, int position)
    {
        if(position < 0 || position >= 64)
        {
            GD.PrintErr("Out of bounds access on bitfield");
            return false;
        }
        // You can't shift a uint by a uint? Apparently?
        return (field & ((ulong)1 << position)) > 0;
    }
    public static ulong Set(ulong field, int position, bool value)
    {
        if(position < 0 && position >= 64)
        {
            GD.PrintErr("Out of bounds write on bitfield");
            return 0;
        }
        return value ? field | ((ulong)1 << position) : field;
    }
    public static ulong GetNumber(ulong field, int offset, int len)
    {
        if(offset < 0)
        {
            GD.PrintErr("Out of bounds offset on bitfield");
            return 0;
        }
        if(len < 1 || offset + len >= 64)
        {
            GD.PrintErr("Out of bounds length on bitfield");
            return 0;
        }
        ulong mask = ((ulong)1 << len) - 1;
        int shift_factor = 64 - len - offset;
        mask <<= shift_factor;
        ulong res = field & mask;
        return res << shift_factor;
    }
    public static ulong SetNumber(ulong field, int offset, int len, ulong value)
    {
        if(offset < 0)
        {
            GD.PrintErr("Out of bounds offset on bitfield");
            return 0;
        }
        if(len < 1 || offset + len >= 64)
        {
            GD.PrintErr("Out of bounds length on bitfield");
            return 0;
        }
        ulong mask = ((ulong)1 << len) - 1;
        if(value > mask)
        {
            GD.PrintErr($"Attempted to write {value} to a region of a bitfield that can only store numbers up to {mask}!");
            return 0;
        }
        int shift_factor = 64 - len - offset;
        // Clear the region
        mask <<= shift_factor;
        mask = ~mask;
        field &= mask;
        // Set the region and return
        return field | value << shift_factor;
    }
}