using System;
using Terraria;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;
using SpiritWalking.Logic;


namespace SpiritWalking {
	[Serializable]
	class SpiritWalkStateProtocol : NetIOBroadcastPayload {
		public static void Broadcast( SpiritWalkingPlayer myplayer ) {
			var protocol = new SpiritWalkStateProtocol( myplayer.player.whoAmI, myplayer.IsSpiritWalking );
			NetIO.Broadcast( protocol );
		}



		////////////////

		public int PlayerWho;
		public bool IsOn;



		////////////////
		
		private SpiritWalkStateProtocol() { }

		public SpiritWalkStateProtocol( int plrWho, bool isOn ) {
			this.PlayerWho = plrWho;
			this.IsOn = isOn;
		}


		////////////////

		public override void ReceiveBroadcastOnClient() {
			this.Receive();
		}

		public override bool ReceiveOnServerBeforeRebroadcast( int fromWho ) {
			this.Receive();
			return true;
		}

		////

		private void Receive() {
			if( this.IsOn ) {
				SpiritWalkLogic.ActivateIf( Main.player[this.PlayerWho], false );
			} else {
				SpiritWalkLogic.DeactivateIf( Main.player[this.PlayerWho], false );
			}
		}
	}
}
