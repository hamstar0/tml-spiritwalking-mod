using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using ModLibsAudio.Services.OverlaySounds;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void ActivationFx( SpiritWalkingPlayer myplayer ) {
			if( SpiritWalkFxLogic.FlightSoundLoop == null ) {
				SpiritWalkFxLogic.FlightSoundLoop = OverlaySound.Create(
					sourceMod: SpiritWalkingMod.Instance,
					soundPath: "Sounds/rocket",
					fadeTicks: 0,
					customCondition: SpiritWalkFxLogic.FlightSoundLoopCondition
				);
			}
			SpiritWalkFxLogic.FlightSoundLoop.Play();

			SpiritWalkFxLogic.EmitSpiritParticles(
				position: myplayer.FlightProjectile.Center,
				direction: new Vector2( 0f, 8f ),
				particles: 96,
				radius: 28
			);
		}


		public static void DeactivationFx( Player player ) {
			SpiritWalkFxLogic.FlightSoundLoop.StopImmediately();
		}
	}
}