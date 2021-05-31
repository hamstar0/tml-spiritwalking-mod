using Terraria;
using Terraria.GameInput;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static void UpdateForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			if( SpiritWalkLogic.IsUponOpenAir(myplayer.player) ) {
				SpiritWalkFlightLogic.ApplySpiritWalkOpenAirFriction( myplayer.player );
				SpiritWalkFxLogic.ApplySpiritWalkOpenAirFriction( myplayer.player );
			}

			//

			SpiritWalkLogic.UpdateSpiritWalkCost( myplayer.player );

			SpiritWalkLogic.RunFinalDashIf( myplayer );

			//

			myplayer.player.gravity = 0f;

			//

			SpiritWalkFlightLogic.UpdateWalk( myplayer );

			SpiritWalkPelletsLogic.UpdateWalk( myplayer );
		}


		////////////////

		public static void UpdateRunSpeedsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.maxRunSpeed = 0;
			myplayer.player.accRunSpeed = 0;
			myplayer.player.runAcceleration = 0;
		}

		private static void UpdatePlayerFlagsPostBuffsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.noItems = true;
			myplayer.player.immune = true;
			//myplayer.player.stoned = true;
			myplayer.player.noFallDmg = true;
			myplayer.player.gills = true;
			myplayer.player.blackout = true;
		}

		private static void UpdatePlayerFlagsPostMiscForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.manaRegenDelay = 60;
			myplayer.player.manaRegenCount = 0;
		}

		private static void UpdateItemHoldStyleForSpiritWalk( SpiritWalkingPlayer myplayer, Item item ) {
			int oldHoldStyle = item.holdStyle;
			item.holdStyle = 0;

			Timers.RunNow( () => item.holdStyle = oldHoldStyle );
		}

		public static void UpdateTriggersForSpiritWalk( SpiritWalkingPlayer myplayer, TriggersSet triggersSet ) {
			bool jump = triggersSet.Jump;
			bool down = triggersSet.KeyStatus["Down"];
			bool up = triggersSet.KeyStatus["Up"];
			bool left = triggersSet.KeyStatus["Left"];
			bool right = triggersSet.KeyStatus["Right"];

			SpiritWalkLogic.HandleWalkControls( myplayer, jump, down, up, left, right );

			triggersSet.QuickMount = false;
			triggersSet.Grapple = false;
			triggersSet.Jump = false;
		}
	}
}