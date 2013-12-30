using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Stores a section of inventory
    /// </summary>
    public class ItemBag
    {
        /// <summary>
        /// Pre: PackedInt - Count
        /// Length = NumItems
        /// </summary>
        public List<Item> Items = new List<Item>();

        public static void SaveToBin(ItemBag item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.Items.Count }, writer);
            for (int i = 0; i < item.Items.Count; i++)
            {
                Item.SaveToBin(item.Items[i], writer);
            }
        }

        public static ItemBag LoadFromBin(BinaryReader reader)
        {
            ItemBag result = new ItemBag();

            PackedInt numItems = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numItems.Value; i++)
            {
                result.Items.Add(Item.LoadFromBin(reader));
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                result += Items[i].GetSizeOfData().Value;
            }

            return new PackedInt() { Value = result };
        }
    }
}
