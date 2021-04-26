using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void EmitSpiritParticles(
					Vector2 position,
					Vector2 direction,
					int particles,
					int radius = 16 ) {
					//bool wide=false ) {
			//int radius = 16;//wide ? 48 : 16;
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


		////

		public static void EmitGoodPelletParticles( Vector2 position, bool playSound=true ) {
			for( int i = 0; i < 16; i++ ) {
				int idx = Dust.NewDust(
					Position: position - new Vector2(8, 8),
					Width: 16,
					Height: 16,
					Type: 59,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 0,
					newColor: Color.Cyan,
					Scale: 2f
				);
				Main.dust[idx].fadeIn = 2.5f;
				Main.dust[idx].noGravity = true;
			}

			if( playSound ) {
				Main.PlaySound( SoundID.MenuTick, position );
			}
		}

		public static void EmitBadPelletParticles( Vector2 position, bool playSound=true ) {
			for( int i = 0; i < 24; i++ ) {
				int idx = Dust.NewDust(
					Position: position - new Vector2(12, 12),
					Width: 24,
					Height: 24,
					Type: 60,
					SpeedX: 0f,
					SpeedY: 0f,
					Alpha: 0,
					newColor: Color.Red,
					Scale: 2f
				);
				Main.dust[idx].fadeIn = 2.5f;
				Main.dust[idx].noGravity = true;
			}

			if( playSound ) {
				Main.PlaySound( SoundID.MenuTick, position );
			}
		}
	}
}