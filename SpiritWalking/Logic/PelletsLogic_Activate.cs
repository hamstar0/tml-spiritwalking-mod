using System;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static void ActivationPreparations( SpiritWalkingPlayer myplayer ) {
			myplayer.NoPelletPickupDuration = 60 * 3;
		}
	}
}