using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void Update( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkFxLogic.UpdateForSpiritWalk( myplayer );
			}
		}


		////////////////

		public static void UpdateDrawLayers( SpiritWalkingPlayer myplayer, List<PlayerLayer> layers, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkFxLogic.UpdateDrawLayersForSpiritWalk( myplayer, layers );
			}
		}


		public static void UpdateFrameEffects( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkFxLogic.UpdateFrameEffectsForSpiritWalk( myplayer );
			}
		}


		////////////////

		public static void UpdateBiomeVisuals( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( isSpiritWalking ) {
				SpiritWalkFxLogic.UpdateBiomeVisualsForSpiritWalk( myplayer );
			} else {
				if( Filters.Scene["Vortex"].IsActive() ) {
					Filters.Scene["Vortex"].Deactivate( new object[0] );
				}
			}
		}
	}
}