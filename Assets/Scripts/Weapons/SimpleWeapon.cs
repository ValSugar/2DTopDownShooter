using PoolObjects;
using UnityEngine;

namespace Weapons
{
    public class SimpleWeapon : WeaponBase
    {
        [SerializeField] private float _spreadRate;

        protected override void Fire()
		{
            var rotation = _muzzle.rotation.eulerAngles;
            rotation.z += Random.Range(-_spreadRate, _spreadRate);
            Pool.SpawnObject(_missilePrefab, _muzzle.position, Quaternion.Euler(rotation));
		}
    }
}
