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

		public static bool HasMana( Player player, int manaCost, out string status ) {
			if( player.statMana < manaCost ) {
				status = "Not enough mana.";
				return false;
			}

			status = "Success.";
			return true;
		}

		////

		public static void ApplyManaDraw( Player player, int manaCost ) {
			player.statMana -= (int)manaCost;

			if( manaCost > 1 ) {
				Main.LocalPlayer.ManaEffect( -manaCost );
			}
		}


		////////////////

		public static bool HasAnima( Player player, float animaPercentCost, out string status ) {
			float energyAsPercent = animaPercentCost / 100f;

			if( Necrotis.NecrotisAPI.GetAnimaPercentOfPlayer(player) < energyAsPercent ) {
				status = "Not enough anima.";
				return false;
			}

			status = "Success.";
			return true;
		}

		////

		public static void ApplyAnimaDraw( Player player, float energyDraw ) {
			float energyAsPercent = energyDraw / 100f;

			Necrotis.NecrotisAPI.SubtractAnimaPercentFromPlayer( player, energyAsPercent, false );
		}
	}
}