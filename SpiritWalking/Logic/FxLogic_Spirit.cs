using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		private static int SpiritFrame = 0;
		private static int SpiritFrameTimer = 0;



		////////////////

		private static DrawData GetSpiritDrawData( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();
			//float angle = MathHelper.ToDegrees( myplayer.FlightDirection.ToRotation() );
			//angle = angle + 90f;
			float rad = myplayer.IntendedFlightVelocity.ToRotation();
			rad = MathHelper.WrapAngle( rad + MathHelper.PiOver2 );

			Texture2D tex = Main.npcTexture[ NPCID.DungeonSpirit ];
			int frameCount = 3;
			int texWidth = tex.Width;
			int texHeight = tex.Height / frameCount;

			/*Main.spriteBatch.Draw(
				texture: tex,
				position: player.Center - Main.screenPosition,	//+ new Vector2( 0, 32 ),
				color: Color.White,
				sourceRectangle: new Rectangle( 0, texHeight * SpiritWalkFxLogic.SpiritFrame, texWidth, texHeight ),
				rotation: rad,	//MathHelper.ToRadians( angle ),  //info.drawPlayer.fullRotation
				origin: new Vector2( texWidth / 2, texHeight / 2 ),
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);*/

			if( SpiritWalkFxLogic.SpiritFrameTimer-- <= 0 ) {
				SpiritWalkFxLogic.SpiritFrameTimer = 4;
				SpiritWalkFxLogic.SpiritFrame += 1;
				SpiritWalkFxLogic.SpiritFrame %= frameCount;
			}

			bool isFlicker = ((myplayer.NoPelletPickupDuration / 10) % 2) != 0;
			Color color = isFlicker
				? Color.Transparent
				: Color.White;
			

			var data = new DrawData(
				texture: tex,
				position: myplayer.FlightProjectile.Center - Main.screenPosition,
				sourceRect: new Rectangle( 0, texHeight * SpiritWalkFxLogic.SpiritFrame, texWidth, texHeight ),
				color: color,
				rotation: rad,
				origin: new Vector2( texWidth / 2, texHeight / 2 ),
				scale: 1f,
				effect: SpriteEffects.None,
				inactiveLayerDepth: 0
			);
			data.ignorePlayerRotation = true;
			return data;
		}
	}
}