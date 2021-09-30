using System.Collections.Generic;
using Weapons;

namespace Player
{
	public class Inventory
	{
		private List<WeaponBase> _weapons;

		public Inventory()
		{
			_weapons = new List<WeaponBase>();
		}

		public WeaponBase GetNextWeapon(WeaponBase weapon)
		{
			var index = _weapons.IndexOf(weapon);
			if (++index >= _weapons.Count)
				index = 0;

			return _weapons[index];
		}

		public void AddWeapon(WeaponBase weapon)
		{
			_weapons.Add(weapon);
		}
	}
}