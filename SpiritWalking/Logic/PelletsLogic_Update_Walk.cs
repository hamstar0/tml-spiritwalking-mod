using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static void UpdateWalk( SpiritWalkingPlayer myplayer ) {
			if( myplayer.NoPelletPickupDuration > 0 ) {
				myplayer.NoPelletPickupDuration--;
			}
		}
	}
}