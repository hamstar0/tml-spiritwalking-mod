using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace SpiritWalking {
	public partial class SpiritWalkingConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;
		
		
		////////////////
		
		[DefaultValue( true )]
		public bool ShadowMirrorRecipeEnabled { get; set; } = true;

		//

		[DefaultValue( true )]
		public bool SpiritWalkUsesAnimaIfNecrotisAvailable { get; set; } = true;

		//

		public bool OpenAirAllowsEngagingSpiritWalk { get; set; } = false;
	}
}