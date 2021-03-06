using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void SteerFlight( SpiritWalkingPlayer myplayer, bool down, bool up, bool left, bool right ) {
			bool isFinalDash = myplayer.FinalDashElapsed > 0;

			if( !isFinalDash ) {
				if( down ) {
					SpiritWalkFlightLogic.ApplyFlightSpeedBrakeIf( myplayer );
				} else if( up ) {
					SpiritWalkFlightLogic.ApplyFlightSpeedBoostIf( myplayer );
				}
			}

			var config = SpiritWalkingConfig.Instance;
			float rotStep = config.Get<float>( nameof(config.SpiritWalkSteering) );	//0.03f;

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