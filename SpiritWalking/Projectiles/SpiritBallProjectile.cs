using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Projectiles {
	partial class SpiritBallProjectile : ModProjectile {
		public override string Texture => "Terraria/NPC_"+NPCID.DungeonSpirit;



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

			this.UpdateCollisionCooldown();

			Player plr = Main.player[ this.projectile.owner ];
			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			float accel = 0.1f;
			Vector2 vel = myplayer.IntendedFlightVelocity * myplayer.CurrentFlightSpeedScale;

			this.projectile.velocity = Vector2.Lerp( this.projectile.velocity, vel, accel );

			/*float currAng = MathHelper.ToDegrees( this.projectile.velocity.ToRotation() );	<- Something fucky is going on with this buy my hair brain can't parse
			float goalAng = MathHelper.ToDegrees( myplayer.FlightDirection.ToRotation() );
			float lerpAng = currAng.AngleLerp( goalAng, 0.1f );
DebugHelpers.Print( "currAng", currAng.ToString() );
DebugHelpers.Print( "goalAng", goalAng.ToString() );
DebugHelpers.Print( "lerpAng", lerpAng.ToString() );

			float currLen = this.projectile.velocity.Length();
			float goalLen = myplayer.FlightDirection.Length();

			this.projectile.velocity = MathHelper.ToRadians(lerpAng).ToRotationVector2();
			this.projectile.velocity *= currLen + ((goalLen - currLen) * accel);*/

			this.projectile.timeLeft = 10;
//DebugHelpers.Print( "pos", this.projectile.position.ToString() );
//DebugHelpers.Print( "vel", this.projectile.velocity.ToString() );
//DebugHelpers.Print( "dir", myplayer.FlightDirection.ToString() );
		}


		////////////////

		public override bool PreDraw( SpriteBatch spriteBatch, Color lightColor ) {
			return false;
		}
	}
}
