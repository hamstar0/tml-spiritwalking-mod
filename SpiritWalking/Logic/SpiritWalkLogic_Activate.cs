using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Services.OverlaySounds;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static OverlaySound RocketLoop = null;



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

			if( Necrotis.NecrotisAPI.GetAnimaPercentOfPlayer(player) < energyAsPercent ) {
				status = "Not enough anima.";
				return false;
			}
			status = "Success.";
			return true;
		}


		////////////////

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

			SpiritWalkLogic.ApplyEnergyDraw( player, nrgAmtDraw );

			SpiritWalkLogic.RocketLoop = OverlaySound.Create(
				sourceMod: SpiritWalkingMod.Instance,
				soundPath: "Sounds/rocket",
				fadeTicks: 0,
				customCondition: () => (0.2f, 0f, 0f, false)
			);
			SpiritWalkLogic.RocketLoop.Play();

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					SpiritWalkStateProtocol.Broadcast( myplayer );
				}
			}

			myplayer.IsSpiritWalking = true;
		}


		public static void DeactivateIf( Player player, bool sync ) {
			var myplayer = player.GetModPlayer<SpiritWalkingPlayer>();

			SpiritWalkLogic.RocketLoop.StopImmediately();

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					SpiritWalkStateProtocol.Broadcast( myplayer );
				}
			}

			myplayer.IsSpiritWalking = false;
		}
	}
}