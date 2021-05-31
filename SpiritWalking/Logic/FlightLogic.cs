using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using ModLibsCore.Libraries.Debug;
using SpiritWalking.Projectiles;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFlightLogic {
		public static Vector2? CalculateSpiritBallVelocity( SpiritBallProjectile myproj ) {
			float chasePerc = 0.1f;
			Projectile proj = myproj.projectile;
			Player plr = Main.player[proj.owner];
			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			if( !myplayer.IsSpiritWalking ) {
				return null;
			}

			if( myplayer.FinalDashElapsed > 0 ) {
				return Vector2.Lerp( proj.velocity, default, chasePerc );
			}

			Vector2 vel = myplayer.IntendedFlightVelocity * myplayer.CurrentFlightSpeedScale;

			return Vector2.Lerp( proj.velocity, vel, chasePerc );

			/*float currAng = MathHelper.ToDegrees( this.projectile.velocity.ToRotation() );	<- Something fucky is going on with this buy my hair brain can't parse
			float goalAng = MathHelper.ToDegrees( myplayer.FlightDirection.ToRotation() );
			float lerpAng = currAng.AngleLerp( goalAng, 0.1f );
DebugLibraries.Print( "currAng", currAng.ToString() );
DebugLibraries.Print( "goalAng", goalAng.ToString() );
DebugLibraries.Print( "lerpAng", lerpAng.ToString() );

			float currLen = this.projectile.velocity.Length();
			float goalLen = myplayer.FlightDirection.Length();

			this.projectile.velocity = MathHelper.ToRadians(lerpAng).ToRotationVector2();
			this.projectile.velocity *= currLen + ((goalLen - currLen) * accel);*/
//DebugLibraries.Print( "pos", this.projectile.position.ToString() );
//DebugLibraries.Print( "vel", this.projectile.velocity.ToString() );
//DebugLibraries.Print( "dir", myplayer.FlightDirection.ToString() );
		}
	}
}