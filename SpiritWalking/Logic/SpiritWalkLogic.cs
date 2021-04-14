using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
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
	}
}