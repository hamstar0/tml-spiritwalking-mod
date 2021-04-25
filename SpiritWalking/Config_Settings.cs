using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace SpiritWalking {
	public partial class SpiritWalkingConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;
		
		
		////////////////
		
		[DefaultValue( true )]
		public bool ShadowMirrorRecipeEnabled { get; set; } = true;

		//

		public bool OpenAirAllowsEngagingSpiritWalk { get; set; } = false;

		//

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 60f )]
		public float ChanceOfPelletPerTile { get; set; } = 1f / 60f;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 4f )]
		public float ChanceOfBadPelletPerPellet { get; set; } = 1f / 4f;
	}
}