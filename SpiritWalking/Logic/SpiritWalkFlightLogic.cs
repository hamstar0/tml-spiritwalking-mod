using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;
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
DebugHelpers.Print( "currAng", currAng.ToString() );
DebugHelpers.Print( "goalAng", goalAng.ToString() );
DebugHelpers.Print( "lerpAng", lerpAng.ToString() );

			float currLen = this.projectile.velocity.Length();
			float goalLen = myplayer.FlightDirection.Length();

			this.projectile.velocity = MathHelper.ToRadians(lerpAng).ToRotationVector2();
			this.projectile.velocity *= currLen + ((goalLen - currLen) * accel);*/
//DebugHelpers.Print( "pos", this.projectile.position.ToString() );
//DebugHelpers.Print( "vel", this.projectile.velocity.ToString() );
//DebugHelpers.Print( "dir", myplayer.FlightDirection.ToString() );
		}


		////////////////

		public static void ApplySpiritWalkOpenAirFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaPercDraw = config.Get<float>( nameof(config.PerTickSpiritWalkAnimaPercentCostInOpenAir) );

				SpiritWalkLogic.ApplyAnimaDraw( player, animaPercDraw );
			} else {
				if( SpiritWalkLogic.ManaCostDuration == 0 ) {
					int manaDraw = config.Get<int>( nameof(config.PerRateSpiritWalkManaCostInOpenAir) );

					SpiritWalkLogic.ApplyManaDraw( player, manaDraw );
				}
			}
		}
		
		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float animaPercDraw = config.Get<float>( nameof(config.PerTickSpiritWalkFrictionAddedAnimaPercentCost) );

				SpiritWalkLogic.ApplyAnimaDraw( player, animaPercDraw );
			} else {
				if( SpiritWalkLogic.ManaCostDuration == 0 ) {
					int manaDraw = config.Get<int>( nameof(config.PerRateSpiritWalkFrictionAddedManaCost) );

					SpiritWalkLogic.ApplyManaDraw( player, manaDraw );
				}
			}
		}
	}
}