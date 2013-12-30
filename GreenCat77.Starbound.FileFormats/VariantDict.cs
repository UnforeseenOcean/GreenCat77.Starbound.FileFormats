using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// A dictionary of variants...found in variants...cue the Inception Cat video.......
    /// </summary>
    public class VariantDict
    {
        /// <summary>
        /// Pre: 
        /// </summary>
        Dictionary<string, Variant> Dict = new Dictionary<string,Variant>();

        public static void SaveToBin(VariantDict item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.Dict.Count }, writer);

            foreach (KeyValuePair<string, Variant> kvp in item.Dict)
            {
                StringIO.SaveToBin(kvp.Key, writer);
                Variant.SaveToBin(kvp.Value, writer);
            }
        }

        public static VariantDict LoadFromBin(BinaryReader reader)
        {
            VariantDict result = new VariantDict();

            PackedInt numDictKeys = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numDictKeys.Value; i++)
            {
                string key = StringIO.LoadFromBin(reader);
                Variant variant = Variant.LoadFromBin(reader);
                result.Dict.Add(key, variant);
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += new PackedInt() { Value = (ulong)Dict.Count }.GetSizeOfData().Value;

            foreach (KeyValuePair<string, Variant> kvp in Dict)
            {
                result += (ulong)kvp.Key.GetSize();
                result += kvp.Value.GetSizeOfData().Value;
            }

            return new PackedInt() { Value = result };
        }

    }
}
