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
    public class Variant
    {

        public byte DataType;
        public object Data;

        public Type GetTypeOfData()
        {
            switch (DataType)
            {
                case 1: return typeof(object);
                case 2: return typeof(double);
                case 3: return typeof(bool);
                case 4: return typeof(PackedInt);
                case 5: return typeof(string);
                case 6: return typeof(PackedVariant);
                case 7: return typeof(VariantDict);
                default: return typeof(object);
            }
        }

        public byte[] GetBytes()
        {
            MemoryStream stream = new MemoryStream();

            BinaryWriter writer = new BinaryWriter(stream);

            switch (DataType)
            {
                case 2: writer.Write((double)(Data)); break;
                case 3: writer.Write((bool)(Data)); break;
                case 4: PackedInt.SaveToBin((PackedInt)Data, writer); break;
                case 5: writer.Write(Data.ToString()); break;
                case 6: PackedVariant.SaveToBin((PackedVariant)Data, writer); break;
                case 7: VariantDict.SaveToBin((VariantDict)Data, writer); break;
            }

            writer.Close();

            BinaryReader reader = new BinaryReader(stream);

            long length = stream.Length;
            byte[] result = reader.ReadBytes((int)length);

            return result;
        }

        public static void SaveToBin(Variant item, BinaryWriter writer)
        {
            Type type = item.GetTypeOfData();

            writer.Write(item.DataType);
            writer.Write(item.GetBytes());
        }

        public static Variant LoadFromBin(BinaryReader reader)
        {
            Variant result = new Variant();

            result.DataType = reader.ReadByte();

            switch (result.DataType)
            {
                case 2: result.Data = reader.ReadDouble(); break;
                case 3: result.Data = reader.ReadBoolean(); break;
                case 4: result.Data = PackedInt.LoadFromBin(reader); break;
                case 5: result.Data = StringIO.LoadFromBin(reader); break;
                case 6: result.Data = PackedVariant.LoadFromBin(reader); break;
                case 7: result.Data = VariantDict.LoadFromBin(reader); break;
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)DataType.GetSize();
            result += (ulong)GetBytes().Length;

            return new PackedInt() { Value = result };
        }

    }
}
