using System;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic {
		public static int ReverseInt( int num ) {
			int result = 0;
			while( num > 0 ) {
				result = result * 10 + num % 10;
				num /= 10;
			}
			return result;
		}



		public static (bool isPellet, bool isBad) IsPelletTile( int tileX, int tileY ) {
			ulong seedInt = (ulong)(tileX + (tileY << 16));
			//ulong seed = (ulong)seedInt + ((ulong)SpiritWalkPelletsLogic.ReverseInt(seedInt) << 32);
			ulong seed = seedInt;
			seed += (seed * seedInt) + seedInt;
			seed += (seed * seedInt) + seedInt;
			seed += (seed * seedInt) + seedInt;

			//var rand = new Random( seed );
			//float val = (float)rand.NextDouble();
			float val = Utils.RandomFloat( ref seed );

			var config = SpiritWalkingConfig.Instance;
			float chanceOfPelletPerTile = config.Get<float>( nameof( config.ChanceOfPelletPerTile ) );
			float chanceOfBadPellet = config.Get<float>( nameof( config.ChanceOfBadPelletPerPellet ) );

			float badVal = val / chanceOfPelletPerTile;

			return (
				isPellet: val < chanceOfPelletPerTile,
				isBad: badVal < chanceOfBadPellet
			);
		}
	}
}