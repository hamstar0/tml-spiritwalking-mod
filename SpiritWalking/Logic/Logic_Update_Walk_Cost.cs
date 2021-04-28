using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static int EmulatedMana = 0;


		////////////////

		public static int ManaCostDuration { get; private set; } = 0;



		////////////////

		private static void UpdateSpiritWalkCost( Player player ) {
			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				SpiritWalkLogic.UpdateSpiritWalkCost_Anima( player );
			} else {
				SpiritWalkLogic.UpdateSpiritWalkCost_Mana( player );
			}
		}

		////

		private static void UpdateSpiritWalkCost_Anima( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			float animaPercCost = config.Get<float>( nameof( config.PerTickSpiritWalkAnimaPercentCost ) );

			SpiritWalkLogic.ApplyAnimaDraw( player, animaPercCost );
		}

		private static void UpdateSpiritWalkCost_Mana( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			int tickRate = config.Get<int>( nameof( config.SpiritWalkManaCostTickRate ) );
			if( SpiritWalkLogic.ManaCostDuration++ >= tickRate ) {
				SpiritWalkLogic.ManaCostDuration = 0;
			}

			if( SpiritWalkLogic.ManaCostDuration == 0 ) {
				int manaCost = config.Get<int>( nameof( config.PerRateSpiritWalkManaCost ) );

				SpiritWalkLogic.ApplyManaDraw( player, manaCost );
			}

			player.statMana = SpiritWalkLogic.EmulatedMana;
		}
	}
}