using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static void PickupPellet( int tileX, int tileY, bool isBad ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<SpiritWalkingPlayer>();
			int coord = tileX + (tileY << 16);

			if( isBad ) {
				SpiritWalkPelletsLogic.ApplyBadPellet();
			} else {
				SpiritWalkPelletsLogic.ApplyGoodPellet();
			}

			myplayer.EatenPelletCoords.Add( coord );

			SpiritWalkPelletsLogic.FlushCache();
		}


		////

		public static void ApplyGoodPellet() {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaGain = config.Get<float>( nameof(config.GoodPelletAnimaPercentGain) );
				
				SpiritWalkPelletsLogic.ApplyPelletGain_Necrotis( animaGain );
			} else {
				int manaGain = config.Get<int>( nameof(config.GoodPelletManaGain) );

				Main.LocalPlayer.statMana += manaGain;
				Main.LocalPlayer.ManaEffect( manaGain );
			}
		}

		public static void ApplyBadPellet() {
			var config = SpiritWalkingConfig.Instance;
			
			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaGain = config.Get<float>( nameof(config.BadPelletAnimaGain) );

				SpiritWalkPelletsLogic.ApplyPelletGain_Necrotis( animaGain );
			} else {
				int manaGain = config.Get<int>( nameof(config.BadPelletManaGain) );

				Main.LocalPlayer.statMana += manaGain;
				Main.LocalPlayer.ManaEffect( manaGain );
			}
		}


		////////////////

		private static void ApplyPelletGain_Necrotis( float gain ) {
			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer( Main.LocalPlayer, -gain, false );
		}
	}
}