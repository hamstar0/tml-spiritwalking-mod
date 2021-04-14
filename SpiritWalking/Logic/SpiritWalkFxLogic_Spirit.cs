using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		private static int SpiritFrame = 0;
		private static int SpiritFrameTimer = 0;



		////////////////

		private static void DrawSpirit( PlayerDrawInfo info ) {
			Texture2D tex = Main.npcTexture[NPCID.DungeonSpirit];
			int texWidth = tex.Width;
			int texHeight = tex.Height / 3;

			Main.spriteBatch.Draw(
				texture: tex,
				position: info.drawPlayer.Center - Main.screenPosition + new Vector2( 0, 32 ),
				color: Color.White,
				sourceRectangle: new Rectangle( 0, texHeight * SpiritWalkFxLogic.SpiritFrame, texWidth, texHeight ),
				rotation: info.drawPlayer.fullRotation,
				origin: new Vector2( tex.Width / 2, tex.Height / 2 ),
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			if( SpiritWalkFxLogic.SpiritFrameTimer-- <= 0 ) {
				SpiritWalkFxLogic.SpiritFrameTimer = 4;
				SpiritWalkFxLogic.SpiritFrame += 1;
				SpiritWalkFxLogic.SpiritFrame %= 3;
			}
		}
	}
}