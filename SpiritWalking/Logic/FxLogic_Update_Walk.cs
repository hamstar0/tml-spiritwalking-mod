using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void UpdateForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			SpiritWalkFxLogic.EmitSpiritParticles(
				position: myplayer.player.MountedCenter,
				direction: default,
				particles: 1
				//wide: false
				//offsetY: -24
			);

			SpiritWalkFxLogic.EmitSpiritTrailParticles(
				position: myplayer.player.MountedCenter	// + new Vector2( 0f, -20f ),
				//direction: -myplayer.player.velocity
			);
		}


		////////////////

		public static void UpdateDrawLayersForSpiritWalk( SpiritWalkingPlayer myplayer, List<PlayerLayer> layers ) {
			layers.ForEach( l => l.visible = false );

			layers.Add( new PlayerLayer(
				SpiritWalkingMod.Instance.Name,
				"Spirit Walker",
				( info ) => {
					DrawData data = SpiritWalkFxLogic.GetSpiritDrawData( myplayer.player );
					Main.playerDrawData.Add( data );
				}
			) );
		}


		public static void UpdateFrameEffectsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			myplayer.player.head = 0;
			myplayer.player.body = 0;
			myplayer.player.legs = 0;
		}


		////////////////

		public static void UpdateBiomeVisualsForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			if( !Filters.Scene["Vortex"].IsActive() ) {
				Filters.Scene.Activate( "Vortex" );
			}
		}
	}
}