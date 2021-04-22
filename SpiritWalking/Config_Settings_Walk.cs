using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace SpiritWalking {
	public partial class SpiritWalkingConfig : ModConfig {
		[Range( 0, 60*60 )]
		[DefaultValue( 60 )]
		public int SpiritWalkSpeedChangeTickDuration { get; set; } = 60;

		[Range( 0, 60 * 60 * 5 )]
		[DefaultValue( (int)(60f * 2.5f) )]
		public int SpiritWalkSpeedChangeCooldown { get; set; } = (int)(60f * 2.5f);

		[Range( 0f, 100f )]
		[DefaultValue( 0.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SpiritWalkBrakeSpeedMultiplier { get; set; } = 0.5f;

		[Range( 0f, 100f )]
		[DefaultValue( 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SpiritWalkBoostSpeedMultiplier { get; set; } = 2f;

		
		[Range( 0, 128 )]
		[DefaultValue( 12 )]
		public int FinalDashTileDistance { get; set; } = 12;
	}
}