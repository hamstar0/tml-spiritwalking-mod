using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void EmitSpiritParticles(
					Vector2 position,
					Vector2 direction,
					int particles ) {
					//bool wide=false ) {
			int radius = 16;//wide ? 48 : 16;
			float speedScale = 1f;//wide ? 2f : 1f;
			float scale = 1.4f;//wide ? 2.4f : 1.4f;

			for( int i = 0; i < particles; i++ ) {
				int idx = Dust.NewDust(
					Position: position - new Vector2( radius, radius ),
					Width: radius * 2,
					Height: radius * 2,
					Type: 180,//DustID.DungeonSpirit,
					SpeedX: direction.X * speedScale,// + (Main.rand.NextFloat() - 0.5f),
					SpeedY: direction.Y * speedScale,// + (Main.rand.NextFloat() - 0.5f),
					Alpha: 0,
					newColor: default( Color ),
					Scale: scale
				);
				Main.dust[idx].noGravity = true;
			}
		}


		public static void EmitSpiritTrailParticles(
					Vector2 position,
					Vector2 direction = default ) {
			Dust dust = Dust.NewDustPerfect(
				Position: position,
				Type: 6,
				Velocity: direction,
				Alpha: 100,
				newColor: Color.Gold,
				Scale: 2f
			);
			//dust.velocity *= 3f;
			dust.noGravity = true;
			//Dust.QuickDust( offsetPos, Color.Cyan );
		}
	}
}