using Microsoft.Xna.Framework;
using Terraria;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static bool HasEnergy( Player player, float energyCost, out string status ) {
			var config = SpiritWalkingConfig.Instance;
			bool swUsesAnima = config.SpiritWalkUsesAnimaIfNecrotisAvailable
				&& SpiritWalkingMod.Instance.NecrotisMod != null;

			if( swUsesAnima ) {
				return HasEnergy_Necrotis( player, energyCost, out status );
			} else {
				if( player.statMana < energyCost ) {
					status = "Not enough mana.";
					return false;
				}
			}

			status = "Success.";
			return true;
		}

		private static bool HasEnergy_Necrotis( Player player, float energyCost, out string status ) {
			float energyAsPercent = energyCost / 100f;

			if( Necrotis.NecrotisAPI.GetAnimaPercentOfPlayer(player) < energyAsPercent ) {
				status = "Not enough anima.";
				return false;
			}
			status = "Success.";
			return true;
		}


		////////////////

		public static void ActivateIf( Player player ) {
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

			SpiritWalkLogic.ApplyEnergyDraw( player, nrgAmtDraw );

			myplayer.IsSpiritWalking = true;
		}


		public static void DeactivateIf( Player player ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			myplayer.IsSpiritWalking = false;
		}
	}
}