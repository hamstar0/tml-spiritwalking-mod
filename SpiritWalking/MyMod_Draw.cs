using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsUI.Services.UI.LayerDisable;


namespace SpiritWalking {
	public partial class SpiritWalkingMod : Mod {
		private void InitializeUI() {
			Mod hudMod = ModLoader.GetMod( "HUDElementsLib" );

			if( hudMod != null ) {
				Func<string, bool> hook = (name) => {
					return name == "Anima Gauge" || !Main.LocalPlayer.GetModPlayer<SpiritWalkingPlayer>().IsSpiritWalking;
				};

				hudMod.Call( "AddWidgetVisibilityHook", hook );
			}
		}


		////////////////

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
						if( !l.Name.Contains("HUDElementsLib: Widgets") ) {
							l.Active = false;
						}
						break;
					}
				} );
			}
		}
	}
}