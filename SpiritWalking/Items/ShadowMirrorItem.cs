using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritWalking.Logic;


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
			var lines = ShadowMirrorItem.TooltipLines.ToList();

			if( SpiritWalkingConfig.SpiritWalkUsesAnima ) {
				float initAnimaCost = config.Get<float>( nameof(config.InitialSpiritWalkAnimaPercentCost) );

				lines.Add( "Requires "+(int)initAnimaCost+"% anima to activate (by default)" );
				lines.Add( "Spirit walking drains anima while active" );
			} else {
				int initManaCost = config.Get<int>( nameof(config.InitialSpiritWalkManaCost) );

				lines.Add( "Requires "+initManaCost+" mana to activate (by default)" );
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

		public override void AddRecipes() {
			int shadowMirrorType = ModContent.ItemType<ShadowMirrorItem>();
			var recipe1 = new ShadowMirrorRecipe( ItemID.MagicMirror, shadowMirrorType, true );
			var recipe2 = new ShadowMirrorRecipe( shadowMirrorType, ItemID.MagicMirror, false );
			recipe1.AddRecipe();
			recipe2.AddRecipe();
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

		 private static bool PickupMessageShown = false;

		public override void UpdateInventory( Player player ) {
			if( !ShadowMirrorItem.PickupMessageShown ) {
				ShadowMirrorItem.PickupMessageShown = true;

				if( ModLoader.GetMod("Messages") != null ) {
					ShadowMirrorItem.DisplayPickupMessage_WeakRef_Messages();
				}
			}
		}

		////

		private static void DisplayPickupMessage_WeakRef_Messages() {
			Messages.MessagesAPI.AddMessage(
				title: "Spirit Walking",
				description: "When the Shadow Mirror is used (see tooltips for requirements), you will enter a"
					+" state known as 'spirit walking'. This state lets you travel around as if you were a spirit:"
					+" Nothing can hurt you, and you can fly in any direction you please. Your time in this mode is"
					+" limited, however. While 'walking', you may see blue and red orbs lying around. Blue orbs will"
					+" increase your walk duration on pickup, but red orbs will shorten it."
					+"\n \nWhile walking, you become much smaller than usual, and the world may appear differently."
					+" Activating 'spirit dash' will end your walk, but give you a short jump that can be used to"
					+" get past obstacles.",
				modOfOrigin: SpiritWalkingMod.Instance,
				alertPlayer: true,
				isImportant: false,
				parentMessage: Messages.MessagesAPI.GameInfoCategoryMsg,
				id: "SpirtWalking_Overview"
			);
		}
	}
}