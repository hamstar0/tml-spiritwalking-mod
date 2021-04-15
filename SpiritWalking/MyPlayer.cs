using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;
using SpiritWalking.Items;


namespace SpiritWalking {
	public partial class SpiritWalkingPlayer : ModPlayer {
		internal bool IsSpiritWalking = false;


		internal Vector2 FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;

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


		////////////////

		public override void PostUpdateRunSpeeds() {
			SpiritWalkLogic.UpdateRunSpeeds( this, this.IsSpiritWalking );
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			SpiritWalkLogic.UpdateTriggers( this, triggersSet, this.IsSpiritWalking );
		}


		////////////////

		public override void ModifyDrawLayers( List<PlayerLayer> layers ) {
			SpiritWalkFxLogic.ModifyDrawLayers( layers, this.IsSpiritWalking );
		}

		public override void FrameEffects() {
			SpiritWalkFxLogic.FrameEffects( this, this.IsSpiritWalking );
		}

		public override void UpdateBiomeVisuals() {
			SpiritWalkFxLogic.UpdateBiomeVisuals( this, this.IsSpiritWalking );
		}
	}
}