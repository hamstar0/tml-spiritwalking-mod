using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static readonly float DefaultFlightSpeed = 5f;
		public static readonly Vector2 DefaultFlightHeading = new Vector2( 0f, -SpiritWalkFlightLogic.DefaultFlightSpeed );



		////////////////

		public static void UpdateWalk( SpiritWalkingPlayer myplayer ) {
			SpiritWalkFlightLogic.UpdateSpeedChanges( myplayer );

			if( myplayer.FlightProjectile?.active != true ) {
				int projWho = SpiritWalkFlightLogic.CreateSpiritBall( myplayer );
				myplayer.FlightProjectile = Main.projectile[ projWho ];
			}

			if( myplayer.FlightProjectile?.active == true ) {
				myplayer.player.Center = myplayer.FlightProjectile.Center;
				myplayer.player.velocity = default;
			}
			//float accel = 0.1f;
			//myplayer.player.velocity = Vector2.Lerp( myplayer.player.velocity, myplayer.FlightDirection, accel );
		}

		////

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