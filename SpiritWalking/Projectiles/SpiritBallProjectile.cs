using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Logic;


namespace SpiritWalking.Projectiles {
	class SpiritBallProjectile : ModProjectile {
		public override string Texture => "Terraria/NPC_" + NPCID.DungeonSpirit;



		////////////////
		
		public override void SetStaticDefaults() {
			Main.instance.LoadNPC( NPCID.DungeonSpirit );
		}

		public override void SetDefaults() {
			this.projectile.width = 8;
			this.projectile.height = 8;
			this.projectile.penetrate = -1;
			this.projectile.hostile = false;
			this.projectile.tileCollide = true;
			this.projectile.ignoreWater = true;
		}


		////////////////

		public override bool TileCollideStyle( ref int width, ref int height, ref bool fallThrough ) {
			fallThrough = true;
			return true;
		}

		public override bool OnTileCollide( Vector2 oldVelocity ) {
			if( this.projectile.npcProj ) {
				return true;
			}

			/*bool velocityChanged = false;

			if( this.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f ) {
				this.projectile.velocity.X = oldVelocity.X * -0.9f;
				velocityChanged = true;
			}
			if( this.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f ) {
				this.projectile.velocity.Y = oldVelocity.Y * -0.9f;
				velocityChanged = true;
			}

			if( velocityChanged ) {
				Player plr = Main.player[ this.projectile.owner ];
				var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();
				float length = this.projectile.velocity.Length();

				myplayer.FlightDirection = Vector2.Normalize( this.projectile.velocity );
				myplayer.FlightDirection *= Math.Min( length, SpiritWalkFlightLogic.DefaultFlightSpeed );
			}*/

			return false;
		}


		////////////////

		public override void AI() {
			if( this.projectile.npcProj ) {
				return;
			}

			Player plr = Main.player[ this.projectile.owner ];
			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			float accel = 0.1f;

			this.projectile.velocity = Vector2.Lerp( this.projectile.velocity, myplayer.FlightDirection, accel );
		}


		////////////////

		public override bool PreDraw( SpriteBatch spriteBatch, Color lightColor ) {
			return false;
		}
	}
}
