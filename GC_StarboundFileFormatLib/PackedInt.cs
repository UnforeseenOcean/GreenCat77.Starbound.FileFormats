using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace GC_StarboundFileFormatLib
{
    public class PackedInt
    {

        public ulong Value;

        public static PackedInt LoadFromBin(BinaryReader reader)
        {
            // return value
            ulong ret = 0;
            // temporary store data
            byte data = 0;
            do
            {
                // read 1 byte from the stream
                data = (byte)reader.ReadByte();
                ret <<= 7; // push data 7 bits to the left
                ret |= (uint)(data & 0x7f); // set 7 rightmost bits (01111111 or 0x7f, or 1 | 2 | 4 | 8 | 16 | 32 | 64)
            }
            // while the 8th bit is 1 (10000000, or 0x80)
            while ((data & 0x80) == 0x80);

            // obvious enough
            return new PackedInt { Value = ret };
        }

        public static void SaveToBin(PackedInt value, BinaryWriter writer)
        {
            // check where we should stop writing
            ulong position = 1;
            while (position < value.Value)
            {
                // write byte:
                writer.Write(
                    // first 7 bits
                    (byte)((value.Value & 0x7f)
                    // continuation bit
                    | (uint)(position < value.Value * 2 ? 0x80 : 0)));

                // pull data 7 bits to the left
                value.Value >>= 7;

                position *= 2;
            }
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            PackedInt value = new PackedInt() { Value = this.Value };

            ulong position = 1;
            while (position < value.Value)
            {
                result++;
                value.Value >>= 7;
                position *= 2;
            }

            return new PackedInt() { Value = result };
        }

    }
}
