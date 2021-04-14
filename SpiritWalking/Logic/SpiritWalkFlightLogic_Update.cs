using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static readonly float DefaultFlightSpeed = 5f;
		public static readonly Vector2 DefaultFlightHeading = new Vector2( 0f, -SpiritWalkFlightLogic.DefaultFlightSpeed );



		////////////////

		public static void Update( SpiritWalkingPlayer myplayer ) {
			float accel = 0.1f;

			SpiritWalkFlightLogic.UpdateSpeedChanges( myplayer );

			myplayer.player.velocity = Vector2.Lerp( myplayer.player.velocity, myplayer.FlightDirection, accel );
		}

		private static void UpdateSpeedChanges( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				myplayer.FlightBurstCooldown--;
			}

			if( myplayer.FlightBurstDuration > 1 ) {
				myplayer.FlightBurstDuration--;
			} else if( myplayer.FlightBurstDuration == 1 ) {
				myplayer.FlightBurstDuration = 0;

				SpiritWalkFlightLogic.RevertFlightSpeedScale( myplayer );
			}
		}
	}
}