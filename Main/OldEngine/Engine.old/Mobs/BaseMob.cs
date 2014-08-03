//-----------------------------------------------------------------------
// <copyright file="BaseMob.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Directors;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// The Base class for all Mobs (Characters) in the game world.
    /// </summary>
    public abstract class BaseMob : GameObject, IMob
    {
        #region Event Handlers

        /// <summary>
        /// The event for when a mob leaves an environment.
        /// </summary>
        /// <param name="arrivalRoom">The arrival room.</param>
        /// <param name="cancel">if set to <c>true</c> [cancel].</param>
        public delegate void OnLeaveHandler(IRoom arrivalRoom, bool cancel = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departingRoom">The departing room.</param>
        public delegate void OnEnterHandler(IRoom departingRoom);

        /// <summary>
        /// Occurs when [on leave event].
        /// </summary>
        public event OnLeaveHandler OnLeaveEvent;

        /// <summary>
        /// Occurs when [on enter event].
        /// </summary>
        public event OnEnterHandler OnEnterEvent;

        /// <summary>
        /// The handler for when the mob attacks its target.
        /// </summary>
        /// <param name="target">The target.</param>
        public delegate void OnAttackTargetHandler(IMob target);

        /// <summary>
        /// The handler for when the mob deals damage to its target.
        /// </summary>
        /// <param name="targets">The targets.</param>
        /// <param name="amount">The amount.</param>
        public delegate void OnDealDamageHandler(IMob[] targets, int amount);

        /// <summary>
        /// The handler for when the mob receives damange from a IGameObject.
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        public delegate void OnRecieveDamageHandler(IGameObject dealer, int amount);

        /// <summary>
        /// The handler for when the mob is healed by a IGameObject
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        public delegate void OnHealHandler(IGameObject dealer, int amount);

        /// <summary>
        /// The handler for when the mob has mana restored by a IGameObject
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        public delegate void OnRestoreManaHandler(IGameObject dealer, int amount);

        /// <summary>
        /// The handler for when the mob consumes mana
        /// </summary>
        /// <param name="amount">The amount.</param>
        public delegate void OnConsumeManaHandler(int amount);

        /// <summary>
        /// The handler for when the mob is killed.
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        public delegate void OnDeathHandler(IGameObject dealer);

        /// <summary>
        /// Occurs when [on attack target event].
        /// </summary>
        public event OnAttackTargetHandler OnAttackTargetEvent;

        /// <summary>
        /// Occurs when [on deal damage event].
        /// </summary>
        public event OnDealDamageHandler OnDealDamageEvent;

        /// <summary>
        /// Occurs when [on recieve damage event].
        /// </summary>
        public event OnRecieveDamageHandler OnRecieveDamageEvent;

        /// <summary>
        /// Occurs when [on heal event].
        /// </summary>
        public event OnHealHandler OnHealEvent;

        /// <summary>
        /// Occurs when [on restore mana event].
        /// </summary>
        public event OnRestoreManaHandler OnRestoreManaEvent;

        /// <summary>
        /// Occurs when [on consume mana event].
        /// </summary>
        public event OnConsumeManaHandler OnConsumeManaEvent;

        /// <summary>
        /// Occurs when [on death event].
        /// </summary>
        public event OnDeathHandler OnDeathEvent;

        /// <summary>
        /// The handler for when the mob equips an IItem
        /// </summary>
        /// <param name="item">The item.</param>
        public delegate void OnEquipHandler(IItem item);

        /// <summary>
        /// The handler for when the mob unequips an IItem
        /// </summary>
        /// <param name="item">The item.</param>
        public delegate void OnUnequipHandler(IItem item);

        /// <summary>
        /// The handler for when the mob uses an IItem
        /// </summary>
        /// <param name="item">The item.</param>
        public delegate void OnUseItemHandler(IItem item);

        /// <summary>
        /// The handler for when the Mob removes an IITem
        /// </summary>
        /// <param name="item">The item.</param>
        public delegate void OnRemoveItemHandler(IItem item);

        /// <summary>
        /// Occurs when [on equip event].
        /// </summary>
        public event OnEquipHandler OnEquipEvent;

        /// <summary>
        /// Occurs when [on unequip event].
        /// </summary>
        public event OnUnequipHandler OnUnequipEvent;

        /// <summary>
        /// Occurs when [on use item event].
        /// </summary>
        public event OnUseItemHandler OnUseItemEvent;

        /// <summary>
        /// Occurs when [on remove item event].
        /// </summary>
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
        public IRoom Location { get; set; }

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

        /// <summary>
        /// Gets or sets the director.
        /// </summary>
        /// <value>
        /// The director.
        /// </value>
        [JsonIgnore()] // Don't need to save this with the player.
        [DisableStateCopy()] // Don't allow BaseMob.CopyState to copy this property
        public IServerDirector Director { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMob"/> class.
        /// </summary>
        public BaseMob()
        {
            Items = new List<IItem>();
            Stats = new List<IStat>();
            Appearance = new List<IAppearanceAttribute>();

            // Setup the character event handlers.
            OnLeaveEvent += new OnLeaveHandler(OnLeave);
            OnEnterEvent += new OnEnterHandler(OnEnter);

            // Following event methods are stubs in the event they are ever called
            // without the user having attached its own methods to them.
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

        // Abstract methods that are required to be implemented by the child class of BaseMob
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="newLine">if set to <c>true</c> [new line].</param>
        public abstract void SendMessage(string message, bool newLine = true);

        /// <summary>
        /// Attacks the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        public abstract void Attack(IMob target);

        /// <summary>
        /// Attacks the specified targets.
        /// </summary>
        /// <param name="targets">The targets.</param>
        public abstract void Attack(IMob[] targets);

        /// <summary>
        /// Damages the specified dealer.
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        public abstract void Damage(IGameObject dealer, int amount);

        /// <summary>
        /// Heals the specified dealer.
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        public abstract void Heal(IGameObject dealer, int amount);

        /// <summary>
        /// Restores the mana.
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        public abstract void RestoreMana(IGameObject dealer, int amount);

        /// <summary>
        /// Consumes the mana.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public abstract void ConsumeMana(int amount);

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
            // Store our current room.
            IRoom departingRoom = Location;

            // If the room is null, call OnLeave with the Cancel arg true.
            if (room == null || Location == null)
                OnLeaveEvent(room, true);
            else
            {
                // Call OnLeave to execute the code that will
                // move the player from the current room to the new room.
                OnLeaveEvent(room);
            }

            if (room != null)
                Location = room;

            // If we have successfully moved, call OnEnter and provide the
            // room we just left in the event we still need to access it.
            if (Location == room)
                OnEnterEvent(departingRoom);
        }


        public void OnLeave(IRoom arrivalRoom, bool cancel = false)
        {
            if (cancel || arrivalRoom == null)
                return; // Don't change our location if we are cancelled or null for some reason.

            IDoor door = null;
            // Find the doorway in this Location that we are going to
            foreach(IDoor d in Location.GetDoorways())
            {
                if (d.Arrival == arrivalRoom)
                    door = d;
            }

            if (door == null)
                return;
            
          // Remove this character from the departing room
            Location.RemoveCharacter(this, door.FacingDirection);
        }

        public void OnEnter(IRoom departingRoom)
        {
            // Stub in the event this is called and the user hasn't attached its own
            // event method. This is called by BaseMob and thus, must always exist.

            IDoor door = null;
            foreach (IDoor d in Location.GetDoorways())
            {
                if (d.Arrival == departingRoom)
                    door = d;
            }

            if (door == null && !Location.Occupants.Contains(this))
                Location.AddCharacter(this, AvailableTravelDirections.None);
            else if (!Location.Occupants.Contains(this))
                Location.AddCharacter(this, door.FacingDirection);
        }

        /// <summary>
        /// Called when [death].
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        private void OnDeath(IGameObject dealer) { }

        /// <summary>
        /// Called when [consume mana].
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void OnConsumeMana(int amount) { }

        /// <summary>
        /// Called when [restore mana].
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        private void OnRestoreMana(IGameObject dealer, int amount) { }

        /// <summary>
        /// Called when [heal].
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        private void OnHeal(IGameObject dealer, int amount) { }

        /// <summary>
        /// Called when [receive damage].
        /// </summary>
        /// <param name="dealer">The dealer.</param>
        /// <param name="amount">The amount.</param>
        private void OnReceiveDamage(IGameObject dealer, int amount) { }

        /// <summary>
        /// Called when [deal damage].
        /// </summary>
        /// <param name="targets">The targets.</param>
        /// <param name="amount">The amount.</param>
        private void OnDealDamage(IMob[] targets, int amount) { }

        /// <summary>
        /// Called when [attack target].
        /// </summary>
        /// <param name="target">The target.</param>
        private void OnAttackTarget(IMob target) { }

        /// <summary>
        /// Called when [remove item].
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnRemoveItem(IItem item) { }

        /// <summary>
        /// Called when [use item].
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnUseItem(IItem item) { }

        /// <summary>
        /// Called when [unequip].
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnUnequip(IItem item) { }

        /// <summary>
        /// Called when [equip].
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnEquip(IItem item) { }
    }
}
