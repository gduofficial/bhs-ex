using UnityEngine;

namespace BHS
{
	public class MouseController
	{
		public static void Lock()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Debug.Log("Mouse lock");
		}

		public static void Unlock()
		{
			Cursor.lockState = CursorLockMode.None;
			Debug.Log("Mouse unlock");
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
