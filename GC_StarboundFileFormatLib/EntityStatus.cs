using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GC_StarboundFileFormatLib
{
    /// <summary>
    /// Stores the default status values of an entity.
    /// </summary>
    public class EntityStatus
    {

        public bool Admin;
        public float MinBodyTemperature;
        public float MaxBodyTemperature;
        public float IdealTemperature;
        public float MaxWarmth;
        public float UNK_VAL_01;
        public float WarmthRate;
        public float ConfortRegen;
        public float MaxHealth;
        public float MaxEnergy;
        public float EnergyRate;
        public float MaxFood;
        public float MaxBreath;
        public float BreathRate;
        public float DrownRate;
        public float WindChill;
        public string BodyMaterial;
        public string DamageConfig;


        public static void SaveToBin(EntityStatus item, BinaryWriter writer)
        {
            writer.Write(item.Admin);
            writer.Write(item.MinBodyTemperature);
            writer.Write(item.MaxBodyTemperature);
            writer.Write(item.IdealTemperature);
            writer.Write(item.MaxWarmth);
            writer.Write(item.UNK_VAL_01);
            writer.Write(item.WarmthRate);
            writer.Write(item.ConfortRegen);
            writer.Write(item.MaxHealth);
            writer.Write(item.MaxEnergy);
            writer.Write(item.EnergyRate);
            writer.Write(item.MaxFood);
            writer.Write(item.MaxBreath);
            writer.Write(item.BreathRate);
            writer.Write(item.DrownRate);
            writer.Write(item.WindChill);
            StringIO.SaveToBin(item.BodyMaterial, writer);
            StringIO.SaveToBin(item.DamageConfig, writer);
        }

        public static EntityStatus LoadFromBin(BinaryReader reader)
        {
            EntityStatus result = new EntityStatus();

            result.Admin = reader.ReadBoolean();
            result.MinBodyTemperature = reader.ReadSingle();
            result.MaxBodyTemperature = reader.ReadSingle();
            result.IdealTemperature = reader.ReadSingle();
            result.MaxWarmth = reader.ReadSingle();
            result.UNK_VAL_01 = reader.ReadSingle();
            result.WarmthRate = reader.ReadSingle();
            result.ConfortRegen = reader.ReadSingle();
            result.MaxHealth = reader.ReadSingle();
            result.MaxEnergy = reader.ReadSingle();
            result.EnergyRate = reader.ReadSingle();
            result.MaxFood = reader.ReadSingle();
            result.MaxBreath = reader.ReadSingle();
            result.BreathRate = reader.ReadSingle();
            result.DrownRate = reader.ReadSingle();
            result.WindChill = reader.ReadSingle();
            result.BodyMaterial = StringIO.LoadFromBin(reader);
            result.DamageConfig = StringIO.LoadFromBin(reader);

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)Admin.GetSize();
            result += (ulong)MinBodyTemperature.GetSize();
            result += (ulong)MaxBodyTemperature.GetSize();
            result += (ulong)IdealTemperature.GetSize();
            result += (ulong)MaxWarmth.GetSize();
            result += (ulong)UNK_VAL_01.GetSize();
            result += (ulong)WarmthRate.GetSize();
            result += (ulong)ConfortRegen.GetSize();
            result += (ulong)MaxHealth.GetSize();
            result += (ulong)MaxEnergy.GetSize();
            result += (ulong)EnergyRate.GetSize();
            result += (ulong)MaxFood.GetSize();
            result += (ulong)MaxBreath.GetSize();
            result += (ulong)BreathRate.GetSize();
            result += (ulong)DrownRate.GetSize();
            result += (ulong)WindChill.GetSize();
            result += (ulong)BodyMaterial.GetSize();
            result += (ulong)DamageConfig.GetSize();

            return new PackedInt() { Value = result };
        }
    }
}
