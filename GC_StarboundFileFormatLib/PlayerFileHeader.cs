using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GC_StarboundFileFormatLib
{
    public class PlayerFileHeader
    {
        public static readonly int SIGNATURE_LENGTH = 8;

        public static readonly byte[] PLAYER_SIGNATURE = new byte[8] 
        { 
            (byte)('S'), 
            (byte)('B'), 
            (byte)('P'), 
            (byte)('F'),
            (byte)('V'),
            (byte)('1'),
            (byte)('.'),
            (byte)('1')
        };


        public uint FileVersion;

        public static void SaveToBin(PlayerFileHeader item, BinaryWriter writer)
        {
            for (int i = 0; i < SIGNATURE_LENGTH; i++)
            {
                writer.Write(PLAYER_SIGNATURE[i]);
            }

            writer.Write(item.FileVersion);
        }

        public static PlayerFileHeader LoadFromBin(BinaryReader reader)
        {
            PlayerFileHeader result = new PlayerFileHeader();

            byte[] signature = new byte[SIGNATURE_LENGTH];

            for (int i = 0; i < SIGNATURE_LENGTH; i++)
            {
                signature[i] = reader.ReadByte();
                if (signature[i] != PLAYER_SIGNATURE[i])
                {
                    throw new Exception(string.Format("Invalid File Format Signature."));
                }
            }

            result.FileVersion = reader.ReadUInt32();

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)SIGNATURE_LENGTH;
            result += (ulong)FileVersion.GetSize();

            return new PackedInt() { Value = result };
        }

    }
}
