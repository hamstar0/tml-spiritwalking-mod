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

			myplayer.player.velocity = Vector2.Lerp( myplayer.player.velocity, myplayer.FlightDirection, accel );
		}
	}
}