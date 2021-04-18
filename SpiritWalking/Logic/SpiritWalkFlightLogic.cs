using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void ApplySpiritWalkOpenAirFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float openAirDrain = config.Get<float>( nameof( config.PerTickSpiritWalkEnergyCostInOpenAir ) );

			SpiritWalkLogic.ApplyEnergyDraw( player, openAirDrain );
		}
		
		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float frictionDrain = config.Get<float>( nameof( config.PerTickSpiritWalkFrictionAddedEnergyCost ) );

			SpiritWalkLogic.ApplyEnergyDraw( player, frictionDrain );
		}


		////////////////

		public static void SteerFlight( SpiritWalkingPlayer myplayer, bool down, bool up, bool left, bool right ) {
			float rotStep = 0.03f;

			if( down ) {
				SpiritWalkFlightLogic.ApplyFlightSpeedScaleDownIf( myplayer );
			} else if( up ) {
				SpiritWalkFlightLogic.ApplyFlightSpeedScaleUpIf( myplayer );
			}

			if( left ) {
				myplayer.IntendedFlightVelocity = myplayer.IntendedFlightVelocity.RotatedBy( -rotStep );
			} else if( right ) {
				myplayer.IntendedFlightVelocity = myplayer.IntendedFlightVelocity.RotatedBy( rotStep );
			}
		}


		////////////////
		
		public static void ApplyFlightSpeedScaleDownIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				return;
			}

			Main.PlaySound( SoundID.DoubleJump );

			SpiritWalkFlightLogic.ApplyFlightSpeedScaleChange( myplayer, 0.5f );

			SpiritWalkFxLogic.EmitParticles(
				myplayer.player.MountedCenter,
				Vector2.Normalize( myplayer.player.velocity ) * 12,
				16
			);
		}

		public static void ApplyFlightSpeedScaleUpIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FlightBurstCooldown > 0 ) {
				return;
			}

			Main.PlaySound( SoundID.Grass );

			SpiritWalkFlightLogic.ApplyFlightSpeedScaleChange( myplayer, 2f );

			SpiritWalkFxLogic.EmitParticles(
				myplayer.player.MountedCenter,
				Vector2.Normalize( -myplayer.player.velocity ) * 12,
				16
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