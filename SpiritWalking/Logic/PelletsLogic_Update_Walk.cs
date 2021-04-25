using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static void UpdateWalk( SpiritWalkingPlayer myplayer ) {
			if( myplayer.NoPelletPickupDuration > 0 ) {
				myplayer.NoPelletPickupDuration--;
			}
		}
	}
}