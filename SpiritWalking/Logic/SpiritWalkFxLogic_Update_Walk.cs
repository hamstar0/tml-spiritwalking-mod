using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void UpdateForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			SpiritWalkFxLogic.EmitParticles( myplayer.player.MountedCenter, default, 1, -24 );

			var offsetPos = myplayer.player.MountedCenter + new Vector2( 0f, -20f );

			Dust dust = Dust.NewDustPerfect(
				Position: offsetPos,// - new Vector2(8),
				Type: 6,
				Velocity: -myplayer.player.velocity,
				Alpha: 100,
				newColor: Color.Gold,
				Scale: 2f
			);
			//dust.velocity *= 3f;
			dust.noGravity = true;
			//Dust.QuickDust( offsetPos, Color.Cyan );
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