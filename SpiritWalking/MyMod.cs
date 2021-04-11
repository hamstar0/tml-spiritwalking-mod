using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SpiritWalking {
	public class SpiritWalkingMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-spiritwalking-mod";


		////////////////
		
		public static SpiritWalkingMod Instance => ModContent.GetInstance<SpiritWalkingMod>();



		////////////////

		internal Mod NecrotisMod = null;




		////////////////

		public override void Load() {
			this.NecrotisMod = ModLoader.GetMod( "Necrotis" );

			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				Main.instance.LoadNPC( NPCID.DungeonSpirit );
			}
		}
	}
}