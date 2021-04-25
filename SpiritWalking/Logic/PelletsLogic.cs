using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		private static IDictionary<ulong, bool?> CachedPellets = new Dictionary<ulong, bool?>();



		////////////////

		public static (bool isPellet, bool isBad) IsPelletTile( int tileX, int tileY ) {
			ulong coord = (ulong)(tileX + (tileY << 16));

			if( SpiritWalkPelletsLogic.CachedPellets.ContainsKey(coord) ) {
				bool? cachedResult = SpiritWalkPelletsLogic.CachedPellets[ coord ];

				if( cachedResult.HasValue ) {
					return (true, cachedResult.Value);
				} else {
					return (false, false);
				}
			}

			(bool isPellet, bool isBad) result = SpiritWalkPelletsLogic.IsPelletCoordUncached( coord );

			SpiritWalkPelletsLogic.CachedPellets[coord] = result.isPellet
				? result.isBad
				: (bool?)null;

			return result;
		}

		public static void FlushCache() {
			SpiritWalkPelletsLogic.CachedPellets.Clear();
		}

		////

		public static (bool isPellet, bool isBad) IsPelletCoordUncached( ulong coord ) {
			ulong seed = coord;
			seed += (seed * coord) + coord;
			seed = seed >> 1;
			seed += (seed * coord) + coord;
			seed = seed >> 1;
			seed += (seed * coord) + coord;
			seed = seed >> 1;
			seed += (seed * coord) + coord;
			seed = Utils.RandomNextSeed( seed );

			//var rand = new Random( seed );
			//float val = (float)rand.NextDouble();
			float val = Utils.RandomFloat( ref seed );

			var config = SpiritWalkingConfig.Instance;
			float chanceOfPelletPerTile = config.Get<float>( nameof( config.ChanceOfPelletPerTile ) );
			float chanceOfBadPellet = config.Get<float>( nameof( config.ChanceOfBadPelletPerPellet ) );

			float badVal = val / chanceOfPelletPerTile;

			var result = (
				isPellet: val < chanceOfPelletPerTile,
				isBad: badVal < chanceOfBadPellet
			);

			return result;
		}


		////////////////
		
		public static bool IsPelletNearPlayer( int tileX, int tileY, bool isBad ) {
			int plrTileX = (int)Main.LocalPlayer.Center.X / 16;
			int plrTileY = (int)Main.LocalPlayer.Center.Y / 16;

			int diffX = plrTileX - tileX;
			int diffY = plrTileY - tileY;
			int distSqr = (diffX * diffX) + (diffY * diffY);

			if( isBad ) {
				return distSqr < 1600;	//40
			} else {
				return distSqr < 144;	//12
			}
		}
	}
}