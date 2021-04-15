using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Mounts;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void Update( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			var config = SpiritWalkingConfig.Instance;
			float nrgAmtDraw = config.PerTickSpiritWalkEnergyCost;

			bool isStillSW = SpiritWalkLogic.HasEnergy( myplayer.player, nrgAmtDraw, out string status );

			if( isStillSW ) {
				SpiritWalkLogic.UpdateSpiritWalk( myplayer );
			} else {
				SpiritWalkLogic.DeactivateIf( myplayer.player, true );
			}

			SpiritWalkLogic.UpdateMountState( myplayer.player, isStillSW );
		}


		////

		public static void UpdateRunSpeeds( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateRunSpeedsForSpiritWalk( myplayer );
			}
		}

		public static void UpdateBuffs( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateFlagsForSpiritWalk( myplayer );
			}
		}

		public static void UpdateMountState( Player player, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				//player.AddBuff( ModContent.BuffType<SpiritModeMountBuff>(), 3 );
				SpiritWalkLogic.UpdateMountStateForSpiritWalk( player );
			} else {
				if( player.mount.Active && player.mount.Type == ModContent.MountType<SpiritModeMount>() ) {
					player.mount.Dismount( player );
				}
			}
		}

		public static void UpdateItemHoldStyle( SpiritWalkingPlayer myplayer, Item item, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateItemHoldStyleForSpiritWalk( myplayer, item );
			}
		}

		public static void UpdateTriggers( SpiritWalkingPlayer myplayer, TriggersSet triggersSet, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateTriggersForSpiritWalk( myplayer, triggersSet );
			}
		}
	}
}