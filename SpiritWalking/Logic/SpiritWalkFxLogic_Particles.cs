using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void EmitParticles( Vector2 position, Vector2 direction, int particles, bool compensateForHitbox=true ) {
			if( compensateForHitbox ) {
				//position.X -= SpiritWalkLogic.PreWalkPlayerWidth / 2;
				//position.Y -= SpiritWalkLogic.PreWalkPlayerHeight / 2;
				position.Y -= 24;
			}

			for( int i = 0; i < particles; i++ ) {
				int idx = Dust.NewDust(
					Position: position - new Vector2( 16f, 16f ),
					Width: 32,
					Height: 32,
					Type: 180,//DustID.DungeonSpirit,
					SpeedX: direction.X,// + (Main.rand.NextFloat() - 0.5f),
					SpeedY: direction.Y,// + (Main.rand.NextFloat() - 0.5f),
					Alpha: 0,
					newColor: default( Color ),
					Scale: 1.4f
				);
				Main.dust[idx].noGravity = true;
			}
		}
	}
}