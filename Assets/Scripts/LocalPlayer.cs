using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections.Generic;
using Mirror;

namespace BHS
{
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(NetworkTransform))]
	public class LocalPlayer : NetworkBehaviour
	{
		[SerializeField] private Material _playerMaterial = null;
		[SerializeField] private GameObject _camera = null;

        [Header("BehaviourConfig")]
		[SerializeField] private Color _colorNormal = Color.white;
		[SerializeField] private Color _colorImmunity = Color.black;
		[SerializeField] private Color _colorAttack = Color.red;
		[SerializeField] private float _durationImmunity = 3f;
		[SerializeField] private float _durationAttack = 3f;
		[SerializeField] private float _speedXZDefault = 2f;
		[SerializeField] private float _speedXZAttack = 5f;
		[SerializeField] private float _speedMouselook = 4f;

		private bool _awakened = false;
		private int _score = 0;
		private DateTime _hurtUntil = DateTime.MinValue;
		private DateTime _attackingUntil = DateTime.MinValue;
		private string playerName = "";

		public bool IsAttacking()
		{
			return DateTime.Now <= _attackingUntil;
		}
		
		public bool IsImmune()
		{
			return DateTime.Now <= _hurtUntil;
		}

		[Command]
		private void OnBeingAttacked()
		{
			_hurtUntil = DateTime.Now.AddSeconds(_durationImmunity);
			SetMaterialColor(_colorImmunity);
		}

		[Command]
		private void OnAttacking()
		{
			_attackingUntil = DateTime.Now.AddSeconds(_durationAttack);
			SetMaterialColor(_colorAttack);
		}

		[Command]
		private void SetMaterialColor(Color inc)
		{
			GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", inc);
		}

		[Command]
		private void BumpScore()
		{
			_score++;
			Lobby.Instance.RefreshScore(new PlayerInfo(this, _score));
		}

		#region Behaviour
		public override void OnStartLocalPlayer()
		{
			MouseController.Lock();
			playerName = (string)connectionToClient.authenticationData;
		}

		public override void OnStopLocalPlayer()
		{
			MouseController.Unlock();
		}

		private void Awake()
		{
			GetComponentInChildren<MeshRenderer>().material = new Material(_playerMaterial);

			_awakened = true;
		}

		private void Start()
		{
			_camera.SetActive(isLocalPlayer);
		}

		private void Update()
		{
			if (!isLocalPlayer || !_awakened)
				return;

			if (!IsAttacking() && Input.GetButton("Fire1"))
				OnAttacking();

			/* Movement and camera */
			float xzspeed = IsAttacking() ? _speedXZAttack : _speedXZDefault;
			float horizontalInput = -Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			transform.Translate(new Vector3(verticalInput, 0f, horizontalInput) * xzspeed * Time.deltaTime);
			transform.Rotate(0, Input.GetAxis("Mouse X") * _speedMouselook, 0);

			if (Input.GetButton("Cancel"))
				MouseController.Toggle();

			/* Refreshing colors */
			if (IsAttacking())
				SetMaterialColor(_colorAttack);
			else if (IsImmune())
				SetMaterialColor(_colorImmunity);
			else
				SetMaterialColor(_colorNormal);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
				return;
				
			if (collision.gameObject.GetComponent<LocalPlayer>().IsAttacking() && !this.IsImmune())
			{
				OnBeingAttacked();
				return;
			}
			
			if (!collision.gameObject.GetComponent<LocalPlayer>().IsImmune() && this.IsAttacking())
			{
				BumpScore();
			}
		}
		#endregion
	}
}
