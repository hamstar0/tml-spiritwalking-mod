using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static bool IsUponOpenAir( Player player ) {
			int tileX = (int)player.Center.X / 16;
			int tileY = (int)player.Center.Y / 16;

			if( !WorldGen.InWorld( tileX, tileY ) ) {
				return false;
			}

			Tile tile = Main.tile[tileX, tileY];
			return tile.wall == 0 && tileY <= WorldHelpers.SurfaceLayerBottomTileY;
		}


		////////////////

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

			if( Necrotis.NecrotisAPI.GetAnimaPercentOfPlayer( player ) < energyAsPercent ) {
				status = "Not enough anima.";
				return false;
			}
			status = "Success.";
			return true;
		}


		////////////////

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
	}
}