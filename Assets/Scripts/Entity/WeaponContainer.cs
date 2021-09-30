using Player;
using UnityEngine;
using Weapons;

namespace Entity
{
	public class WeaponContainer : MonoBehaviour, IInteractable
	{
		[SerializeField] private WeaponBase _weaponPrefab;

		public void Interact(PlayerMechanics playerMechanics)
		{
			playerMechanics.AddWeapon(_weaponPrefab);
			gameObject.SetActive(false);
		}
	}
}
