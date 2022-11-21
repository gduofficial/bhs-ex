using UnityEngine;

namespace BHS
{
	public class MouseController
	{
		public static void Lock()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		public static void Unlock()
		{
			Cursor.lockState = CursorLockMode.None;
		}

		public static void Toggle()
		{
			if (Cursor.lockState == CursorLockMode.None)
				Lock();
			else
				Unlock();
		}
	}
}
