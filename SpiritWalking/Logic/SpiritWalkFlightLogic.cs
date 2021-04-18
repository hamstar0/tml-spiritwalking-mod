using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static void ApplySpiritWalkOpenAirFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float openAirDrain = config.Get<float>( nameof( config.PerTickSpiritWalkEnergyCostInOpenAir ) );

			SpiritWalkLogic.ApplyEnergyDraw( player, openAirDrain );
		}
		
		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float frictionDrain = config.Get<float>( nameof( config.PerTickSpiritWalkFrictionAddedEnergyCost ) );

			SpiritWalkLogic.ApplyEnergyDraw( player, frictionDrain );
		}
	}
}