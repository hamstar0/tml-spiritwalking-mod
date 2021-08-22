using Terraria;
using Terraria.ID;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void DeactivateIf( Player player, bool syncIfClient ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();
			if( !myplayer.IsSpiritWalking ) {
				return;
			}

			//

			SpiritWalkLogic.DeactivatePlayerForm( player );

			SpiritWalkFlightLogic.DeactivateFlightBehavior( player );

			SpiritWalkFxLogic.DeactivationFx( player );

			//

			SpiritWalkPelletsLogic.FlushCache();

			//

			myplayer.IsSpiritWalking = false;

			SpiritWalkingAPI.RunSpiritWalkActivationHooks( player, false );

			//

			if( syncIfClient ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					SpiritWalkStateProtocol.Broadcast( myplayer );
				}
			}
		}


		////////////////

		private static void DeactivatePlayerForm( Player player ) {
			player.height = SpiritWalkLogic.PreWalkPlayerHeight;
			player.width = SpiritWalkLogic.PreWalkPlayerWidth;
		}
	}
}
