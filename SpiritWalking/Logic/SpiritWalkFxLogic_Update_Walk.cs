using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void UpdateForSpiritWalk( SpiritWalkingPlayer myplayer ) {
			SpiritWalkFxLogic.EmitParticles(
				position: myplayer.player.MountedCenter,
				direction: default,
				particles: 1
			);

			//Vector2 offsetPos = myplayer.player.MountedCenter + new Vector2( 0f, -20f );

			/*int num75 = Dust.NewDust( offsetPos - new Vector2(8), 16, 16, 6, 0f, 0f, 100, default, 2.5f );
			Main.dust[num75].velocity *= 3f;
			Main.dust[num75].noGravity = true;*/
			//Dust.QuickDust( offsetPos, Color.Cyan );
		}


		////////////////

		public static void UpdateDrawLayersForSpiritWalk( List<PlayerLayer> layers ) {
			layers.ForEach( l => l.visible = false );

			layers.Add( new PlayerLayer(SpiritWalkingMod.Instance.Name, "Spirit Walker", SpiritWalkFxLogic.DrawSpirit) );
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