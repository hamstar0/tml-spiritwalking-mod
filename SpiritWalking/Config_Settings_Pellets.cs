using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace SpiritWalking {
	public partial class SpiritWalkingConfig : ModConfig {
		[Range( 0f, 1f )]
		[DefaultValue( 1f / 80f )]
		public float ChanceOfPelletPerTile { get; set; } = 1f / 80f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 5f )]
		public float ChanceOfBadPelletPerPellet { get; set; } = 1f / 5f;

		//

		[Range( 0f, 100f )]
		[DefaultValue( 10f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float GoodPelletAnimaPercentGain { get; set; } = 10f;

		[Range( -100f, 0f )]
		[DefaultValue( -33f )]
		[CustomModConfigItem( typeof(MyFloatInputElement) )]
		public float BadPelletAnimaGain { get; set; } = -33f;


		[Range( 0, 200 )]
		[DefaultValue( 20 )]
		public int GoodPelletManaGain { get; set; } = 20;

		[Range( -200, 0 )]
		[DefaultValue( -80 )]
		public int BadPelletManaGain { get; set; } = -80;
	}
}