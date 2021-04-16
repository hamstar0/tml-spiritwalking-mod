using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float frictionDrain = config.Get<float>( nameof( config.PerTickSpiritWalkFrictionAddedEnergyCost ) );

			SpiritWalkLogic.ApplyEnergyDraw( player, frictionDrain );
		}


		////////////////

		public static void SteerFlight( SpiritWalkingPlayer myplayer, bool down, bool up, bool left, bool right ) {
			float rot = 0.03f;

			if( down ) {
				SpiritWalkFlightLogic.ApplyFlightSpeedScaleDownIf( myplayer );
			} else if( up ) {
				SpiritWalkFlightLogic.ApplyFlightSpeedScaleUpIf( myplayer );
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
			myplayer.FlightDirection *= scale;
			myplayer.CurrentFlightScale = scale;

			var config = SpiritWalkingConfig.Instance;
			myplayer.FlightBurstDuration = config.Get<int>( nameof(config.SpiritWalkSpeedChangeTickDuration) );
			myplayer.FlightBurstCooldown = config.Get<int>( nameof(config.SpiritWalkSpeedChangeCooldown) );
		}

		////////////////

		public static void RevertFlightSpeedScale( SpiritWalkingPlayer myplayer ) {
			myplayer.FlightBurstDuration = 0;

			myplayer.FlightDirection /= myplayer.CurrentFlightScale;
			myplayer.CurrentFlightScale = 1f;
		}
	}
}