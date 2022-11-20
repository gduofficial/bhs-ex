using UnityEngine;
using Mirror;

namespace BHS
{
	public class Lobby : NetworkRoomManager
	{
		bool showStartButton;
		public override void OnRoomServerPlayersReady()
		{
#if UNITY_SERVER
			base.OnRoomServerPlayersReady();
#else
			showStartButton = true;
#endif
		}

		public override void OnGUI()
		{
			base.OnGUI();
			if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
			{
				showStartButton = false;
				ServerChangeScene(GameplayScene);
			}
		}
	}
}
