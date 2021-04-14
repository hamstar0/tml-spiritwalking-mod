using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static readonly float DefaultFlightSpeed = 5f;
		public static readonly Vector2 DefaultFlightHeading = new Vector2( 0f, -SpiritWalkLogic.DefaultFlightSpeed );



		////////////////

		private static void UpdateSpiritWalkFlight( SpiritWalkingPlayer myplayer ) {
			float accel = 0.1f;

			SpiritWalkLogic.UpdateSpiritWalkFlightSpeedChanges( myplayer );

			myplayer.player.velocity = Vector2.Lerp( myplayer.player.velocity, myplayer.FlightDirection, accel );
		}

		private static void UpdateSpiritWalkFlightSpeedChanges( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				myplayer.FlightBurstCooldown--;
			}

			if( myplayer.FlightBurstDuration > 1 ) {
				myplayer.FlightBurstDuration--;
			} else if( myplayer.FlightBurstDuration == 1 ) {
				myplayer.FlightBurstDuration = 0;

				SpiritWalkLogic.RevertFlightSpeedScale( myplayer );
			}
		}
	}
}