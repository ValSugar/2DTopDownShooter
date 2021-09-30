using PoolObjects;
using UnityEngine;

namespace Weapons.Missiles
{
	public abstract class MissileBase : MonoBehaviour
	{
		[SerializeField] protected int _damage;
		[SerializeField] protected float _speed;

		protected Transform _transform;   //Chached for micro-optimization

		private void Awake()
		{
			_transform = transform;
		}

		protected virtual void OnDisable()
		{
			Pool.ReturnToPool(gameObject);
		}
	}
}