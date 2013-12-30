using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Provides methods for saving/loading entire files.
    /// </summary>
    public class MainIO
    {

        public static PlayerFile LoadPlayerFile(string file)
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(file));
            PlayerFile result = PlayerFile.LoadFromBin(reader);
            reader.Close();
            return result;
        }

        public static void SavePlayerFile(PlayerFile file, string path)
        {
            BinaryWriter writer = new BinaryWriter(File.OpenWrite(path));
            PlayerFile.SaveToBin(file, writer);
            writer.Close();
        }

    }
}
