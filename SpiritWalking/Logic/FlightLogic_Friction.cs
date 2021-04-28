using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void ApplySpiritWalkOpenAirFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				SpiritWalkFlightLogic.ApplySpiritWalkOpenAirFriction_Anima( player );
			} else {
				SpiritWalkFlightLogic.ApplySpiritWalkOpenAirFriction_Mana( player );
			}
		}

		////

		private static void ApplySpiritWalkOpenAirFriction_Anima( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float animaPercDraw = config.Get<float>( nameof(config.PerTickSpiritWalkAnimaPercentCostInOpenAir) );

			SpiritWalkLogic.ApplyAnimaDraw( player, animaPercDraw );
		}

		public static void ApplySpiritWalkOpenAirFriction_Mana( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			if( SpiritWalkLogic.ManaCostDuration > 0 ) {
				return;
			}

			int manaDraw = config.Get<int>( nameof( config.PerRateSpiritWalkManaCostInOpenAir ) );
			SpiritWalkLogic.ApplyManaDraw( player, manaDraw );
		}

		////////////////

		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				SpiritWalkFlightLogic.ApplySpiritWalkCollisionFriction_Anima( player );
			} else {
				SpiritWalkFlightLogic.ApplySpiritWalkCollisionFriction_Mana( player );
			}
		}

		////

		private static void ApplySpiritWalkCollisionFriction_Anima( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float animaPercDraw = config.Get<float>( nameof(config.PerTickSpiritWalkFrictionAddedAnimaPercentCost) );

			SpiritWalkLogic.ApplyAnimaDraw( player, animaPercDraw );
		}

		private static void ApplySpiritWalkCollisionFriction_Mana( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			if( SpiritWalkLogic.ManaCostDuration > 0 ) {
				return;
			}

			int manaDraw = config.Get<int>( nameof(config.PerRateSpiritWalkFrictionAddedManaCost) );
			SpiritWalkLogic.ApplyManaDraw( player, manaDraw );
		}
	}
}