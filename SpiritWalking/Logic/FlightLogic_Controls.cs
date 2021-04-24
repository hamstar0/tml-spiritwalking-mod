using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void SteerFlight( SpiritWalkingPlayer myplayer, bool down, bool up, bool left, bool right ) {
			float rotStep = 0.03f;
			bool isFinalDash = myplayer.FinalDashElapsed > 0;

			if( !isFinalDash ) {
				if( down ) {
					SpiritWalkFlightLogic.ApplyFlightSpeedBrakeIf( myplayer );
				} else if( up ) {
					SpiritWalkFlightLogic.ApplyFlightSpeedBoostIf( myplayer );
				}
			}

			if( left ) {
				myplayer.IntendedFlightVelocity = myplayer.IntendedFlightVelocity.RotatedBy( -rotStep );
			} else if( right ) {
				myplayer.IntendedFlightVelocity = myplayer.IntendedFlightVelocity.RotatedBy( rotStep );
			}
		}


		////////////////
		
		public static void ApplyFlightSpeedBrakeIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				return;
			}

			var config = SpiritWalkingConfig.Instance;
			float brake = config.Get<float>( nameof(config.SpiritWalkBrakeSpeedMultiplier) );

			SpiritWalkFlightLogic.ApplyFlightSpeedScaleChange( myplayer, brake );

			Main.PlaySound( SoundID.DoubleJump );

			SpiritWalkFxLogic.EmitSpiritParticles(
				position: myplayer.FlightProjectile.Center,
				direction: myplayer.FlightProjectile.velocity * 2f,
				particles: 20
			);
		}

		public static void ApplyFlightSpeedBoostIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				return;
			}
			
			var config = SpiritWalkingConfig.Instance;
			float boost = config.Get<float>( nameof(config.SpiritWalkBoostSpeedMultiplier) );

			SpiritWalkFlightLogic.ApplyFlightSpeedScaleChange( myplayer, boost );

			Main.PlaySound( SoundID.Grass );

			SpiritWalkFxLogic.EmitSpiritParticles(
				position: myplayer.FlightProjectile.Center,
				direction: myplayer.FlightProjectile.velocity * -2f,
				particles: 20
			);
		}

		////

		public static void ApplyFlightSpeedScaleChange( SpiritWalkingPlayer myplayer, float scale ) {
			float speed = SpiritWalkFlightLogic.DefaultFlightSpeed * scale;

			myplayer.CurrentFlightSpeedScale = scale;

			var config = SpiritWalkingConfig.Instance;
			myplayer.FlightBurstDuration = config.Get<int>( nameof(config.SpiritWalkSpeedChangeTickDuration) );
			myplayer.FlightBurstCooldown = config.Get<int>( nameof(config.SpiritWalkSpeedChangeCooldown) );
		}

		////////////////

		public static void RevertFlightSpeedScale( SpiritWalkingPlayer myplayer ) {
			myplayer.FlightBurstDuration = 0;

			myplayer.CurrentFlightSpeedScale = 1f;
		}
	}
}