using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace SpiritWalking {
	public partial class SpiritWalkingConfig : ModConfig {
		public static bool SpiritWalkUsesAnima =>
			SpiritWalkingConfig.Instance.Get<bool>( nameof(SpiritWalkingConfig.SpiritWalkUsesAnimaIfNecrotisAvailable) )
				&& ModLoader.GetMod( "Necrotis" ) != null;



		////////////////

		[DefaultValue( true )]
		public bool SpiritWalkUsesAnimaIfNecrotisAvailable { get; set; } = true;

		//

		[Range( 0, 400 )]
		[DefaultValue( 20 )]
		public int InitialSpiritWalkManaCost { get; set; } = 20;

		[Range( 0, 100 )]
		[DefaultValue( 1 )]
		public int SpiritWalkManaCostTickRate { get; set; } = 1;

		[Range( 0, 100 )]
		[DefaultValue( 1 )]
		public int PerRateSpiritWalkManaCost { get; set; } = 1;

		[Range( 0, 100 )]
		[DefaultValue( 3 )]
		public int PerRateSpiritWalkManaCostInOpenAir { get; set; } = 3;

		[Range( 0, 100 )]
		[DefaultValue( 2 )]
		public int PerRateSpiritWalkFrictionAddedManaCost { get; set; } = 2;

		//

		[Range( 0f, 400f )]
		[DefaultValue( 20f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float InitialSpiritWalkAnimaPercentCost { get; set; } = 20f;

		[Range( 0f, 100f )]
		[DefaultValue( 1f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkAnimaPercentCost { get; set; } = 1f / 60f;

		[Range( 0f, 100f )]
		[DefaultValue( 3f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkAnimaPercentCostInOpenAir { get; set; } = 3f / 60f;

		[Range( 0f, 100f )]
		[DefaultValue( 2f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkFrictionAddedAnimaPercentCost { get; set; } = 2f / 60f;
	}
}