using Terraria;
using Terraria.ModLoader;
using SpiritWalking.Logic;


namespace SpiritWalking {
	class SpiritWalkingItem : GlobalItem {
		public override void HoldStyle( Item item, Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			SpiritWalkLogic.UpdateItemHoldStyle( myplayer, item, myplayer.IsSpiritWalking );
		}

		////

		public override bool CanPickup( Item item, Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			return !myplayer.IsSpiritWalking;
		}

		////

		public override bool CanUseItem( Item item, Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			return !myplayer.IsSpiritWalking;
		}
	}
}