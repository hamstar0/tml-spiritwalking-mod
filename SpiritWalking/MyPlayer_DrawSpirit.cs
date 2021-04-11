using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SpiritWalking {
	public partial class SpiritWalkingPlayer : ModPlayer {
		private int SpiritFrame = 0;
		private int SpiritFrameTimer = 0;



		////////////////

		private void DrawSpirit( PlayerDrawInfo info ) {
			Texture2D tex = Main.npcTexture[NPCID.DungeonSpirit];
			int texWidth = tex.Width;
			int texHeight = tex.Height / 3;

			Main.spriteBatch.Draw(
				texture: tex,
				position: player.Center - Main.screenPosition,
				color: Color.White,
				sourceRectangle: new Rectangle( 0, texHeight * this.SpiritFrame, texWidth, texHeight ),
				rotation: player.fullRotation,
				origin: new Vector2( tex.Width / 2, tex.Height / 2 ),
				scale: 1f,
				effects: SpriteEffects.None,
				layerDepth: 0f
			);

			if( this.SpiritFrameTimer-- <= 0 ) {
				this.SpiritFrameTimer = 4;
				this.SpiritFrame += 1;
				this.SpiritFrame %= 3;
			}
		}
	}
}