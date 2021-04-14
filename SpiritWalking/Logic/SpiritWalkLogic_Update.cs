using Terraria;
using Terraria.GameInput;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void Update( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;

			bool isStillSW = SpiritWalkLogic.HasEnergy( myplayer.player, nrgAmtDraw, out string status );

			if( isStillSW ) {
				SpiritWalkLogic.UpdateSpiritWalk( myplayer );
			} else {
				SpiritWalkLogic.DeactivateIf( myplayer.player, true );
			}
		}

		////

		public static void UpdateRunSpeeds( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateRunSpeedsForSpiritWalk( myplayer );
			}
		}

		public static void UpdateBuffs( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateBuffsForSpiritWalk( myplayer );
			}
		}

		public static void UpdateItemHoldStyle( SpiritWalkingPlayer myplayer, Item item, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateItemHoldStyleForSpiritWalk( myplayer, item );
			}
		}

		public static void UpdateTriggers( SpiritWalkingPlayer myplayer, TriggersSet triggersSet, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateTriggersForSpiritWalk( myplayer, triggersSet );
			}
		}
	}
}