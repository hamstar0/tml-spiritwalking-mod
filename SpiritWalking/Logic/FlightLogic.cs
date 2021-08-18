using Microsoft.Xna.Framework;
using Terraria;
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

			Vector2 intendedVel = myplayer.IntendedFlightVelocity;
			float currSpeedScale = myplayer.CurrentFlightSpeedScale;

			if( !SpiritWalkingAPI.RunSpiritBallVelCalcHooks(proj, chasePerc, ref intendedVel, ref currSpeedScale) ) {
				return null;
			}

			return SpiritWalkingAPI.PredictSpiritBallPosition( proj.velocity, intendedVel, currSpeedScale, chasePerc );
		}
	}
}