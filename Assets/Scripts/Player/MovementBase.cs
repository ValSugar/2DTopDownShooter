using UnityEngine;

namespace Player
{
	public abstract class MovementBase
	{
		protected Transform _transform;
		protected float _speed;

		public abstract void Move();

		public MovementBase(Transform transform, float speed)
		{
			_transform = transform;
			_speed = speed;
		}
	}
}