using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;

namespace BHS
{
	public class ScoreManager : MonoBehaviour
	{
		[SerializeField] private Text _label = null;
		
		public static ScoreManager Instance = null;
		
		private void Awake()
		{
			Instance = this;
		}

		public void Refresh(Dictionary<uint, PlayerInfo> dict)
		{
			StringBuilder sb = new StringBuilder("Score:\n", 1024);
			foreach (PlayerInfo pin in dict.Values)
			{
				sb.AppendFormat("Player {0}: \t{1}", pin.id, pin.score );
			}

			_label.text = sb.ToString();
		}
	}
}
