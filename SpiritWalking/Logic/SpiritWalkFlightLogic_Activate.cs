using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.OverlaySounds;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		private static OverlaySound RocketLoop = null;



		////////////////

		public static void Activate( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			SpiritWalkFlightLogic.RocketLoop = OverlaySound.Create(
				sourceMod: SpiritWalkingMod.Instance,
				soundPath: "Sounds/rocket",
				fadeTicks: 0,
				customCondition: () => (0.2f, 0f, 0f, false)
			);
			SpiritWalkFlightLogic.RocketLoop.Play();

			myplayer.FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;
		}


		public static void Deactivate( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			SpiritWalkFlightLogic.RocketLoop.StopImmediately();

			myplayer.FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;
		}
	}
}