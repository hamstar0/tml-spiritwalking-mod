using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using HamstarHelpers.Services.UI.LayerDisable;
using System;

namespace SpiritWalking {
	public class SpiritWalkingMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-spiritwalking-mod";


		////////////////
		
		public static SpiritWalkingMod Instance => ModContent.GetInstance<SpiritWalkingMod>();



		////////////////

		internal Mod NecrotisMod = null;




		////////////////

		public override void Load() {
			this.NecrotisMod = ModLoader.GetMod( "Necrotis" );

			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				Main.instance.LoadNPC( NPCID.DungeonSpirit );

				Mod hudMod = ModLoader.GetMod( "HUDElementsLib" );
				if( hudMod != null ) {
					Func<string, bool> hook = name => name == "Anima Gauge";

					hudMod.Call( "AddWidgetViewHook", hook );
				}
			}
		}


		////////////////
		
		public override void UpdateMusic( ref int music, ref MusicPriority priority ) {
			var plr = Main.LocalPlayer;

			if( Main.myPlayer == -1 || Main.gameMenu || !plr.active ) {
				return;
			}

			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.IsSpiritWalking ) {
				music = this.GetSoundSlot( SoundType.Music, "Sounds/Music/MattsBlues" );
				priority = MusicPriority.BossHigh;
			}
		}


		public override void ModifyLightingBrightness( ref float scale ) {
			var plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.IsSpiritWalking ) {
				scale = 0.95f + (Main.rand.NextFloat() * 0.05f);
			}
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( SpiritWalkingConfig.Instance.DebugModeInfo ) {
				return;
			}

			var plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<SpiritWalkingPlayer>();

			if( myplayer.IsSpiritWalking ) {
				layers.ForEach( l => {
					switch( l.Name ) {
					case LayerDisable.AchievementCompletePopups:
					case LayerDisable.CaptureManagerCheck:
					case LayerDisable.Cursor:
					case LayerDisable.DeathText:
					case LayerDisable.DebugStuff:
					case LayerDisable.DiagnoseNet:
					case LayerDisable.DiagnoseVideo:
					case LayerDisable.EmoteBubbles:
					case LayerDisable.EntityHealthBars:
					case LayerDisable.IngameOptions:
					case LayerDisable.MouseOver:
					case LayerDisable.MouseText:
					case LayerDisable.MPPlayerNames:
					case LayerDisable.NPCOrSignDialog:
					case LayerDisable.PlayerChat:
					case LayerDisable.ResourceBars:
					case LayerDisable.SettingsButton:
					case LayerDisable.MapOrMinimap:
						break;
					default:
						if( l.Name.Contains("HUDElementsLib: Widgets") ) {
							break;
						}

						l.Active = false;
						break;
					}
				} );
			}
		}
	}
}