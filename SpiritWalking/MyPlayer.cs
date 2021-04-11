using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SpiritWalking.Items;


namespace SpiritWalking {
	public class SpiritWalkingPlayer : ModPlayer {
		internal bool IsSpiritWalking = false;



		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( this.player.itemAnimation > 0 && this.player.HeldItem?.type == ModContent.ItemType<ShadowMirrorItem>() ) {
				this.player.itemAnimation = 0;

				Main.NewText( "Mirror use interrupted.", Color.Yellow );
			}
		}


		////////////////

		public override void PreUpdate() {
			SpiritWalkLogic.Update( this, this.IsSpiritWalking );
		}
	}
}