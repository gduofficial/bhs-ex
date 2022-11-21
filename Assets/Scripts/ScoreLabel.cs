using UnityEngine;
using UnityEngine.UI;

namespace BHS
{
	public class ScoreLabel : MonoBehaviour
	{
		private Text _label = null;
		
		private void Awake()
		{
			_label = this.GetComponent<Text>();
		}

		public void SetScore(int score)
		{
			if (_label == null)
			{
				Debug.LogWarning("No Text component found");
				return;
			}

			_label.text = score.ToString();
		}
	}
}
