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
	}
}