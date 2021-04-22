using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace SpiritWalking {
	public partial class SpiritWalkingConfig : ModConfig {
		[Range( 0f, 400f )]
		[DefaultValue( 20f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float InitialSpiritWalkEnergyCost { get; set; } = 20f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkEnergyCost { get; set; } = 1f / 60f;

		[Range( 0f, 100f )]
		[DefaultValue( 3f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkEnergyCostInOpenAir { get; set; } = 3f / 60f;

		[Range( 0f, 100f )]
		[DefaultValue( 2f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkFrictionAddedEnergyCost { get; set; } = 2f / 60f;
	}
}