using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;


namespace SpiritWalking {
	partial class SpiritWalkingPlayer : ModPlayer {
		public override void PreUpdate() {
			SpiritWalkLogic.Update( this, this.IsSpiritWalking );
		}

		public override void UpdateDead() {
			if( this.IsSpiritWalking ) {
				SpiritWalkLogic.DeactivateIf( this.player, false );
			}
		}


		////////////////

		public override bool PreItemCheck() {
			return !this.IsSpiritWalking;
		}


		////////////////

		public override void PostUpdateBuffs() {
			SpiritWalkLogic.UpdatePlayerFlagsPostBuffs( this, this.IsSpiritWalking );
		}

		public override void PostUpdateMiscEffects() {
			//public override void PreUpdateBuffs() {
			SpiritWalkLogic.UpdatePlayerFlagsPostMisc( this, this.IsSpiritWalking );
		}

		////

		public override void PostUpdateRunSpeeds() {
			SpiritWalkLogic.UpdateRunSpeeds( this, this.IsSpiritWalking );
		}


		////////////////

		public override void UpdateBiomeVisuals() {
			SpiritWalkFxLogic.UpdateBiomeVisuals( this, this.IsSpiritWalking );
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			SpiritWalkLogic.UpdateTriggers( this, triggersSet, this.IsSpiritWalking );
		}


		////////////////
		
		public override void ModifyDrawLayers( List<PlayerLayer> layers ) {
			SpiritWalkFxLogic.UpdateDrawLayers( this, layers, this.IsSpiritWalking );
		}

		public override void FrameEffects() {
			SpiritWalkFxLogic.UpdateFrameEffects( this, this.IsSpiritWalking );
		}
	}
}