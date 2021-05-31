using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		private static bool CanSpiritWalk( Player player ) {
			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				return SpiritWalkLogic.CanSpiritWalkWithAnima( player );
			} else {
				return SpiritWalkLogic.CanSpiritWalkWithMana( player );
			}
		}

		////

		private static bool CanSpiritWalkWithMana( Player player ) {
			if( SpiritWalkLogic.ManaCostDuration > 0 ) {
				return true;
			}

			var config = SpiritWalkingConfig.Instance;
			int manaCostRate = config.Get<int>( nameof(config.PerRateSpiritWalkManaCost) );

			return SpiritWalkLogic.HasMana( player, manaCostRate, out string _ );
		}

		private static bool CanSpiritWalkWithAnima( Player player ) {
			var config = SpiritWalkingConfig.Instance;
			float animaCostRate = config.Get<float>( nameof(config.PerTickSpiritWalkAnimaPercentCost) );

			return SpiritWalkLogic.HasAnima( player, animaCostRate, out string _ );
		}


		////////////////

		public static void Update( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
//DebugLibraries.Print( "walk?", ""+isSpiritWalking+", "+myplayer.player.width+", "+myplayer.player.height );
			if( !isSpiritWalking ) {
				return;
			}

			bool canSW = SpiritWalkLogic.CanSpiritWalk( myplayer.player );

			if( canSW ) {
				SpiritWalkLogic.UpdateForSpiritWalk( myplayer );
			} else {
				SpiritWalkLogic.DeactivateIf( myplayer.player, true );
			}

			SpiritWalkFxLogic.Update( myplayer, canSW );
		}


		////

		public static void UpdateRunSpeeds( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdateRunSpeedsForSpiritWalk( myplayer );
			}
		}

		////

		public static void UpdatePlayerFlagsPostBuffs( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdatePlayerFlagsPostBuffsForSpiritWalk( myplayer );
			}
		}

		public static void UpdatePlayerFlagsPostMisc( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkLogic.UpdatePlayerFlagsPostMiscForSpiritWalk( myplayer );
			}
		}

		////

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