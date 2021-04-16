using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void ActivateIf( Player player, bool sync ) {
			var config = SpiritWalkingConfig.Instance;
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();
			float nrgAmtDraw = config.InitialSpiritWalkEnergyCost;

			if( myplayer.IsSpiritWalking ) {
				return;
			}

			if( !SpiritWalkLogic.HasEnergy(player, nrgAmtDraw, out string status) ) {
				Main.NewText( status, Color.Yellow );
				return;
			}

			//

			SpiritWalkLogic.ApplyEnergyDraw( player, nrgAmtDraw );

			//

			SpiritWalkFlightLogic.Activate( player );
			SpiritWalkFxLogic.Activate( player );

			//

			SpiritWalkLogic.ActivatePlayerForm( player );
			if( player.mount.Active ) {
				player.ClearBuff( player.mount.BuffType );
				player.mount.Dismount( player );
			}

			SpiritWalkLogic.PreWalkPlayerWidth = myplayer.player.width;
			SpiritWalkLogic.PreWalkPlayerHeight = myplayer.player.height;

			myplayer.player.width = 0;
			myplayer.player.height = 0;

			//

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					SpiritWalkStateProtocol.Broadcast( myplayer );
				}
			}

			//

			myplayer.IsSpiritWalking = true;
		}


		public static void DeactivateIf( Player player, bool sync ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			//

			SpiritWalkLogic.DeactivatePlayerForm( player );
			myplayer.player.width = SpiritWalkLogic.PreWalkPlayerWidth;
			myplayer.player.height = SpiritWalkLogic.PreWalkPlayerHeight;

			//

			SpiritWalkFlightLogic.Deactivate( player );
			SpiritWalkFxLogic.Deactivate( player );

			//

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					SpiritWalkStateProtocol.Broadcast( myplayer );
				}
			}

			//

			myplayer.IsSpiritWalking = false;
		}


		////////////////

		private static void ActivatePlayerForm( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			if( player.mount.Active ) {
				player.ClearBuff( player.mount.BuffType );
				player.mount.Dismount( player );
			}

			SpiritWalkLogic.PreWalkPlayerWidth = myplayer.player.width;
			SpiritWalkLogic.PreWalkPlayerHeight = myplayer.player.height;

			myplayer.player.width = 0;
			myplayer.player.height = 0;
		}


		private static void DeactivatePlayerForm( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			myplayer.player.width = SpiritWalkLogic.PreWalkPlayerWidth;
			myplayer.player.height = SpiritWalkLogic.PreWalkPlayerHeight;
		}
	}
}