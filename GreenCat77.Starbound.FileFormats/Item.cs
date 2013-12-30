using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Stores an item.
    /// </summary>
    public class Item
    {

        public string Name;
        /// <summary>
        /// Actually = Item Stack# + 1
        /// </summary>
        public PackedInt Stack;
        public Variant ItemParams;

        public static void SaveToBin(Item item, BinaryWriter writer)
        {
            StringIO.SaveToBin(item.Name, writer);
            PackedInt.SaveToBin(item.Stack, writer);
            Variant.SaveToBin(item.ItemParams, writer);
        }

        public static Item LoadFromBin(BinaryReader reader)
        {
            Item result = new Item();

            result.Name = StringIO.LoadFromBin(reader);
            result.Stack = PackedInt.LoadFromBin(reader);
            result.ItemParams = Variant.LoadFromBin(reader);

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)Name.GetSize();
            result += Stack.GetSizeOfData().Value;
            result += ItemParams.GetSizeOfData().Value;

            return new PackedInt() { Value = result };
        }

    }
}
