using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static void UpdateSpiritWalk( SpiritWalkingPlayer myplayer ) {
			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;

			SpiritWalkLogic.ApplyEnergyDraw( myplayer.player, nrgAmtDraw );

			myplayer.player.gravity = 0f;

			SpiritWalkLogic.UpdateSpiritWalkFlight( myplayer );
		}

		////

		private static void UpdateBuffsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.noItems = true;
			myplayer.player.immune = true;
			//myplayer.player.stoned = true;
		}

		private static void UpdateItemHoldStyleForSpiritWalk( SpiritWalkingPlayer myplayer, Item item ) {
			int oldHoldStyle = item.holdStyle;
			item.holdStyle = 0;

			Timers.RunNow( () => item.holdStyle = oldHoldStyle );
		}
	}
}