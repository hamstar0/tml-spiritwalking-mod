using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.OverlaySounds;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkFxLogic {
		private static OverlaySound FlightSoundLoop = null;

		private static bool CollisionDetected = false;
		


		////////////////
		
		private static (float VolumeOverride, float PanOverride, float PitchOverride, bool IsEnded) FlightSoundLoopCondition() {
			float volume = 0.1f;

			if( SpiritWalkFxLogic.CollisionDetected ) {
				SpiritWalkFxLogic.CollisionDetected = false;

				volume = 0.3f;
			}

			return (volume, 0f, 0f, false);
		}


		////////////////
		
		public static void ApplySpiritWalkCollisionFriction( Player player ) {
			SpiritWalkFxLogic.CollisionDetected = true;

			SpiritWalkFxLogic.EmitParticles( player.MountedCenter, default, 2 );
		}


		////////////////

		public static void UpdateDrawLayers( List<PlayerLayer> layers, bool isSpiritWalking ) {
			if( !isSpiritWalking ) {
				return;
			}

			layers.ForEach( l => l.visible = false );

			layers.Add( new PlayerLayer(SpiritWalkingMod.Instance.Name, "Spirit Walker", SpiritWalkFxLogic.DrawSpirit) );
		}


		public static void UpdateFrameEffects( SpiritWalkingPlayer myplayer, bool isSpiritWalking ) {
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