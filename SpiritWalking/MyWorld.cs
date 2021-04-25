using System;
using SpiritWalking.Logic;
using Terraria;
using Terraria.ModLoader;


namespace SpiritWalking {
	class SpiritWalkingWorld : ModWorld {
		public override void PostDrawTiles() {
			if( !Main.LocalPlayer.GetModPlayer<SpiritWalkingPlayer>().IsSpiritWalking ) {
				return;
			}

			int scrMinX = (int)Main.screenPosition.X / 16;
			int scrMinY = (int)Main.screenPosition.Y / 16;
			int scrWid = Main.screenWidth / 16;
			int scrHei = Main.screenHeight / 16;
			int scrMaxX = scrMinX + scrWid;
			int scrMaxY = scrMinY + scrHei;

			for( int i=scrMinX; i<scrMaxX; i++ ) {
				for( int j=scrMinY; j<scrMaxY; j++ ) {
					(bool isPellet, bool isBad) pellet = SpiritWalkPelletsLogic.DrawPelletIf( i, j );
					
					if( SpiritWalkPelletsLogic.IsPelletNearPlayer(i, j, pellet.isBad) ) {
						SpiritWalkPelletsLogic.PickupPellet( i, j, pellet.isBad );
					}
				}
			}
		}
	}
}
