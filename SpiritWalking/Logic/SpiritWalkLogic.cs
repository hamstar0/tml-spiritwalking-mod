using Microsoft.Xna.Framework;
using Terraria;


namespace SpiritWalking.Items {
	internal partial class SpiritWalkLogic {
		public static void ApplyEnergyDraw( Player player, float energyDraw ) {
			var config = SpiritWalkingConfig.Instance;
			bool swUsesAnima = config.SpiritWalkUsesAnimaIfNecrotisAvailable
				&& SpiritWalkingMod.Instance.NecrotisMod != null;

			if( swUsesAnima ) {
				SpiritWalkLogic.ApplyEnergyDraw_Necrotis( player, energyDraw );
			} else {
				player.statMana -= (int)energyDraw;
			}
		}
		
		private static void ApplyEnergyDraw_Necrotis( Player player, float energyDraw ) {
			float energyAsPercent = energyDraw / 100f;

			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer( player, energyAsPercent, false );
		}


		////

		public static void Update( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;
			
			bool isStillSW = SpiritWalkLogic.HasEnergy( myplayer.player, nrgAmtDraw, out string status );

			if( isStillSW ) {
				SpiritWalkLogic.ApplyEnergyDraw( myplayer.player, nrgAmtDraw );
			} else {
				SpiritWalkLogic.DeactivateIf( myplayer.player );
			}
		}
	}
}