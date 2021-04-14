using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;
using SpiritWalking.Items;


namespace SpiritWalking {
	public partial class SpiritWalkingPlayer : ModPlayer {
		internal bool IsSpiritWalking = false;


		internal Vector2 FlightDirection = SpiritWalkLogic.DefaultFlightHeading;

		internal float CurrentFlightScale = 1f;


		internal int FlightBurstCooldown = 0;

		internal int FlightBurstDuration = 0;



		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( this.player.itemAnimation > 0 && this.player.HeldItem?.type == ModContent.ItemType<ShadowMirrorItem>() ) {
				this.player.itemAnimation = 0;

				Main.NewText( "Mirror use interrupted.", Color.Yellow );
			}
		}


		////////////////

		public override void PreUpdate() {
			SpiritWalkLogic.Update( this, this.IsSpiritWalking );
		}

		public override void PostUpdateBuffs() {
			SpiritWalkLogic.UpdateBuffs( this, this.IsSpiritWalking );
		}

		public override void PostItemCheck() {
			if( this.IsSpiritWalking ) {
				if( this.player.mount.Active ) {
					this.player.mount.Dismount( this.player );
				}
			}
		}


		////////////////

		public override void ModifyDrawLayers( List<PlayerLayer> layers ) {
			if( this.IsSpiritWalking ) {
				layers.ForEach( l => l.visible = false );

				layers.Add( new PlayerLayer( this.mod.Name, "Spirit Walker", this.DrawSpirit ) );
			}
		}

		public override void FrameEffects() {
			if( this.IsSpiritWalking ) {
				this.player.head = 0;
				this.player.body = 0;
				this.player.legs = 0;

				for( int i = 0; i < 1; i++ ) {
					int idx = Dust.NewDust(
						Position: this.player.MountedCenter + new Vector2(-16, -16),
						Width: 32,
						Height: 32,
						Type: DustID.DungeonSpirit,
						SpeedX: Main.rand.NextFloat() - 0.5f,
						SpeedY: Main.rand.NextFloat() - 0.5f,
						Alpha: 0,
						newColor: default( Color ),
						Scale: 1.4f
					);
					Main.dust[idx].noGravity = true;
				}
			}
		}

		public override void UpdateBiomeVisuals() {
			bool isMLFilterActive = Filters.Scene["Vortex"].IsActive();

			if( this.IsSpiritWalking ) {
				if( !isMLFilterActive ) {
					Filters.Scene.Activate( "Vortex" );
				}
			} else if( isMLFilterActive ) {
				Filters.Scene["Vortex"].Deactivate( new object[0] );
			}
		}

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			SpiritWalkLogic.UpdateTriggers( this, triggersSet, this.IsSpiritWalking );
		}


		public override void PostUpdateRunSpeeds() {
			SpiritWalkLogic.UpdateRunSpeeds( this, this.IsSpiritWalking );
		}
	}
}