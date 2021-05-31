using Terraria;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void HandleWalkControls(
					SpiritWalkingPlayer myplayer,
					bool jump,
					bool down,
					bool up,
					bool left,
					bool right ) {
			if( jump ) {
				bool isFinalDash = myplayer.FinalDashElapsed > 0;

				if( !isFinalDash ) {
					SpiritWalkLogic.BeginFinalDash( myplayer );
				}
			}

			if( down || up || left || right ) {
				SpiritWalkFlightLogic.SteerFlight( myplayer, down, up, left, right );
			}
		}
	}
}