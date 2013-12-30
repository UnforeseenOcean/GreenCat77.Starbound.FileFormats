using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    public static class ExtensionMethods
    {
        public static ulong GetSize(this uint v)
        {
            return sizeof(uint);
        }

        public static ulong GetSize(this ulong v)
        {
            return sizeof(ulong);
        }

        public static ulong GetSize(this byte v)
        {
            return sizeof(byte);
        }

        public static ulong GetSize(this int v)
        {
            return sizeof(int);
        }

        public static ulong GetSize(this bool v)
        {
            return sizeof(bool);
        }

        public static ulong GetSize(this float v)
        {
            return sizeof(float);
        }

        public static ulong GetSize(this double v)
        {
            return sizeof(double);
        }

        public static ulong GetSize(this string v)
        {
            ulong result = 0;
            result += new PackedInt() { Value = (ulong)v.Length }.Value;
            result += (ulong)System.Text.Encoding.UTF8.GetBytes(v).Length;
            return result;
        }

        public static ulong GetSize(this long v)
        {
            return sizeof(long);
        }

    }
}
