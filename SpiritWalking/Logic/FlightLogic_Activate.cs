using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Projectiles;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void ActivateFlightBehavior( SpiritWalkingPlayer myplayer ) {
			myplayer.IntendedFlightVelocity = SpiritWalkFlightLogic.DefaultFlightHeading;

			int projWho = SpiritWalkFlightLogic.CreateSpiritBall( myplayer );
			myplayer.FlightProjectile = Main.projectile[ projWho ];
		}


		public static void DeactivateFlightBehavior( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			myplayer.IntendedFlightVelocity = SpiritWalkFlightLogic.DefaultFlightHeading;

			if( myplayer.FlightProjectile != null ) {
				myplayer.FlightProjectile.Kill();
				myplayer.FlightProjectile.active = false;
				myplayer.FlightProjectile = null;
			}
		}


		////////////////

		private static int CreateSpiritBall( SpiritWalkingPlayer myplayer ) {
			return Projectile.NewProjectile(
				position: myplayer.player.Center,
				velocity: myplayer.IntendedFlightVelocity,
				Type: ModContent.ProjectileType<SpiritBallProjectile>(),
				Damage: 0,
				KnockBack: 0f,
				Owner: myplayer.player.whoAmI
			);
		}
	}
}