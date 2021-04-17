using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Logic;


namespace SpiritWalking.Items {
	public class ShadowMirrorItem : ModItem {
		public static readonly IList<string> TooltipLines = new List<string> {
			"Gaze into the mirror to go to Other Side",
			"Activates 'spirit walking' on use",
			"Press 'jump' while spirit walking to return (also causes a short dash)",
			"\"I'm as free as a bird now!\""
		};
		


		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Shadow Mirror" );
			this.Tooltip.SetDefault( string.Join("\n", ShadowMirrorItem.TooltipLines) );
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

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			var config = SpiritWalkingConfig.Instance;
			float initNrgCost = config.Get<float>( nameof(config.InitialSpiritWalkEnergyCost) );
			bool isAnima = config.Get<bool>( nameof(config.SpiritWalkUsesAnimaIfNecrotisAvailable) )
				&& ModLoader.GetMod( "Necrotis" ) != null;

			var lines = ShadowMirrorItem.TooltipLines.ToList();

			if( isAnima ) {
				string initNrgCostPerc = ((int)(initNrgCost * 100f)).ToString();

				lines.Insert( 3, "Requires "+initNrgCostPerc+"% anima to activate (by default)" );
				lines.Insert( 4, "Spirit walking drains anima while active" );
			} else {
				lines.Insert( 3, "Requires "+(int)initNrgCost+" mana to activate (by default)" );
				lines.Insert( 4, "Spirit walking drains mana while active" );
			}

			lines.Insert( 5, "Collisions or contact with open air cause faster drain" );
		}


		////////////////

		public override bool UseItem( Player player ) {
			if( player.itemAnimation == 1 ) {
				SpiritWalkLogic.ActivateIf( player, true );
			}
			return base.UseItem( player );
		}

		////////////////

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