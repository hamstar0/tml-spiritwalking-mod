using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static bool ApplyActivateCostIf( Player player ) {
			var config = SpiritWalkingConfig.Instance;

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				SpiritWalkLogic.ApplyActivateAnimaCostIf( player );
			} else {
				SpiritWalkLogic.ApplyActivateManaCostIf( player );
			}

			return true;
		}

		////

		private static bool ApplyActivateAnimaCostIf( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float animaPercCost = config.Get<float>( nameof(config.InitialSpiritWalkAnimaPercentCost) );

			SpiritWalkLogic.ApplyAnimaDraw( player, animaPercCost );

			return true;
		}

		private static bool ApplyActivateManaCostIf( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			int manaCost = config.Get<int>( nameof(config.InitialSpiritWalkManaCost) );

			if( !SpiritWalkLogic.HasMana( player, manaCost, out string status ) ) {
				Main.NewText( status, Color.Yellow );

				return false;
			}

			SpiritWalkLogic.EmulatedMana = player.statMana;

			SpiritWalkLogic.ApplyManaDraw( player, manaCost );

			return true;
		}


		////////////////

		public static void ActivateIf( Player player, bool syncIfClient ) {
			var config = SpiritWalkingConfig.Instance;
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.IsSpiritWalking ) {
				return;
			}

			if( SpiritWalkLogic.IsUponOpenAir( player ) ) {
				if( !config.Get<bool>( nameof( config.OpenAirAllowsEngagingSpiritWalk ) ) ) {
					Main.NewText( "Cannot enage spirit walking upon open air.", Color.Yellow );

					return;
				}
			}

			if( !SpiritWalkLogic.ApplyActivateCostIf(player) ) {
				return;
			}

			//

			SpiritWalkFlightLogic.ActivateFlightBehavior( myplayer );

			SpiritWalkLogic.ActivatePlayerForm( player );

			SpiritWalkFxLogic.ActivationFx( myplayer );

			SpiritWalkPelletsLogic.ActivationPreparations( myplayer );

			//

			myplayer.IsSpiritWalking = true;

			SpiritWalkingAPI.RunSpiritWalkActivationHooks( player, true );

			//

			if( syncIfClient ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					SpiritWalkStateProtocol.Broadcast( myplayer );
				}
			}
		}


		////////////////

		private static int PreWalkPlayerWidth = 0;
		private static int PreWalkPlayerHeight = 0;


		//

		private static void ActivatePlayerForm( Player player ) {
			if( player.mount.Active ) {
				player.ClearBuff( player.mount.BuffType );
				player.mount.Dismount( player );
			}

			SpiritWalkLogic.PreWalkPlayerWidth = player.width;
			SpiritWalkLogic.PreWalkPlayerHeight = player.height;

			player.height = 0;
			player.width = 0;
		}
	}
}
