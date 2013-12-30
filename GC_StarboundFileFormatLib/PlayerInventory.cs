using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GC_StarboundFileFormatLib
{
    /// <summary>
    /// Stores the player's inventory.
    /// </summary>
    public class PlayerInventory
    {
        public static readonly int NUM_ITEM_BAGS = 5;

        public ulong Pixels;
        /// <summary>
        /// Length = 5. 
        /// { 
        ///     Main Inventory, 
        ///     Second Page of Inventory,
        ///     Hotbar,
        ///     Armor,
        ///     Tools (L/R on Hotbar)
        /// }
        /// </summary>
        public ItemBag[] ItemBags;
        public Item CurrentItem;
        public byte PrimaryHandSlotBag;
        public PackedInt PrimaryHandSlot;
        public byte AltHandSlotBag;
        public PackedInt AltHandSlot;


        public static void SaveToBin(PlayerInventory item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(item.GetSizeOfData(), writer);
            writer.Write(item.Pixels);

            for (int i = 0; i < NUM_ITEM_BAGS; i++)
            {
                ItemBag.SaveToBin(item.ItemBags[i], writer);
            }

            Item.SaveToBin(item.CurrentItem, writer);
            writer.Write(item.PrimaryHandSlotBag);
            PackedInt.SaveToBin(item.PrimaryHandSlot, writer);
            writer.Write(item.AltHandSlotBag);
            PackedInt.SaveToBin(item.AltHandSlot, writer);
        }

        public static PlayerInventory LoadFromBin(BinaryReader reader)
        {
            PlayerInventory result = new PlayerInventory();

            PackedInt dataLength = PackedInt.LoadFromBin(reader);
            result.Pixels = reader.ReadUInt64();

            result.ItemBags = new ItemBag[NUM_ITEM_BAGS];

            for (int i = 0; i < NUM_ITEM_BAGS; i++)
            {                
                result.ItemBags[i] = ItemBag.LoadFromBin(reader);
            }

            result.CurrentItem = Item.LoadFromBin(reader);
            result.PrimaryHandSlotBag = reader.ReadByte();
            result.PrimaryHandSlot = PackedInt.LoadFromBin(reader);
            result.AltHandSlotBag = reader.ReadByte();
            result.AltHandSlot = PackedInt.LoadFromBin(reader);

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)Pixels.GetSize();

            for (int i = 0; i < NUM_ITEM_BAGS; i++)
            {
                result += ItemBags[i].GetSizeOfData().Value;
            }

            result += CurrentItem.GetSizeOfData().Value;
            result += (ulong)PrimaryHandSlotBag.GetSize();
            result += PrimaryHandSlot.GetSizeOfData().Value;
            result += (ulong)AltHandSlotBag.GetSize();
            result += AltHandSlot.GetSizeOfData().Value;

            return new PackedInt() { Value = result };
        }
    }
}
