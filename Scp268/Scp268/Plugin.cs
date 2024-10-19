using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using PlayerEvents = Exiled.Events.Handlers.Player;
using MEC;
using UnityEngine;
using Exiled.Events.EventArgs.Player;
using Exiled.CreditTags;
using CommandSystem.Commands.RemoteAdmin;
using CommandSystem;
using Exiled.Events.Features;
using Exiled.API.Interfaces;
using InventorySystem;

namespace Scp268
{
    public class Plugin : Plugin<Config>
    {
        private static float currentHealth;

        public override string Name => "Scp268";
        public override string Author => "Finor";
        public override Version RequiredExiledVersion => new Version(8, 12, 2);
        public override Version Version => new Version(1, 0, 0);
        private Dictionary<Player, float> originalCooldowns = new Dictionary<Player, float>();

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            base.OnDisabled();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.SCP268)
            {
                ev.IsAllowed = true;
                ev.Player.EnableEffect(EffectType.Invisible, 1);
                ev.Player.RemoveItem(ev.Item.Serial);
                ev.Player.AddItem(ItemType.SCP268);
            }
        }
        private void OnShooting(ShootingEventArgs ev)
        {
            if (ev.Player.GetEffect(EffectType.Invisible).IsEnabled)
                ev.Player.DisableEffect(EffectType.Invisible);
        }
    }
}
