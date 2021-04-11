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


		////////////////

		public override void UpdateMusic( ref int music, ref MusicPriority priority ) {
			var plr = Main.LocalPlayer;

			if( Main.myPlayer == -1 || Main.gameMenu || !plr.active ) {
				return;
			}

			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.IsSpiritWalking ) {
				music = this.GetSoundSlot( SoundType.Music, "Sounds/Music/MattsBlues" );
				priority = MusicPriority.BossHigh;
			}
		}
	}
}