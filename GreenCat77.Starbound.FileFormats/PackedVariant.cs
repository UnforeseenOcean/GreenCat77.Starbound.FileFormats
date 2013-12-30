using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Packed Variants. i.e. VARIANTCEPTION
    /// </summary>
    public class PackedVariant
    {
        /// <summary>
        /// Length = NumVariants
        /// </summary>
        public List<Variant> Variants = new List<Variant>();

        public static void SaveToBin(PackedVariant item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.Variants.Count }, writer);

            for (int i = 0; i < item.Variants.Count; i++)
            {
                Variant.SaveToBin(item.Variants[i], writer);
            }
        }

        public static PackedVariant LoadFromBin(BinaryReader reader)
        {
            PackedVariant result = new PackedVariant();

            PackedInt numVariants = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numVariants.Value; i++)
            {
                result.Variants.Add(Variant.LoadFromBin(reader));
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            for (int i = 0; i < Variants.Count; i++)
            {
                result += Variants[i].GetSizeOfData().Value;
            }

            return new PackedInt() { Value = result };
        }
    }
}
