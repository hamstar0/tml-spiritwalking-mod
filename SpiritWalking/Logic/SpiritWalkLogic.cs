using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void ApplyEnergyDraw( Player player, float energyDraw ) {
			var config = SpiritWalkingConfig.Instance;
			bool swUsesAnima = config.SpiritWalkUsesAnimaIfNecrotisAvailable
				&& SpiritWalkingMod.Instance.NecrotisMod != null;

			if( swUsesAnima ) {
				SpiritWalkLogic.ApplyEnergyDraw_Necrotis( player, energyDraw );
			} else {
				player.statMana -= (int)energyDraw;
			}
		}
		
		private static void ApplyEnergyDraw_Necrotis( Player player, float energyDraw ) {
			float energyAsPercent = energyDraw / 100f;

			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer( player, energyAsPercent, false );
		}


		////////////////

		public static void Update( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;

			bool isStillSW = SpiritWalkLogic.HasEnergy( myplayer.player, nrgAmtDraw, out string status );

			if( isStillSW ) {
				SpiritWalkLogic.UpdateSpiritWalk( myplayer.player );
			} else {
				SpiritWalkLogic.DeactivateIf( myplayer.player );
			}
		}

		public static void UpdateBuffs( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateBuffsForSpiritWalk( myplayer.player );
			}
		}

		public static void UpdateItemHoldStyle( SpiritWalkingPlayer myplayer, Item item, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateItemHoldStyleForSpiritWalk( myplayer.player, item );
			}
		}


		////

		private static void UpdateSpiritWalk( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;

			SpiritWalkLogic.ApplyEnergyDraw( player, nrgAmtDraw );

			//player.stoned = true;
			player.gravity = 0f;
		}

		private static void UpdateBuffsForSpiritWalk( Player player ) {
			player.noItems = true;
			player.immune = true;
		}

		private static void UpdateItemHoldStyleForSpiritWalk( Player player, Item item ) {
			int oldHoldStyle = item.holdStyle;
			item.holdStyle = 0;

			Timers.RunNow( () => item.holdStyle = oldHoldStyle );
		}
	}
}