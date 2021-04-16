using Terraria;
using Terraria.GameInput;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static void UpdateSpiritWalk( SpiritWalkingPlayer myplayer ) {
			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;

			SpiritWalkLogic.ApplyEnergyDraw( myplayer.player, nrgAmtDraw );

			myplayer.player.gravity = 0f;
			myplayer.player.width = 0;
			myplayer.player.height = 0;

			SpiritWalkFlightLogic.Update( myplayer );
		}

		////

		public static void UpdateRunSpeedsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.maxRunSpeed = 0;
			myplayer.player.accRunSpeed = 0;
			myplayer.player.runAcceleration = 0;
		}

		private static void UpdateFlagsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.noItems = true;
			myplayer.player.immune = true;
			//myplayer.player.stoned = true;
			myplayer.player.noFallDmg = true;
		}

		private static void UpdateItemHoldStyleForSpiritWalk( SpiritWalkingPlayer myplayer, Item item ) {
			int oldHoldStyle = item.holdStyle;
			item.holdStyle = 0;

			Timers.RunNow( () => item.holdStyle = oldHoldStyle );
		}

		public static void UpdateTriggersForSpiritWalk( SpiritWalkingPlayer myplayer, TriggersSet triggersSet ) {
			bool down = triggersSet.KeyStatus["Down"];
			bool up = triggersSet.KeyStatus["Up"];
			bool left = triggersSet.KeyStatus["Left"];
			bool right = triggersSet.KeyStatus["Right"];
			
			if( down || up || left || right ) {
				SpiritWalkFlightLogic.SteerFlight( myplayer, down, up, left, right );
			}

			triggersSet.QuickMount = false;
			triggersSet.Grapple = false;
			triggersSet.Jump = false;
		}
	}
}