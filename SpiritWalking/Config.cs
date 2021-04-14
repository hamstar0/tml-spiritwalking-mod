using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace SpiritWalking {
	class MyFloatInputElement : FloatInputElement { }
	//[CustomModConfigItem( typeof( MyFloatInputElement ) )]




	public partial class SpiritWalkingConfig : ModConfig {
		public static SpiritWalkingConfig Instance => ModContent.GetInstance<SpiritWalkingConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////
		
		[DefaultValue( true )]
		public bool ShadowMirrorRecipeEnabled { get; set; } = true;
		
		
		[DefaultValue( true )]
		public bool SpiritWalkUsesAnimaIfNecrotisAvailable { get; set; } = true;
		
		
		[Range( 0f, 400f )]
		[DefaultValue( 20f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float InitialSpiritWalkEnergyCost { get; set; } = 20f;


		[Range( 0f, 100f )]
		[DefaultValue( 1f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PerTickSpiritWalkEnergyCost { get; set; } = 1f / 60f;


		[Range( 0, 60*60 )]
		[DefaultValue( 60 )]
		public int SpiritWalkSpeedChangeTickDuration { get; set; } = 60;

		[Range( 0, 60 * 60 * 5 )]
		[DefaultValue( (int)(60f * 2.5f) )]
		public int SpiritWalkSpeedChangeCooldown { get; set; } = (int)(60f * 2.5f);
	}
}