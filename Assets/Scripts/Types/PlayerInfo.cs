using UnityEngine;
using Mirror;

namespace BHS
{
	public class PlayerInfo
	{
		public uint id;
		public bool isLocal;
		public int score;
		
		public PlayerInfo(NetworkBehaviour in_nb, int in_score )
		{
			score = in_score;
			id = in_nb.netId;
			isLocal = in_nb.isLocalPlayer;
		}
	}
}
