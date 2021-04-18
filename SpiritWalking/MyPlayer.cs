using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using SpiritWalking.Logic;
using SpiritWalking.Items;


namespace SpiritWalking {
	public partial class SpiritWalkingPlayer : ModPlayer {
		internal bool IsSpiritWalking = false;


		internal Projectile FlightProjectile = null;


		internal Vector2 IntendedFlightVelocity = SpiritWalkFlightLogic.DefaultFlightHeading;

		internal float CurrentFlightSpeedScale = 1f;


		internal int FlightBurstCooldown = 0;

		internal int FlightBurstDuration = 0;



		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( this.player.itemAnimation > 0 && this.player.HeldItem?.type == ModContent.ItemType<ShadowMirrorItem>() ) {
				this.player.itemAnimation = 0;

				Main.NewText( "Mirror use interrupted.", Color.Yellow );
			}
		}
	}
}