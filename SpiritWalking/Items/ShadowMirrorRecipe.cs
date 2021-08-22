using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SpiritWalking.Items {
	class ShadowMirrorRecipe : ModRecipe {
		public ShadowMirrorRecipe( int mirror1ItemType, int mirror2ItemType, bool isShadowMirror )
					: base( SpiritWalkingMod.Instance ) {
			this.AddIngredient( mirror1ItemType, 1 );
			if( isShadowMirror ) {
				this.AddIngredient( ItemID.SoulofNight, 1 );
			} else {
				this.AddIngredient( ItemID.SoulofLight, 1 );
			}

			this.AddTile( TileID.WorkBenches );

			if( isShadowMirror ) {
				this.SetResult( mirror2ItemType );
			} else {
				this.SetResult( ItemID.MagicMirror );
			}
		}


		public override bool RecipeAvailable() {
			return SpiritWalkingConfig.Instance.ShadowMirrorRecipeEnabled;
		}
	}
}