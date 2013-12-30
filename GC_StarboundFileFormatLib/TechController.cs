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
    public class TechController
    {
        /// <summary>
        /// Pre: PackedInt - Count
        /// Length = NumTech
        /// </summary>
        public List<string> Tech = new List<string>();
        /// <summary>
        /// Length = NumTech
        /// </summary>
        public byte[] UNK_ARR_1;

        public static void SaveToBin(TechController item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(item.GetSizeOfData(), writer);
            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.Tech.Count }, writer);

            for (int i = 0; i < item.Tech.Count; i++)
            {
                StringIO.SaveToBin(item.Tech[i], writer);
            }

            for (int i = 0; i < item.Tech.Count; i++)
            {
                writer.Write(item.UNK_ARR_1[i]);
            }
        }

        public static TechController LoadFromBin(BinaryReader reader)
        {
            TechController result = new TechController();

            PackedInt sizeOfData = PackedInt.LoadFromBin(reader);
            long endPos = reader.BaseStream.Position + (long)sizeOfData.Value;

            PackedInt numTechs = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numTechs.Value; i++)
            {
                result.Tech.Add(StringIO.LoadFromBin(reader));
            }

            long numUnkBytes = endPos - reader.BaseStream.Position;

            result.UNK_ARR_1 = new byte[(int)numUnkBytes];

            for (long i = 0; i < numUnkBytes; i++)
            {
                result.UNK_ARR_1[i] = reader.ReadByte();
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            for (int i = 0; i < Tech.Count; i++)
            {
                result += (ulong)Tech[i].GetSize();
            }

            result += (ulong)UNK_ARR_1.Length;

            return new PackedInt() { Value = result };
        }

    }
}
