using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace GreenCat77.Starbound.FileFormats
{
    /// <summary>
    /// Holds the actual data of the player.
    /// </summary>
    public class Player
    {
        public static readonly int UUID_LENGTH = 16;

        /// <summary>
        /// Tells whether the UUID is present.
        /// </summary>
        public bool HasUUID;
        /// <summary>
        /// UUID. Only present if HasUUID is true. Length = 16
        /// </summary>
        public byte[] UUID;
        /// <summary>
        /// Stores player appearance data.
        /// </summary>
        public Humanoid HumanoidEntity;
        /// <summary>
        /// Stores default values about status parameters.
        /// </summary>
        public EntityStatus DefaultStatus;
        /// <summary>
        /// Stores the current values of status.
        /// </summary>
        public Status CurrentStatus;
        /// <summary>
        /// Description Text for Player, Apparently
        /// </summary>
        public string Description;
        /// <summary>
        /// How long you've been playing as the char.
        /// </summary>
        public double PlayingTime;
        /// <summary>
        /// The player's inventory ¬_¬
        /// </summary>
        public PlayerInventory Inventory;
        /// <summary>
        /// Blueprints.
        /// </summary>
        public BlueprintData Blueprints;
        /// <summary>
        /// Player's Techs
        /// </summary>
        public TechController Tech;
        /// <summary>
        /// The player's current equipped armor, including vanity slots.
        /// </summary>
        public Armor EquippedArmor;
        /// <summary>
        /// Item in the player's left hand.
        /// </summary>
        public Item LeftHandItem;
        /// <summary>
        /// Item in the player's right hand.
        /// </summary>
        public Item RightHandItem;

        public static void SaveToBin(Player item, BinaryWriter writer)
        {
            PackedInt.SaveToBin(item.GetSizeOfData(), writer);
            writer.Write(item.HasUUID);
            if (item.HasUUID)
            {
                for (int i = 0; i < UUID_LENGTH; i++)
                {
                    if (i >= 0 && i < item.UUID.Length)
                    {
                        writer.Write(item.UUID[i]);
                    }
                    else
                    {
                        writer.Write((byte)30);
                    }
                }
            }

            Humanoid.SaveToBin(item.HumanoidEntity, writer);
            EntityStatus.SaveToBin(item.DefaultStatus, writer);
            Status.SaveToBin(item.CurrentStatus, writer);
            writer.Write(item.Description);
            writer.Write(item.PlayingTime);
            PlayerInventory.SaveToBin(item.Inventory, writer);
            BlueprintData.SaveToBin(item.Blueprints, writer);
            TechController.SaveToBin(item.Tech, writer);
            Armor.SaveToBin(item.EquippedArmor, writer);
            Item.SaveToBin(item.LeftHandItem, writer);
            Item.SaveToBin(item.RightHandItem, writer);
        }

        public static Player LoadFromBin(BinaryReader reader)
        {
            Player result = new Player();

            PackedInt lengthOfData = PackedInt.LoadFromBin(reader);

            result.HasUUID = reader.ReadBoolean();
            if (result.HasUUID)
            {
                result.UUID = new byte[UUID_LENGTH];

                for (int i = 0; i < UUID_LENGTH; i++)
                {
                    result.UUID[i] = reader.ReadByte();
                }
            }

            result.HumanoidEntity = Humanoid.LoadFromBin(reader);
            result.DefaultStatus = EntityStatus.LoadFromBin(reader);
            result.CurrentStatus = Status.LoadFromBin(reader);
            result.Description = StringIO.LoadFromBin(reader);
            result.PlayingTime = reader.ReadDouble();
            result.Inventory = PlayerInventory.LoadFromBin(reader);
            result.Blueprints = BlueprintData.LoadFromBin(reader);
            result.Tech = TechController.LoadFromBin(reader);
            result.EquippedArmor = Armor.LoadFromBin(reader);
            result.LeftHandItem = Item.LoadFromBin(reader);
            result.RightHandItem = Item.LoadFromBin(reader);

            return result;
        }

        public PackedInt GetSizeOfData()
        {
            ulong result = 0;

            result += (ulong)HasUUID.GetSize();

            if (HasUUID)
            {
                result += (ulong)UUID_LENGTH;
            }

            result += HumanoidEntity.GetSizeOfData().Value;
            result += DefaultStatus.GetSizeOfData().Value;
            result += CurrentStatus.GetSizeOfData().Value;
            result += (ulong)Description.GetSize();
            result += (ulong)PlayingTime.GetSize();
            result += Inventory.GetSizeOfData().Value;
            result += Blueprints.GetSizeOfData().Value;
            result += Tech.GetSizeOfData().Value;
            result += EquippedArmor.GetSizeOfData().Value;
            result += LeftHandItem.GetSizeOfData().Value;
            result += RightHandItem.GetSizeOfData().Value;

            return new PackedInt() { Value = result };
        }
    }
}
