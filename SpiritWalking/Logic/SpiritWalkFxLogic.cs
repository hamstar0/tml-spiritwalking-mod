using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		public static void ModifyDrawLayers( List<PlayerLayer> layers, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			layers.ForEach( l => l.visible = false );

			layers.Add( new PlayerLayer( SpiritWalkingMod.Instance.Name, "Spirit Walker", SpiritWalkFxLogic.DrawSpirit ) );
		}


		public static void FrameEffects( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			myplayer.player.head = 0;
			myplayer.player.body = 0;
			myplayer.player.legs = 0;

			SpiritWalkFxLogic.EmitParticles(
				position: myplayer.player.MountedCenter,
				direction: default,
				particles: 1
			);
		}


		////////////////

		public static void UpdateBiomeVisuals( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
			bool isMLFilterActive = Filters.Scene["Vortex"].IsActive();

			if( isSpiritWalking ) {
				if( !isMLFilterActive ) {
					Filters.Scene.Activate( "Vortex" );
				}
			} else if( isMLFilterActive ) {
				Filters.Scene["Vortex"].Deactivate( new object[0] );
			}
		}
	}
}