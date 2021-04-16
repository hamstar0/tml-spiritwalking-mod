using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.OverlaySounds;
using SpiritWalking.Projectiles;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void Activate( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

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

			myplayer.FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;

			myplayer.FlightProjectile.Kill();
			myplayer.FlightProjectile = null;
		}
	}
}