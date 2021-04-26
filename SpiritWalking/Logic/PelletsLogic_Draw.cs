using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static (bool isPellet, bool isBad) DrawPelletIf( int tileX, int tileY ) {
			(bool isPellet, bool isBad) p = SpiritWalkPelletsLogic.IsPelletTile( tileX, tileY );
			
			if( p.isPellet ) {
				SpiritWalkPelletsLogic.DrawPellet( tileX, tileY, p.isBad );
			}

			return p;
		}


		public static void DrawPellet( int tileX, int tileY, bool isBad ) {
			//Dust.QuickDust( new Point(tileX, tileY), Color.Red );
			int dustIdx = Dust.NewDust(
				Position: new Vector2( tileX, tileY ) * 16f,
				Width: 0,
				Height: 0,
				Type: 267,
				SpeedX: 0f,
				SpeedY: 0f,
				Alpha: 0,
				newColor: default( Color ),
				Scale: isBad ? 4f : 2f
			);

			Dust dust = Main.dust[dustIdx];
			dust.velocity = Vector2.Zero;
			dust.fadeIn = 1f;
			//dust.noLight = true;
			dust.noGravity = true;
			dust.color = isBad
				? Color.Red
				: Color.Cyan;
		}
	}
}