using Location;
using PoolObjects;
using System;
using UnityEngine;

namespace Entity
{
    public class Dummy : MonoBehaviour, ITakeDamage
    {
		[SerializeField] private int _maxHealth;
		[SerializeField] private int _scoreCount;

		private int _currentHealth;

		private Action _onDie;

		private bool _isDead;

		private void OnEnable()
		{
			_currentHealth = _maxHealth;
			_isDead = false;
		}

		public void TakeDamage(int damage)
		{
			_currentHealth -= damage;
			if (_currentHealth > 0 || _isDead)
				return;

			_isDead = true;
			gameObject.SetActive(false);
			_onDie?.Invoke();
			LevelDirector.Instance.AddScore(_scoreCount);
		}

		public void SetDieCallback(Action onDie)
		{
			_onDie = onDie;
		}

		protected void OnDisable()
		{
			Pool.ReturnToPool(gameObject);
		}
	}
}
