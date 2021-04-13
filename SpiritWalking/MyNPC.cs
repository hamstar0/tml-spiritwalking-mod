using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;


namespace SpiritWalking {
	class SpiritWalkingNPC : GlobalNPC {
		public override void EditSpawnPool( IDictionary<int, float> pool, NPCSpawnInfo spawnInfo ) {
			var myplayer = spawnInfo.player.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.IsSpiritWalking ) {
				pool.Clear();
			}
		}
	}
}