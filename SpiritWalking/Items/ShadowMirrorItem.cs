using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Logic;


namespace SpiritWalking.Items {
	public class ShadowMirrorItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Shadow Mirror" );
			this.Tooltip.SetDefault(
				"Gaze into the mirror to go to Other Side"
				+"\nActivates 'spirit walking' mode"
				+"\n\"When you gaze into the abyss...\""
			);
		}

		public override void SetDefaults() {
			this.item.useTurn = true;
			this.item.width = 20;
			this.item.height = 20;
			this.item.useStyle = ItemUseStyleID.HoldingUp;
			this.item.useTime = 120;
			this.item.UseSound = SoundID.Item6;
			this.item.useAnimation = 90;
			this.item.rare = ItemRarityID.Pink;
			this.item.value = Item.buyPrice( 0, 2, 0, 0 );
		}

		////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation == 1 ) {
				SpiritWalkLogic.ActivateIf( player );
			}
			return base.UseItem( player );
		}

		////

		public override void AddRecipes() {
			int shadowMirrorType = ModContent.ItemType<ShadowMirrorItem>();
			var recipe1 = new ShadowMirrorRecipe( ItemID.MagicMirror, shadowMirrorType, true );
			var recipe2 = new ShadowMirrorRecipe( shadowMirrorType, ItemID.MagicMirror, false );
			recipe1.AddRecipe();
			recipe2.AddRecipe();
		}
	}




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