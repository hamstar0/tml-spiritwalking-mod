using System;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static bool IsPelletNearPlayer( int tileX, int tileY, bool isBad ) {
			int plrWldX = (int)Main.LocalPlayer.Center.X;
			int plrWldY = (int)Main.LocalPlayer.Center.Y;

			int diffX = plrWldX - ( tileX * 16 );
			int diffY = plrWldY - ( tileY * 16 );
			int distSqr = ( diffX * diffX ) + ( diffY * diffY );

			if( isBad ) {
				return distSqr < 900;  //30
			} else {
				return distSqr < 400;   //20
			}
		}


		////////////////

		public static void PickupPelletIf( int tileX, int tileY, bool isBad ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<SpiritWalkingPlayer>();
			if( myplayer.NoPelletPickupDuration > 0 ) {
				return;
			}

			int coord = tileX + (tileY << 16);
			var pelletPos = new Vector2( (tileX * 16) + 8, (tileY * 16) + 8 );

			if( isBad ) {
				SpiritWalkPelletsLogic.ApplyBadPellet();
				SpiritWalkFxLogic.EmitBadPelletParticles( pelletPos );
			} else {
				SpiritWalkPelletsLogic.ApplyGoodPellet();
				SpiritWalkFxLogic.EmitGoodPelletParticles( pelletPos );
			}

			myplayer.EatenPelletCoords.Add( coord );

			//

			SpiritWalkPelletsLogic.CachedRevealedPellets.Remove( (ulong)coord );
		}


		////

		public static void ApplyGoodPellet() {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaLargePercGain = config.Get<float>( nameof(config.GoodPelletAnimaPercentGain) );
				
				SpiritWalkPelletsLogic.ApplyPelletGain_Necrotis( animaLargePercGain );
			} else {
				int manaGain = config.Get<int>( nameof(config.GoodPelletManaGain) );

				Main.LocalPlayer.statMana += manaGain;
				Main.LocalPlayer.ManaEffect( manaGain );
			}
		}

		public static void ApplyBadPellet() {
			var config = SpiritWalkingConfig.Instance;
			
			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaLargePercGain = config.Get<float>( nameof(config.BadPelletAnimaGain) );

				SpiritWalkPelletsLogic.ApplyPelletGain_Necrotis( animaLargePercGain );
			} else {
				int manaGain = config.Get<int>( nameof(config.BadPelletManaGain) );

				Main.LocalPlayer.statMana += manaGain;
				Main.LocalPlayer.ManaEffect( manaGain );
			}
		}


		////////////////

		private static void ApplyPelletGain_Necrotis( float largePercentGain ) {
			float percGain = largePercentGain / 100f;

			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer( Main.LocalPlayer, -percGain, false );
		}
	}
}