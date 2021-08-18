using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Classes.Loadable;


namespace SpiritWalking {
	public delegate bool SpiritBallVelocityCalculationHook(
			Projectile projectile,
			float targetLerpToPercent,
			ref Vector2 intendedFlightVelocity,
			ref float currentFlightSpeedScale );




	public class SpiritWalkingAPI : ILoadable {
		public static void AddSpiritBallVelocityCalculationHook( SpiritBallVelocityCalculationHook hook ) {
			var api = ModContent.GetInstance<SpiritWalkingAPI>();
			api.BallVelCalcHooks.Add( hook );
		}


		////////////////

		public static Vector2 PredictSpiritBallPosition(
					Vector2 currentVelocity,
					Vector2 intendedVelocity,
					float currentFlightSpeedScale,
					float lerpPercent ) {
			Vector2 vel = intendedVelocity * currentFlightSpeedScale;

			return Vector2.Lerp( currentVelocity, vel, lerpPercent );
		}


		////////////////

		internal static bool RunSpiritBallVelCalcHooks(
					Projectile projectile,
					float chasePerc,
					ref Vector2 intendedVel,
					ref float currSpeedScale ) {
			var api = ModContent.GetInstance<SpiritWalkingAPI>();

			foreach( SpiritBallVelocityCalculationHook hook in api.BallVelCalcHooks ) {
				if( !hook.Invoke(projectile, chasePerc, ref intendedVel, ref currSpeedScale) ) {
					return false;
				}
			}
			return true;
		}



		////////////////

		private ISet<SpiritBallVelocityCalculationHook> BallVelCalcHooks = new HashSet<SpiritBallVelocityCalculationHook>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }
	}
}