using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.Debug;


namespace SpiritWalking.Logic {
	internal partial class SpiritWalkPelletsLogic : ILoadable {
		private static int BaseSeed;



		////////////////

		public static (bool isPellet, bool isBad) IsPelletTile( Player player, int tileX, int tileY ) {
			int seed = SpiritWalkPelletsLogic.BaseSeed + tileX + (tileY << 16);
			uint seedHash = (uint)seed.GetHashCode();
			double valDbl = (double)seedHash / (double)uint.MaxValue;
			float val = (float)valDbl;

			var config = SpiritWalkingConfig.Instance;
			float chanceOfPelletPerTile = config.Get<float>( nameof( config.ChanceOfPelletPerTile ) );
			float chanceOfBadPellet = config.Get<float>( nameof( config.ChanceOfBadPelletPerPellet ) );

			float badVal = ( val - chanceOfPelletPerTile ) / ( 1f - chanceOfPelletPerTile );

			return (
				isPellet: val < chanceOfPelletPerTile,
				isBad: badVal < chanceOfBadPellet
			);
		}


		////////////////

		public void OnModsLoad() {
			SpiritWalkPelletsLogic.BaseSeed = Main.clientUUID.GetHashCode();
		}

		public void OnModsUnload() { }

		public void OnPostModsLoad() { }
	}
}