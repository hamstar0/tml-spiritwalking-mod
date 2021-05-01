using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;


namespace SpiritWalking.Projectiles {
	partial class SpiritBallProjectile : ModProjectile {
		public override string Texture => "Terraria/NPC_"+NPCID.DungeonSpirit;



		////////////////
		
		public override void SetStaticDefaults() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				Main.instance.LoadNPC( NPCID.DungeonSpirit );
			}
		}

		public override void SetDefaults() {
			this.projectile.width = 12;
			this.projectile.height = 12;
			this.projectile.penetrate = -1;
			this.projectile.hostile = false;
			this.projectile.tileCollide = true;
		}


		////////////////

		public override bool TileCollideStyle( ref int width, ref int height, ref bool fallThrough ) {
			fallThrough = true;
			return true;
		}


		////////////////

		public override void AI() {
			if( this.projectile.npcProj ) {
				return;
			}

			Vector2? vel = SpiritWalkFlightLogic.CalculateSpiritBallVelocity( this );
			if( !vel.HasValue ) {
				this.projectile.Kill();

				return;
			}

			this.projectile.velocity = vel.Value;

			this.projectile.timeLeft = 10;
		}


		////////////////

		public override bool PreDraw( SpriteBatch spriteBatch, Color lightColor ) {
			return false;
		}
	}
}
