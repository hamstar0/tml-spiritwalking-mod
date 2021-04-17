using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Projectiles;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void Activate( SpiritWalkingPlayer myplayer ) {
			myplayer.FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;

			int projWho = SpiritWalkFlightLogic.CreateSpiritBall( myplayer );
			myplayer.FlightProjectile = Main.projectile[ projWho ];
		}


		public static void Deactivate( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			myplayer.FlightDirection = SpiritWalkFlightLogic.DefaultFlightHeading;

			myplayer.FlightProjectile.Kill();
			myplayer.FlightProjectile = null;
		}


		////

		private static int CreateSpiritBall( SpiritWalkingPlayer myplayer ) {
			return Projectile.NewProjectile(
				position: myplayer.player.Center,
				velocity: myplayer.FlightDirection,
				Type: ModContent.ProjectileType<SpiritBallProjectile>(),
				Damage: 0,
				KnockBack: 0f,
				Owner: myplayer.player.whoAmI
			);
		}
	}
}