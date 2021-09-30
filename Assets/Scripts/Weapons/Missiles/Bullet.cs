using Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons.Missiles
{
	public class Bullet : MissileBase
	{
		public const float DISTANCE_CHECK = 100f;
		public const int EXCLUDED_LAYER = 1 << 6;

		private ITakeDamage _target;
		private Vector2 _targetPosition;

		private void OnEnable()
		{
			var hit = Physics2D.Raycast(transform.position, transform.up, DISTANCE_CHECK, ~EXCLUDED_LAYER);
			if (hit.transform == null)
			{
				_targetPosition = _transform.position + _transform.up * DISTANCE_CHECK;
				return;
			}

			_targetPosition = hit.point;
			hit.transform.TryGetComponent(out _target);
		}

		private void FixedUpdate()
		{
			if (Vector2.Distance(_transform.position, _targetPosition) > 0.1f)
			{
				_transform.position = Vector2.MoveTowards(_transform.position, _targetPosition,  _speed * Time.fixedDeltaTime);
				return;
			}

			if (_target != null)
				_target.TakeDamage(_damage);
			gameObject.SetActive(false);
		}
	}
}
