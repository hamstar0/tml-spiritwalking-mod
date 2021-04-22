using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Logic;
using Microsoft.Xna.Framework;

namespace SpiritWalking.Items {
	public class ShadowMirrorItem : ModItem {
		public static readonly string[] TooltipLines = new string[] {
			"Activates 'spirit walking' on use",
			"When spirit walking, use the arrow keys to navigate",
			"Press 'jump' while spirit walking to return (also performs a spirit dash)",
		};

		public static readonly string[] TooltipQuoteLines  = new string[] {
			"\"Let my spirit carry me.\"",
			//"\"Why do I see a pink bunny?\"",
			"\"I dreamt I was a butterfly.\"",
			"\"Break on through to the other side.\""
		};

		private static int TooltipQuoteLineIdx = 0;
		


		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Shadow Mirror" );
			//this.Tooltip.SetDefault( string.Join("\n", ShadowMirrorItem.TooltipLines) );

			ShadowMirrorItem.TooltipQuoteLineIdx = Main.rand.Next( ShadowMirrorItem.TooltipQuoteLines.Length );
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
				lines.Add( "Requires "+(int)initNrgCost+"% anima to activate (by default)" );
				lines.Add( "Spirit walking drains anima while active" );
			} else {
				lines.Add( "Requires "+(int)initNrgCost+" mana to activate (by default)" );
				lines.Add( "Spirit walking drains mana while active" );
			}

			lines.Add( "Collisions or contact with open air cause faster drain" );

			lines.Add( ShadowMirrorItem.TooltipQuoteLines[ShadowMirrorItem.TooltipQuoteLineIdx] );

			//

			IEnumerable<TooltipLine> tooltipLines = lines.Select(
				(line, i) => new TooltipLine( this.mod, "ShadowMirror_"+i, line )
			);

			tooltips.InsertRange( 1, tooltipLines );
		}


		////////////////

		public override void HoldItem( Player player ) {
			if( player.itemAnimation <= 0 ) {
				return;
			}

			Vector2 pos = player.Center;
			Vector2 offset = player.direction > 0
				? new Vector2( 16, -16 )
				: new Vector2( -16, -16 );
			pos += player.gravDir > 0
				? offset
				: new Vector2( offset.X, -offset.Y );

			SpiritWalkFxLogic.EmitSpiritParticles(
				position: pos,
				direction: default,
				particles: 1,
				radius: 6
			);
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