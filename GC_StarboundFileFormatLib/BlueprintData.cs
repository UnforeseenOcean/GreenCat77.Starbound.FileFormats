using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GC_StarboundFileFormatLib
{
    /// <summary>
    /// INCOMPLETE
    /// </summary>
    public class BlueprintData
    {
        public List<Item> Blueprints = new List<Item>();

        public static void SaveToBin(BlueprintData item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(item.GetSizeOfData(), writer);
            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.Blueprints.Count }, writer);

            for (int i = 0; i < item.Blueprints.Count; i++)
            {
                Item.SaveToBin(item.Blueprints[i], writer);
            }
        }

        public static BlueprintData LoadFromBin(BinaryReader reader)
        {
            BlueprintData result = new BlueprintData();

            PackedInt dataLength = PackedInt.LoadFromBin(reader);
            PackedInt numBlueprints = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numBlueprints.Value; i++)
            {
                result.Blueprints.Add(Item.LoadFromBin(reader));
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += new PackedInt() { Value = (ulong)Blueprints.Count }.GetSizeOfData().Value;

            for (int i = 0; i < Blueprints.Count; i++)
            {
                result += Blueprints[i].GetSizeOfData().Value;
            }

            return new PackedInt() { Value = result };
        }
    }
}
