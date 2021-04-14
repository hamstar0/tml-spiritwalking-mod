using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void SteerFlight( SpiritWalkingPlayer myplayer, bool down, bool up, bool left, bool right ) {
			float rot = 0.03f;

			if( down ) {
				SpiritWalkLogic.ApplyFlightSpeedScaleDownIf( myplayer );
			} else if( up ) {
				SpiritWalkLogic.ApplyFlightSpeedScaleUpIf( myplayer );
			}

			if( left ) {
				myplayer.FlightDirection = myplayer.FlightDirection.RotatedBy( -rot );
			} else if( right ) {
				myplayer.FlightDirection = myplayer.FlightDirection.RotatedBy( rot );
			}
		}

		////////////////

		public static void ApplyFlightSpeedScaleDownIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				return;
			}

			Main.PlaySound( SoundID.DoubleJump );

			SpiritWalkLogic.ApplyFlightSpeedScaleChange( myplayer, 0.5f );
		}

		public static void ApplyFlightSpeedScaleUpIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				return;
			}

			Main.PlaySound( SoundID.Grass );

			SpiritWalkLogic.ApplyFlightSpeedScaleChange( myplayer, 2f );
		}

		public static void ApplyFlightSpeedScaleChange( SpiritWalkingPlayer myplayer, float scale ) {
			myplayer.FlightDirection *= scale;
			myplayer.CurrentFlightScale = scale;

			var config = SpiritWalkingConfig.Instance;
			myplayer.FlightBurstDuration = config.Get<int>( nameof(config.SpiritWalkSpeedChangeTickDuration) );
			myplayer.FlightBurstCooldown = config.Get<int>( nameof(config.SpiritWalkSpeedChangeCooldown) );
		}

		////

		public static void RevertFlightSpeedScale( SpiritWalkingPlayer myplayer ) {
			myplayer.FlightBurstDuration = 0;

			myplayer.FlightDirection /= myplayer.CurrentFlightScale;
			myplayer.CurrentFlightScale = 1f;
		}
	}
}