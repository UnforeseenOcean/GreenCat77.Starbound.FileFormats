using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// The actual player file class, consisting of the header and player data.
    /// </summary>
    public class PlayerFile
    {

        public PlayerFileHeader Header;
        public Player Player;

        public static void SaveToBin(PlayerFile item, BinaryWriter writer)
        {
            PlayerFileHeader.SaveToBin(item.Header, writer);
            Player.SaveToBin(item.Player, writer);
        }

        public static PlayerFile LoadFromBin(BinaryReader reader)
        {
            PlayerFile result = new PlayerFile();

            result.Header = PlayerFileHeader.LoadFromBin(reader);
            result.Player = Player.LoadFromBin(reader);

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += Header.GetSizeOfData().Value;
            result += Player.GetSizeOfData().Value;

            return new PackedInt() { Value = result };
        }

    }
}
