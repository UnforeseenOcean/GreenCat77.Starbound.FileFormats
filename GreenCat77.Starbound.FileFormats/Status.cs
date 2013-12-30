using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Stores the current status of a player.
    /// </summary>
    public class Status
    {

        public float CurrentHealth;
        public float MaxHealth;
        public float CurrentEnergy;
        public float MaxEnergy;
        public float CurrentWarmth;
        public float MaxWarmth;
        public float CurrentFood;
        public float MaxFood;
        public float CurrentBreath;
        public float MaxBreath;
        public bool Invulnerable;
        /// <summary>
        /// 3 Long: { R, G, B }
        /// </summary>
        public float[] GlowColor;

        /// <summary>
        /// Pre: PackedInt - Length
        /// Length = NumActveEffects
        /// </summary>
        public List<string> ActiveEffects = new List<string>();
       
        /// <summary>
        /// Pre: PackedInt - Length
        /// Length = NumEffectSources
        /// </summary>
        public List<string> EffectSources = new List<string>();

        public static void SaveToBin(Status item, BinaryWriter writer)
        {            
            writer.Write(item.CurrentHealth);
            writer.Write(item.MaxHealth);
            writer.Write(item.CurrentEnergy);
            writer.Write(item.MaxEnergy);
            writer.Write(item.CurrentWarmth);
            writer.Write(item.MaxWarmth);
            writer.Write(item.CurrentFood);
            writer.Write(item.MaxFood);
            writer.Write(item.CurrentBreath);
            writer.Write(item.MaxBreath);
            writer.Write(item.Invulnerable);

            for (int i = 0; i < 3; i++)
            {
                writer.Write(item.GlowColor[i]);
            }

            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.ActiveEffects.Count }, writer);

            for (int i = 0; i < item.ActiveEffects.Count; i++)
            {
                StringIO.SaveToBin(item.ActiveEffects[i], writer);
            }

            PackedInt.SaveToBin(new PackedInt() { Value = (ulong)item.EffectSources.Count }, writer);

            for (int i = 0; i < item.EffectSources.Count; i++)
            {
                StringIO.SaveToBin(item.EffectSources[i], writer);
            }
        }

        public static Status LoadFromBin(BinaryReader reader)
        {
            Status result = new Status();

            result.CurrentHealth = reader.ReadSingle();
            result.MaxHealth = reader.ReadSingle();
            result.CurrentEnergy = reader.ReadSingle();
            result.MaxEnergy = reader.ReadSingle();
            result.CurrentWarmth = reader.ReadSingle();
            result.MaxWarmth = reader.ReadSingle();
            result.CurrentFood = reader.ReadSingle();
            result.MaxFood = reader.ReadSingle();
            result.CurrentBreath = reader.ReadSingle();
            result.MaxBreath = reader.ReadSingle();
            result.Invulnerable = reader.ReadBoolean();

            result.GlowColor = new float[3];

            for (int i = 0; i < 3; i++)
            {
                result.GlowColor[i] = reader.ReadSingle();
            }

            PackedInt numActiveEffects = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numActiveEffects.Value; i++)
            {
                result.ActiveEffects.Add(StringIO.LoadFromBin(reader));
            }

            PackedInt numEffectSources = PackedInt.LoadFromBin(reader);

            for (ulong i = 0; i < numEffectSources.Value; i++)
            {
                result.EffectSources.Add(StringIO.LoadFromBin(reader));
            }

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)CurrentHealth.GetSize();
            result += (ulong)MaxHealth.GetSize();
            result += (ulong)CurrentEnergy.GetSize();
            result += (ulong)MaxEnergy.GetSize();
            result += (ulong)CurrentWarmth.GetSize();
            result += (ulong)MaxWarmth.GetSize();
            result += (ulong)CurrentFood.GetSize();
            result += (ulong)MaxFood.GetSize();
            result += (ulong)CurrentBreath.GetSize();
            result += (ulong)MaxBreath.GetSize();
            result += (ulong)Invulnerable.GetSize();

            result += (ulong)GlowColor.Length;

            result += new PackedInt() { Value = (ulong)ActiveEffects.Count }.GetSizeOfData().Value;

            for (int i = 0; i < ActiveEffects.Count; i++)
            {
                result += (ulong)ActiveEffects[i].GetSize();
            }

            result += new PackedInt() { Value = (ulong)EffectSources.Count }.GetSizeOfData().Value;

            for (int i = 0; i < EffectSources.Count; i++)
            {
                result += (ulong)EffectSources[i].GetSize();
            }

            return new PackedInt() { Value = result };
        }
    }
}
