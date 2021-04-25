using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		private static IDictionary<ulong, bool?> CachedPellets = new Dictionary<ulong, bool?>();



		////////////////

		public static (bool isPellet, bool isBad) IsPelletTile( int tileX, int tileY ) {
			ulong seedInt = (ulong)(tileX + (tileY << 16));

			if( SpiritWalkPelletsLogic.CachedPellets.ContainsKey(seedInt) ) {
				bool? cachedResult = SpiritWalkPelletsLogic.CachedPellets[ seedInt ];

				if( cachedResult.HasValue ) {
					return (true, cachedResult.Value);
				} else {
					return (false, false);
				}
			}

			ulong seed = seedInt;
			seed += (seed * seedInt) + seedInt;
			seed = seed >> 1;
			seed += (seed * seedInt) + seedInt;
			seed = seed >> 1;
			seed += (seed * seedInt) + seedInt;
			seed = seed >> 1;
			seed += (seed * seedInt) + seedInt;
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

			SpiritWalkPelletsLogic.CachedPellets[seedInt] = result.isPellet
				? result.isBad
				: (bool?)null;

			return result;
		}


		public static void FlushCache() {
			SpiritWalkPelletsLogic.CachedPellets.Clear();
		}
	}
}