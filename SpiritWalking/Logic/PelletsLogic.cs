using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		private static IDictionary<ulong, bool?> CachedRevealedPellets = new Dictionary<ulong, bool?>();



		////////////////

		public static (bool isPellet, bool isBad) IsPelletTile( int tileX, int tileY ) {
			int coordInt = tileX + (tileY << 16);
			ulong coord = (ulong)coordInt;

			if( SpiritWalkPelletsLogic.CachedRevealedPellets.ContainsKey(coord) ) {
				bool? cachedResult = SpiritWalkPelletsLogic.CachedRevealedPellets[ coord ];

				if( cachedResult.HasValue ) {
					return (true, cachedResult.Value);
				} else {
					return (false, false);
				}
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.EatenPelletCoords.Contains(coordInt) ) {
				return (false, false);
			}

			//

			(bool isPellet, bool isBad) result = SpiritWalkPelletsLogic.IsPelletCoordUncached( coord );

			SpiritWalkPelletsLogic.CachedRevealedPellets[coord] = result.isPellet
				? result.isBad
				: (bool?)null;

			return result;
		}

		public static void FlushCache() {
			SpiritWalkPelletsLogic.CachedRevealedPellets.Clear();
		}


		////

		private static (bool isPellet, bool isBad) IsPelletCoordUncached( ulong coord ) {
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
	}
}