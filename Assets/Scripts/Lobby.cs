using UnityEngine;
using System.Collections.Generic;
using Mirror;

namespace BHS
{
	public class Lobby : NetworkRoomManager
	{
		public static Lobby Instance = null;
		public const float MatchFinishTime = 5f;
		private static Dictionary<uint, PlayerInfo> PlayerList = new Dictionary<uint, PlayerInfo>();

		public void RefreshScore(PlayerInfo pin)
		{
			if (!PlayerList.ContainsKey(pin.id))
			{
				PlayerList.Add(pin.id, pin);
				ScoreManager.Instance.Refresh(PlayerList);
			}
		}

		public override void Awake()
		{
			base.Awake();
			Instance = this;
		}

		private bool showStartButton;
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
