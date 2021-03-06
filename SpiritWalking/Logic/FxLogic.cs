using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsAudio.Services.OverlaySounds;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		private static OverlaySound FlightSoundLoop = null;

		private static bool OpenAirDetected = false;
		private static bool CollisionDetected = false;
		


		////////////////
		
		private static (float VolumeOverride, float PanOverride, float PitchOverride, bool IsEnded) FlightSoundLoopCondition() {
			float volume = 0.1f;

			if( SpiritWalkFxLogic.CollisionDetected || SpiritWalkFxLogic.OpenAirDetected ) {
				SpiritWalkFxLogic.OpenAirDetected = false;
				SpiritWalkFxLogic.CollisionDetected = false;

				volume = 0.3f;
			}

			return (volume, 0f, 0f, false);
		}


		////////////////
		
		public static void ApplySpiritWalkOpenAirFriction( Player player ) {
			SpiritWalkFxLogic.OpenAirDetected = true;

			SpiritWalkFxLogic.EmitSpiritParticles(
				position: player.MountedCenter,
				direction: default,
				particles: 2
				//wide: false,
				//offsetY: 24
			);
		}
		
		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			SpiritWalkFxLogic.CollisionDetected = true;

			SpiritWalkFxLogic.EmitSpiritParticles(
				position: player.MountedCenter,
				direction: default,
				particles: 2
				//wide: false,
				//offsetY: -24
			);
		}
	}
}