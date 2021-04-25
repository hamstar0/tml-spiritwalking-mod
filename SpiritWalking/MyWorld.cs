using System;
using Microsoft.Xna.Framework;
using SpiritWalking.Logic;
using Terraria;
using Terraria.ModLoader;


namespace SpiritWalking {
	class SpiritWalkingWorld : ModWorld {
		public override void PostDrawTiles() {
			int scrMinX = (int)Main.screenPosition.X / 16;
			int scrMinY = (int)Main.screenPosition.Y / 16;
			int scrWid = Main.screenWidth / 16;
			int scrHei = Main.screenHeight / 16;
			int scrMaxX = scrMinX + scrWid;
			int scrMaxY = scrMinY + scrHei;

			for( int i=scrMinX; i<scrMaxX; i++ ) {
				for( int j=scrMinY; j<scrMaxY; j++ ) {
					this.DrawPelletIf( i, j );
				}
			}
		}


		////////////////

		private void DrawPelletIf( int tileX, int tileY ) {
			Tile tile = Main.tile[ tileX, tileY ];
			if( tile == null ) { return; }
			if( tile.active() == true ) { return; }

			(bool isPellet, bool isBad) p = SpiritWalkPelletsLogic.IsPelletTile( tileX, tileY );
			
			if( p.isPellet ) {
				if( p.isBad ) {
					Dust.QuickDust( new Point(tileX, tileY), Color.Red );
				} else {
					Dust.QuickDust( new Point(tileX, tileY), Color.Cyan );
				}
			}
		}
	}
}
