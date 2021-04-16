using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.OverlaySounds;
using SpiritWalking.Projectiles;


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

			int projWho = Projectile.NewProjectile(
				position: player.Center,
				velocity: SpiritWalkFlightLogic.DefaultFlightHeading,
				Type: ModContent.ProjectileType<SpiritBallProjectile>(),
				Damage: 0,
				KnockBack: 0f,
				Owner: player.whoAmI
			);
			myplayer.FlightProjectile = Main.projectile[ projWho ];
		}


		public static void Deactivate( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			SpiritWalkFlightLogic.RocketLoop.StopImmediately();

			myplayer.FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;

			myplayer.FlightProjectile.Kill();
			myplayer.FlightProjectile = null;
		}
	}
}