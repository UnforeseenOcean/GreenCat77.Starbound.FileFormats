using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GC_StarboundFileFormatLib
{
    /// <summary>
    /// Holds the equipped armor of a character.
    /// </summary>
    public class Armor
    {
        public Item Head;
        public Item Chest;
        public Item Legs;
        public Item Back;
        public Item VanityHead;
        public Item VanityChest;
        public Item VanityLegs;
        public Item VanityBack;

        public static void SaveToBin(Armor item, BinaryWriter writer)
        {
            Item.SaveToBin(item.Head, writer);
            Item.SaveToBin(item.Chest, writer);
            Item.SaveToBin(item.Legs, writer);
            Item.SaveToBin(item.Back, writer);
            Item.SaveToBin(item.VanityHead, writer);
            Item.SaveToBin(item.VanityChest, writer);
            Item.SaveToBin(item.VanityLegs, writer);
            Item.SaveToBin(item.VanityBack, writer);
        }

        public static Armor LoadFromBin(BinaryReader reader)
        {
            Armor result = new Armor();

            result.Head = Item.LoadFromBin(reader);
            result.Chest = Item.LoadFromBin(reader);
            result.Legs = Item.LoadFromBin(reader);
            result.Back = Item.LoadFromBin(reader);
            result.VanityHead = Item.LoadFromBin(reader);
            result.VanityChest = Item.LoadFromBin(reader);
            result.VanityLegs = Item.LoadFromBin(reader);
            result.VanityBack = Item.LoadFromBin(reader);

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += Head.GetSizeOfData().Value;
            result += Chest.GetSizeOfData().Value;
            result += Legs.GetSizeOfData().Value;
            result += Back.GetSizeOfData().Value;
            result += VanityHead.GetSizeOfData().Value;
            result += VanityChest.GetSizeOfData().Value;
            result += VanityLegs.GetSizeOfData().Value;
            result += VanityBack.GetSizeOfData().Value;

            return new PackedInt() { Value = result };
        }
    }
}
