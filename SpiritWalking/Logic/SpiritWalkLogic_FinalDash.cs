using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkLogic {
		public static void BeginFinalDash( SpiritWalkingPlayer myplayer ) {
			myplayer.FinalDashElapsed = 1;
		}


		////////////////

		private static void RunFinalDashIf( SpiritWalkingPlayer myplayer ) {
			if( myplayer.FinalDashElapsed <= 0 ) {
				return;
			}

			int chargeTime = 90;

			SpiritWalkFxLogic.EmitSpiritTrailParticles(
				position: myplayer.player.MountedCenter,
				direction: Vector2.One.RotatedByRandom(Math.PI) * 5f
				//particles: 1
				//wide: true
				//offsetY: 0
			);

			if( myplayer.FinalDashElapsed >= chargeTime ) {
				myplayer.FinalDashElapsed = 0;

				SpiritWalkLogic.AttemptFinalDash( myplayer, true );

				SpiritWalkLogic.DeactivateIf( myplayer.player, true );
			} else {
				myplayer.FinalDashElapsed++;
			}
		}


		////////////////

		public static bool CanFinalDashTo( Vector2 dest ) {
			int tileX = (int)dest.X / 16;
			int tileY = (int)dest.Y / 16;

			for( int i=tileX; i<tileX+2; i++ ) {
				for( int j=tileY; j<tileY+3; j++ ) {
					if( !WorldGen.InWorld(i, j) ) {
						return false;
					}

					Tile tile = Main.tile[i, j];
					if( tile == null ) {
						return false;
					}
					if( tile?.active() == true && Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type] ) {
						return false;
					}
				}
			}
			return true;
		}

		////

		public static void AttemptFinalDash( SpiritWalkingPlayer myplayer, bool sync ) {
			var config = SpiritWalkingConfig.Instance;
			int finalDashTileDist = config.Get<int>( nameof(config.FinalFishTileDistance) );

			Vector2 aim = Vector2.Normalize( myplayer.IntendedFlightVelocity );
			aim *= (float)finalDashTileDist * 16f;

			Vector2 dest = myplayer.FlightProjectile.Center + aim;

			if( SpiritWalkLogic.CanFinalDashTo(dest) ) {
				myplayer.player.Teleport( dest, 1 );

				if( sync ) {
					NetMessage.SendData( MessageID.Teleport, -1, -1, null, 0, (float)myplayer.player.whoAmI, dest.X, dest.Y, 1, 0, 0 );
				}
			}
		}
	}
}