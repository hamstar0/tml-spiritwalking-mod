using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Mounts {
	public class SpiritModeMount : ModMountData {
		public override void SetDefaults() {
			Main.instance.LoadNPC( NPCID.DungeonSpirit );

			this.mountData.spawnDust = 180;

			this.mountData.fallDamage = 0f;

			this.mountData.runSpeed = 0f;
			this.mountData.dashSpeed = 0f;
			this.mountData.flightTimeMax = 0;
			this.mountData.fatigueMax = 0;
			this.mountData.acceleration = 0f;

			this.mountData.jumpSpeed = 0f;
			this.mountData.jumpHeight = 0;
			this.mountData.blockExtraJumps = true;
			this.mountData.constantJump = false;

			this.mountData.totalFrames = 3;
			this.mountData.bodyFrame = 3;

			this.mountData.xOffset = 0;
			this.mountData.yOffset = 0;
			this.mountData.heightBoost = -32;
			//this.mountData.playerHeadOffset = -30;

			int[] array = new int[ this.mountData.totalFrames ];
			for( int l = 0; l < array.Length; l++ ) {
				array[l] = -32;
			}
			this.mountData.playerYOffsets = array;
		}


		public override void SetMount( Player player, ref bool skipDust ) {
			skipDust = false;
		}


		public override bool Draw(
					List<DrawData> playerDrawData,
					int drawType,
					Player drawPlayer,
					ref Texture2D texture,
					ref Texture2D glowTexture,
					ref Vector2 drawPosition,
					ref Rectangle frame,
					ref Color drawColor,
					ref Color glowColor,
					ref float rotation,
					ref SpriteEffects spriteEffects,
					ref Vector2 drawOrigin,
					ref float drawScale,
					float shadow ) {
			return false;
		}
	}
}
