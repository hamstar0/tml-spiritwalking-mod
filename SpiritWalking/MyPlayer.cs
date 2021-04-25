using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;
using SpiritWalking.Items;


namespace SpiritWalking {
	partial class SpiritWalkingPlayer : ModPlayer {
		internal bool IsSpiritWalking = false;


		internal Projectile FlightProjectile = null;


		internal Vector2 IntendedFlightVelocity = SpiritWalkFlightLogic.DefaultFlightHeading;

		internal float CurrentFlightSpeedScale = 1f;


		internal int FlightBurstCooldown = 0;

		internal int FlightBurstDuration = 0;


		internal int FinalDashElapsed = 0;


		internal int NoPelletPickupDuration = 0;

		////

		internal ISet<int> EatenPelletCoords = new HashSet<int>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.EatenPelletCoords.Clear();

			if( !tag.ContainsKey("eaten_pellet_count") ) {
				return;
			}

			int count = tag.GetInt( "eaten_pellet_count" );

			for( int i=0; i<count; i++ ) {
				this.EatenPelletCoords.Add( tag.GetInt("eaten_pellet_coord_"+i) );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "eaten_pellet_count", this.EatenPelletCoords.Count }
			};

			int i = 0;
			foreach( int coord in this.EatenPelletCoords ) {
				tag[ "eaten_pellet_coord_"+i ] = coord;
				i++;
			}

			return tag;
		}


		////////////////

		public override bool PreHurt( bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource ) {
			return !this.IsSpiritWalking;
		}

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( this.player.itemAnimation > 0 && this.player.HeldItem?.type == ModContent.ItemType<ShadowMirrorItem>() ) {
				this.player.itemAnimation = 0;

				Main.NewText( "Mirror use interrupted.", Color.Yellow );
			}
		}
	}
}