using UnityEngine;
using System;
using Mirror;

namespace BHS
{
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(NetworkTransform))]
	public class LocalPlayer : NetworkBehaviour
	{
		private static float DurationImmunity = 3f;
		private static float DurationAttack = 3f;
		private static float XZSpeedDefault = 2f;
		private static float XZSpeedAttack = 5f;
		private static float MouseSpeed = 1f;

		private int _score = 0;
		private DateTime _hurtUntil = DateTime.MinValue;
		private DateTime _attackingUntil = DateTime.MinValue;

		private void OnBeingAttacked()
		{
			_hurtUntil = DateTime.Now.AddSeconds(DurationImmunity);
		}
		
		private void OnAttacking()
		{
			_attackingUntil = DateTime.Now.AddSeconds(DurationAttack);
		}

		private void Update()
		{
			if (!isLocalPlayer)
				return;

			bool attackStatus = DateTime.Now < _attackingUntil;
			bool hurtStatus = DateTime.Now < _hurtUntil;

			float xzspeed = attackStatus ? XZSpeedDefault : XZSpeedAttack;

			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			transform.Translate(new Vector3(verticalInput, 0f, -horizontalInput) * xzspeed * Time.deltaTime);
			transform.Rotate(0, Input.GetAxis("Mouse X") * MouseSpeed, 0);
		}

		private void FixedUpdate()
		{
			if (!isLocalPlayer)
				return;
		}
	}
}
