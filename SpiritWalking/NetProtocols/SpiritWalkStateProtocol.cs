using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Services.Network.SimplePacket;
using SpiritWalking.Logic;


namespace SpiritWalking {
	[Serializable]
	class SpiritWalkStateProtocol : SimplePacketPayload {
		public static void Broadcast( SpiritWalkingPlayer myplayer ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				throw new ModLibsException( "Not client" );
			}

			var packet = new SpiritWalkStateProtocol( myplayer.player.whoAmI, myplayer.IsSpiritWalking );

			SimplePacket.SendToServer( packet );
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

		public override void ReceiveOnServer( int fromWho ) {
			this.Receive();

			SimplePacket.SendToClient( this, -1, fromWho );
		}

		public override void ReceiveOnClient() {
			this.Receive();
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
