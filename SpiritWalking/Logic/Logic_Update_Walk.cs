using Terraria;
using Terraria.GameInput;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static int ManaCostDuration { get; private set; } = 0;



		////////////////

		private static void ApplySpiritWalkCost( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaPercCost = config.Get<float>( nameof(config.PerTickSpiritWalkAnimaPercentCost) );

				SpiritWalkLogic.ApplyAnimaDraw( player, animaPercCost );
			} else {
				if( SpiritWalkLogic.ManaCostDuration == 0 ) {
					int manaCost = config.Get<int>( nameof(config.PerRateSpiritWalkManaCost) );

					SpiritWalkLogic.ApplyManaDraw( player, manaCost );
				}
			}
		}


		////

		private static void UpdateForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			var config = SpiritWalkingConfig.Instance;

			//

			if( SpiritWalkLogic.IsUponOpenAir(myplayer.player) ) {
				SpiritWalkFlightLogic.ApplySpiritWalkOpenAirFriction( myplayer.player );
				SpiritWalkFxLogic.ApplySpiritWalkOpenAirFriction( myplayer.player );
			}

			//

			int tickRate = config.Get<int>( nameof(config.SpiritWalkManaCostTickRate) );
			if( SpiritWalkLogic.ManaCostDuration++ >= tickRate ) {
				SpiritWalkLogic.ManaCostDuration = 0;
			}

			SpiritWalkLogic.ApplySpiritWalkCost( myplayer.player );

			//

			SpiritWalkLogic.RunFinalDashIf( myplayer );

			//

			myplayer.player.gravity = 0f;

			//

			SpiritWalkFlightLogic.UpdateWalk( myplayer );

			SpiritWalkPelletsLogic.UpdateWalk( myplayer );
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
			myplayer.player.gills = true;
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