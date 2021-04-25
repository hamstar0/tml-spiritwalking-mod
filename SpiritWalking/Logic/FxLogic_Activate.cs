using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.OverlaySounds;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void ActivationFx( Player player ) {
			SpiritWalkFxLogic.FlightSoundLoop = OverlaySound.Create(
				sourceMod: SpiritWalkingMod.Instance,
				soundPath: "Sounds/rocket",
				fadeTicks: 0,
				customCondition: SpiritWalkFxLogic.FlightSoundLoopCondition
			);
			SpiritWalkFxLogic.FlightSoundLoop.Play();
		}


		public static void DeactivationFx( Player player ) {
			SpiritWalkFxLogic.FlightSoundLoop.StopImmediately();
		}
	}
}