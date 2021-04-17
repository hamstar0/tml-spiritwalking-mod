using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;


namespace SpiritWalking.Projectiles {
	partial class SpiritBallProjectile : ModProjectile {
		//private int CollisionCooldown = 0;



		////////////////

		private void UpdateCollisionCooldown() {
			//if( this.CollisionCooldown > 0 ) {
			//	this.CollisionCooldown--;
			//}
		}


		public override bool OnTileCollide( Vector2 oldVelocity ) {
			if( this.projectile.npcProj ) {
				return true;
			}

			//if( this.CollisionCooldown > 0 ) {
			//	return false;
			//}
			//
			//this.CollisionCooldown = 4;

			Player plr = Main.player[ this.projectile.owner ];
			SpiritWalkFlightLogic.ApplySpiritWalkCollisionFriction( plr );
			SpiritWalkFxLogic.ApplySpiritWalkCollisionFriction( plr );

			//

			/*Vector2 newDir = this.projectile.velocity;
			bool velocityChanged = false;

			if( this.projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f ) {
				newDir.X = oldVelocity.X * -1f;
				velocityChanged = true;
			}
			if( this.projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f ) {
				newDir.Y = oldVelocity.Y * -1f;
				velocityChanged = true;
			}

			if( velocityChanged ) {
				var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

				float length = myplayer.FlightDirection.Length();
				float newAngle = newDir.ToRotation();
				float dirAngle = myplayer.FlightDirection.ToRotation();
				float newDirAngle = Utils.AngleLerp( dirAngle, newAngle, 0.1f );

				myplayer.FlightDirection = newDirAngle.ToRotationVector2();
				myplayer.FlightDirection *= length;
			}*/

			//

			return false;
		}
	}
}
