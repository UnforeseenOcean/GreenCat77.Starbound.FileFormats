using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// A class that simply saves/loads strings.
    /// And no, I DO NOT KNOW WHY I MADE THIS ITS OWN CLASS. >:I
    /// </summary>
    public class StringIO
    {

        public static string LoadFromBin(BinaryReader reader)
        {
            PackedInt length = PackedInt.LoadFromBin(reader);

            byte[] bytes = new byte[(int)length.Value];

            for (ulong i = 0; i < length.Value; i++)
            {
                bytes[i] = reader.ReadByte();
                
            }

            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static void SaveToBin(string text, BinaryWriter writer)
        {
            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)text.Length }, writer);
            writer.Write(System.Text.Encoding.UTF8.GetBytes(text));
        }

    }
}
