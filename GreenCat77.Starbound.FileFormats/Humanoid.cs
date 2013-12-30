using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Stores the basic attrbutes of a humanoid entity (i.e. Player)
    /// </summary>
    public class Humanoid
    {
        public string Name;
        public string Species;
        public byte Gender;
        public string HairGroup;
        public string HairType;
        public string HairDirectives;
        public string BodyDirectives;
        public string FacialHairGroup;
        public string FacialHairType;
        public string FacialHairDirectives;
        public string FacialMaskGroup;
        public string FacialMaskType;
        public string FacialMaskDirectives;
        public string Idle;
        public string ArmIdle;
        public float HeadOffsetX;
        public float HeadOffsetY;
        public float ArmOffsetX;
        public float ArmOffsetY;
        /// <summary>
        /// Length = 4. Most likely { R, G, B, A }
        /// </summary>
        public byte[] Color;

        public static void SaveToBin(Humanoid item, BinaryWriter writer)
        {
            StringIO.SaveToBin(item.Name, writer);
            StringIO.SaveToBin(item.Species, writer);
            writer.Write(item.Gender);
            StringIO.SaveToBin(item.HairGroup, writer);
            StringIO.SaveToBin(item.HairType, writer);
            StringIO.SaveToBin(item.HairDirectives, writer);
            StringIO.SaveToBin(item.BodyDirectives, writer);
            StringIO.SaveToBin(item.FacialHairGroup, writer);
            StringIO.SaveToBin(item.FacialHairType, writer);
            StringIO.SaveToBin(item.FacialHairDirectives, writer);
            StringIO.SaveToBin(item.FacialMaskGroup, writer);
            StringIO.SaveToBin(item.FacialMaskType, writer);
            StringIO.SaveToBin(item.FacialMaskDirectives, writer);
            StringIO.SaveToBin(item.Idle, writer);
            StringIO.SaveToBin(item.ArmIdle, writer);
            writer.Write(item.HeadOffsetX);
            writer.Write(item.HeadOffsetY);
            writer.Write(item.ArmOffsetX);
            writer.Write(item.ArmOffsetY);

            for (int i = 0; i < 4; i++)
            {
                writer.Write(item.Color[i]);
            }
        }

        public static Humanoid LoadFromBin(BinaryReader reader)
        {
            Humanoid result = new Humanoid();

            result.Name = StringIO.LoadFromBin(reader);
            result.Species = StringIO.LoadFromBin(reader);
            result.Gender = reader.ReadByte();
            result.HairGroup = StringIO.LoadFromBin(reader);
            result.HairType = StringIO.LoadFromBin(reader);
            result.HairDirectives = StringIO.LoadFromBin(reader);
            result.BodyDirectives = StringIO.LoadFromBin(reader);
            result.FacialHairGroup = StringIO.LoadFromBin(reader);
            result.FacialHairType = StringIO.LoadFromBin(reader);
            result.FacialHairDirectives = StringIO.LoadFromBin(reader);
            result.FacialMaskGroup = StringIO.LoadFromBin(reader);
            result.FacialMaskType = StringIO.LoadFromBin(reader);
            result.FacialMaskDirectives = StringIO.LoadFromBin(reader);
            result.Idle = StringIO.LoadFromBin(reader);
            result.ArmIdle = StringIO.LoadFromBin(reader);
            result.HeadOffsetX = reader.ReadSingle();
            result.HeadOffsetY = reader.ReadSingle();
            result.ArmOffsetX = reader.ReadSingle();
            result.ArmOffsetY = reader.ReadSingle();

            result.Color = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                result.Color[i] = reader.ReadByte();
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)Name.GetSize();
            result += (ulong)Species.GetSize();
            result += (ulong)Gender.GetSize();
            result += (ulong)HairGroup.GetSize();
            result += (ulong)HairType.GetSize();
            result += (ulong)HairDirectives.GetSize();
            result += (ulong)BodyDirectives.GetSize();
            result += (ulong)FacialHairGroup.GetSize();
            result += (ulong)FacialHairType.GetSize();
            result += (ulong)FacialHairDirectives.GetSize();
            result += (ulong)FacialMaskGroup.GetSize();
            result += (ulong)FacialMaskType.GetSize();
            result += (ulong)FacialMaskDirectives.GetSize();
            result += (ulong)Idle.GetSize();
            result += (ulong)ArmIdle.GetSize();
            result += (ulong)HeadOffsetX.GetSize();
            result += (ulong)HeadOffsetY.GetSize();
            result += (ulong)ArmOffsetX.GetSize();
            result += (ulong)ArmOffsetY.GetSize();

            return new PackedInt() { Value = result };
        }
    }
}
