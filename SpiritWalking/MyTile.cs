using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritWalking.Logic;
using Terraria;
using Terraria.ModLoader;


namespace SpiritWalking {
	class SpiritWalkingTile : GlobalTile {
		public override void DrawEffects(
					int i,
					int j,
					int type,
					SpriteBatch spriteBatch,
					ref Color drawColor,
					ref int nextSpecialDrawIndex ) {
			Tile tile = Main.tile[ i, j ];
			if( tile == null ) { return; }
			//if( tile.active() == true ) { return; }

			(bool isPellet, bool isBad) p = SpiritWalkPelletsLogic.IsPelletTile( i, j );

			if( p.isPellet ) {
				if( p.isBad ) {
					Dust.QuickDust( new Point( i, j ), Color.Red );
				} else {
					Dust.QuickDust( new Point( i, j ), Color.Cyan );
				}
			}
		}
	}
}
