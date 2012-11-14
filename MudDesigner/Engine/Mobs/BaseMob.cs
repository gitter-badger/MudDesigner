/* BaseMob
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The Base class for all Mobs (Characters) in the game world.
 */
//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;

//Newtonsoft JSon using statements
using Newtonsoft.Json;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// The Base class for all Mobs (Characters) in the game world.
    /// </summary>
    public abstract class BaseMob : GameObject, IMob
    {
        #region Event Handlers
        //Environment handlers
        public delegate void OnLeaveHandler(IRoom arrivalRoom, bool cancel = false);
        public delegate void OnEnterHandler(IRoom departingRoom);
        public event OnLeaveHandler OnLeaveEvent;
        public event OnEnterHandler OnEnterEvent;

        //Stat modifier handlers
        public delegate void OnAttackTargetHandler(IMob target);
        public delegate void OnDealDamageHandler(IMob[] targets, int amount);
        public delegate void OnRecieveDamageHandler(IGameObject dealer, int amount);
        public delegate void OnHealHandler(IGameObject dealer, int amount);
        public delegate void OnRestoreManaHandler(IGameObject dealer, int amount);
        public delegate void OnConsumeManaHandler(int amount);
        public delegate void OnDeathHandler(IGameObject dealer);
        public event OnAttackTargetHandler OnAttackTargetEvent;
        public event OnDealDamageHandler OnDealDamageEvent;
        public event OnRecieveDamageHandler OnRecieveDamageEvent;
        public event OnHealHandler OnHealEvent;
        public event OnRestoreManaHandler OnRestoreManaEvent;
        public event OnConsumeManaHandler OnConsumeManaEvent;
        public event OnDeathHandler OnDeathEvent;

        //Item handlers
        public delegate void OnEquipHandler(IItem item);
        public delegate void OnUnequipHandler(IItem item);
        public delegate void OnUseItemHandler(IItem item);
        public delegate void OnRemoveItemHandler(IItem item);
        public event OnEquipHandler OnEquipEvent;
        public event OnUnequipHandler OnUnequipEvent;
        public event OnUseItemHandler OnUseItemEvent;
        public event OnRemoveItemHandler OnRemoveItemEvent;
        #endregion

        /// <summary>
        /// Gender of this character
        /// </summary>
        public IGender Gender { get; set; }

        /// <summary>
        /// Race of this character
        /// </summary>
        public IRace Race { get; set; }

        /// <summary>
        /// Character Class
        /// </summary>
        public IClass Class { get; set; }

        /// <summary>
        /// Current location this character resides within
        /// </summary>
        public IRoom Location { get; protected set; }

        /// <summary>
        /// Gets or Sets if this Character can Talk
        /// </summary>
        public bool CanTalk { get; set; }

        /// <summary>
        /// Gets or Sets the maximum number of items the character can carry.
        /// </summary>
        public int MaxInventorySize { get; set; }
        
        /// <summary>
        /// Gets or Sets a collection of Items that the character has.
        /// </summary>
        public List<IItem> Items { get; set; }

        /// <summary>
        /// Gets or Sets a collection of stats that the character has
        /// </summary>
        public List<IStat> Stats { get; set; }

        /// <summary>
        /// Gets or Sets a reference to a collection of appearance attributes the character has.
        /// </summary>
        public List<IAppearanceAttribute> Appearance { get; set; }

        [JsonIgnore()] //Don't need to save this with the player.
        public IServerDirector Director { get; set; }

        public BaseMob()
        {
            Items = new List<IItem>();
            Stats = new List<IStat>();
            Appearance = new List<IAppearanceAttribute>();

            //Setup the character event handlers.
            OnLeaveEvent += new OnLeaveHandler(OnLeave);
            OnEnterEvent += new OnEnterHandler(OnEnter);

            //Following event methods are stubs in the event they are ever called
            //without the user having attached its own methods to them.
            OnAttackTargetEvent += new OnAttackTargetHandler(OnAttackTarget);
            OnDealDamageEvent += new OnDealDamageHandler(OnDealDamage);
            OnRecieveDamageEvent += new OnRecieveDamageHandler(OnReceiveDamage);
            OnHealEvent += new OnHealHandler(OnHeal);
            OnRestoreManaEvent += new OnRestoreManaHandler(OnRestoreMana);
            OnConsumeManaEvent += new OnConsumeManaHandler(OnConsumeMana);
            OnDeathEvent += new OnDeathHandler(OnDeath);

            OnEquipEvent += new OnEquipHandler(OnEquip);
            OnUnequipEvent += new OnUnequipHandler(OnUnequip);
            OnUseItemEvent += new OnUseItemHandler(OnUseItem);
            OnRemoveItemEvent += new OnRemoveItemHandler(OnRemoveItem);
        }

        //Abstract methods that are required to be implemented by the child class of BaseMob
        public abstract void SendMessage(string message, bool newLine = true);
        public abstract void Attack(IMob target);
        public abstract void Attack(IMob[] targets);
        public abstract void Damage(IGameObject dealer, int amount);
        public abstract void Heal(IGameObject dealer, int amount);
        public abstract void RestoreMana(IGameObject dealer, int amount);
        public abstract void ConsumeMana(int amount);

        /// <summary>
        /// Takes all of this Game Objects properties and copies them over to the argument object.
        /// </summary>
        /// <param name="copyTo">The object that will have it's properties replaced with the calling Object</param>
        public override void CopyState(ref dynamic copyTo)
        {
            if (copyTo is IMob)
            {
                Scripting.ScriptObject newObject = new Scripting.ScriptObject(copyTo);

                newObject.SetProperty("Gender", Gender, null);
                newObject.SetProperty("Race", Race, null);
                newObject.SetProperty("Class", Class, null);
                newObject.SetProperty("Location", Location, null);

                newObject.SetProperty("CanTalk", CanTalk, null);
                newObject.SetProperty("MaxInventorySize", MaxInventorySize, null);
                newObject.SetProperty("Items", Items, null);
                newObject.SetProperty("Stats", Stats, null);
                newObject.SetProperty("Appearance", Appearance, null);
            }

            base.CopyState(ref copyTo);
        }

        /// <summary>
        /// Adds a specified item to the characters inventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(IItem item)
        {
            if (Items == null)
                Items = new List<IItem>();

            if (Items.Count > MaxInventorySize)
                return;
            else
                Items.Add(item);
        }

        /// <summary>
        /// Returns an array of the characters inventory items
        /// </summary>
        /// <returns></returns>
        public IItem[] GetItems()
        {
            return Items.ToArray();
        }

        /// <summary>
        /// Removes the specified item from the characters inventory
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(IItem item)
        {
            if (Items.Contains(item))
                Items.Remove(item);
        }

        /// <summary>
        /// Removes the specified item from the characters inventory
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(string item)
        {
            foreach (IItem currentItem in Items)
            {
                if (currentItem.Name == item)
                {
                    Items.Remove(currentItem);
                    break;
                }
            }
        }

        /// <summary>
        /// Clears the characters inventory
        /// </summary>
        public void ClearItems()
        {
            Items.Clear();
        }

        /// <summary>
        /// Adds the specified stat to the character
        /// </summary>
        /// <param name="stat"></param>
        public void AddStat(IStat stat)
        {
            if (stat == null)
                return;

            if (Stats.Contains(stat))
                return;
            else
                Stats.Add(stat);
        }

        /// <summary>
        /// Returns an array of all stats the character currently has
        /// </summary>
        /// <returns></returns>
        public IStat[] GetStats()
        {
            if (Stats == null)
                Stats = new List<IStat>();

            return Stats.ToArray();
        }

        /// <summary>
        /// Removes the specified stat from the character
        /// </summary>
        /// <param name="stat"></param>
        public void RemoveStat(IStat stat)
        {
            if (Stats == null)
                return;

            if (Stats.Contains(stat))
                Stats.Remove(stat);
        }

        /// <summary>
        /// Adds an appearance attribute to the character
        /// </summary>
        /// <param name="attribute"></param>
        public void AddAppearanceDescription(IAppearanceAttribute attribute)
        {
            if (Appearance.Contains(attribute))
                return;
            else
                Appearance.Add(attribute);
        }

        /// <summary>
        /// Gets all of the appearance attributes that the character has.
        /// </summary>
        /// <returns></returns>
        public IAppearanceAttribute[] GetAppearanceDescriptions()
        {
            return Appearance.ToArray();
        }

        /// <summary>
        /// Removes a specified appearance attribute from the character
        /// </summary>
        /// <param name="attribute"></param>
        public void RemoveAppearanceAttribute(IAppearanceAttribute attribute)
        {
            if (Appearance.Contains(attribute))
                Appearance.Remove(attribute);
        }

        /// <summary>
        /// Sends a message from the character, to all occupants within the broadcast range specified.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="broadcastLevel"></param>
        public virtual void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room)
        {
            switch (broadcastLevel)
            {
                case MessageBroadcastLevels.Room:
                    {
                        foreach (IPlayer player in Location.Occupants)
                        {
                            player.SendMessage(message);
                        }
                        break;
                    }
                case MessageBroadcastLevels.Zone:
                    {
                        foreach (IRoom room in Location.Zone.Rooms)
                        {
                            foreach (IPlayer player in room.Occupants)
                                player.SendMessage(message);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Sends a message from the character, to the target character
        /// </summary>
        /// <param name="message"></param>
        /// <param name="target"></param>
        public virtual void Talk(string message, IMob target)
        {
            if (target == null)
                return;
            else
                target.SendMessage(message);
        }

        /// <summary>
        /// Sends a message from the character, to the target group of characters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="group"></param>
        public virtual void Talk(string message, IMob[] group)
        {
            if (group == null)
                return;

            foreach (IMob mob in group)
            {
                if (mob == null)
                    return;
                else
                    mob.SendMessage(message);
            }
        }

        /// <summary>
        /// Moves the character from one location, to another.
        /// </summary>
        /// <param name="room"></param>
        public virtual void Move(IRoom room)
        {
            //Store our current room.
            IRoom departingRoom = Location;

            //If the room is null, call OnLeave with the Cancel arg true.
            if (room == null || Location == null)
                OnLeaveEvent(room, true);
            else
            {
                //Call OnLeave to execute the code that will
                //move the player from the current room to the new room.
                OnLeaveEvent(room);
            }

            if (room != null)
                Location = room;

            //If we have successfully moved, call OnEnter and provide the
            //room we just left in the event we still need to access it.
            if (Location == room)
                OnEnterEvent(departingRoom);
        }


        public void OnLeave(IRoom arrivalRoom, bool cancel = false)
        {
            if (cancel || arrivalRoom == null)
                return; //Don't change our location if we are cancelled or null for some reason.

            IDoor door = null;
            //Find the doorway in this Location that we are going to
            foreach(IDoor d in Location.GetDoorways())
            {
                if (d.Arrival == arrivalRoom)
                    door = d;
            }

            if (door == null)
                return;

            //Remove this character from the departing room
            Location.RemoveCharacter(this, door.FacingDirection);
        }

        public void OnEnter(IRoom departingRoom)
        {
            //Stub in the event this is called and the user hasn't attached its own
            //event method. This is called by BaseMob and thus, must always exist.

            IDoor door = null;
            foreach (IDoor d in Location.GetDoorways())
            {
                if (d.Arrival == departingRoom)
                    door = d;
            }

            if (door == null)
                Location.AddCharacter(this, AvailableTravelDirections.None);
            else
                Location.AddCharacter(this, door.FacingDirection);
        }

        private void OnDeath(IGameObject dealer) { }

        private void OnConsumeMana(int amount) { }

        private void OnRestoreMana(IGameObject dealer, int amount) { }

        private void OnHeal(IGameObject dealer, int amount) { }

        private void OnReceiveDamage(IGameObject dealer, int amount) { }

        private void OnDealDamage(IMob[] targets, int amount) { }

        private void OnAttackTarget(IMob target) { }

        private void OnRemoveItem(IItem item) { }

        private void OnUseItem(IItem item) { }

        private void OnUnequip(IItem item) { }

        private void OnEquip(IItem item) { }
    }
}
