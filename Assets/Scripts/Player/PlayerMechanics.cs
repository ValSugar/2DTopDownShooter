using Entity;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerMechanics : MonoBehaviour
    {
		[SerializeField] private float _movementSpeed;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private Transform _weaponSlot;

		private Transform _transform;
        private MovementBase _movement;
        private Inventory _inventory;
        private WeaponBase _weapon;

		private Camera _camera;

		private IInteractable _interactableObject;

		private void Awake()
		{
			_transform = transform;
			_movement = new StandartMovement(_transform, _movementSpeed);
			_inventory = new Inventory();
		}

		private void Start()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			if (_weapon != null)
				_weapon.TryFire();

			if (Input.GetKeyDown(KeyCode.Q))
				SetWeapon(_inventory.GetNextWeapon(_weapon));

			if (Input.GetKeyDown(KeyCode.E))
				TryInteract();
		}

		private void FixedUpdate()
		{
			RotateToMouse();

			_movement.Move();
		}

		private void RotateToMouse()
		{
			var pointToRotation = _camera.ScreenToWorldPoint(Input.mousePosition);
			var angle = Vector2.Angle(Vector2.up, pointToRotation - _transform.position);
			var toRotation = Quaternion.Euler(new Vector3(0f, 0f, _transform.position.x < pointToRotation.x ? -angle : angle));
			_transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
		}

		private void TryInteract()
		{
			if (_interactableObject != null)
				_interactableObject.Interact(this);
		}

		private void SetWeapon(WeaponBase newWeapon)
		{
			if (_weapon != null)
				_weapon.gameObject.SetActive(false);

			_weapon = newWeapon;
			_weapon.gameObject.SetActive(true);
		}

		public void AddWeapon(WeaponBase weapon)
		{
			var weaponGO = Instantiate(weapon, _weaponSlot.position, _weaponSlot.rotation, _weaponSlot);
			SetWeapon(weaponGO);
			_inventory.AddWeapon(weaponGO);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!collision.TryGetComponent(out IInteractable interactableObject))
				return;

			_interactableObject = interactableObject;
			InGameUI.SwitchPressButton(true);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (!collision.TryGetComponent(out IInteractable interactableObject))
				return;

			if (_interactableObject != interactableObject)
				return;

			_interactableObject = null;
			InGameUI.SwitchPressButton(false);
		}
	}
}
