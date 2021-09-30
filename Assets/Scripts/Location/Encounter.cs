using Entity;
using PoolObjects;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Location
{
	public class Encounter : MonoBehaviour
	{
		[SerializeField] private GameObject[] _objectsToEnableOnStart;
		[SerializeField] private GameObject[] _objectsToEnableOnEnd;
		[SerializeField] private GameObject[] _objectsToDisableOnStart;
		[SerializeField] private GameObject[] _objectsToDisableOnEnd;
		[SerializeField] private Dummy _dummyPrefab;
		[SerializeField] private float _spawnRadius;
		[SerializeField] private float _spawnTimeMin;
		[SerializeField] private float _spawnTimeMax;
		[SerializeField] private int _spawnCountPerTimerMin;
		[SerializeField] private int _spawnCountPerTimerMax;
		[SerializeField] private int _maxDummyesToSpawn;

		private int _currentSpawnDummyesCount;
		private int _currentDeadDummyesCount;

		private Coroutine _spawnCoroutine;

		public Action onEnd;

		private void Start()
		{
			LevelDirector.Instance.AddEncounter(this);
		}

		private IEnumerator EncounterCoroutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(Random.Range(_spawnTimeMin, _spawnTimeMax));
				var count = Random.Range(_spawnCountPerTimerMin, _spawnCountPerTimerMax);
				if (_currentSpawnDummyesCount + count > _maxDummyesToSpawn)
					count = _maxDummyesToSpawn - _currentSpawnDummyesCount;

				_currentSpawnDummyesCount += count;

				while (count > 0)
				{
					count--;
					var spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * _spawnRadius;
					var spawnRotation = Quaternion.Euler(0f, 0f, Random.Range(-180, 180));
					var dummy = Pool.SpawnObject(_dummyPrefab, spawnPosition, spawnRotation);
					dummy.SetDieCallback(OnDummyDie);
				}
			}
		}

		private void OnDummyDie()
		{
			if (++_currentDeadDummyesCount >= _maxDummyesToSpawn)
				End();
		}

		private void End()
		{
			foreach (var obj in _objectsToEnableOnEnd)
				obj.SetActive(true);

			foreach (var obj in _objectsToDisableOnEnd)
				obj.SetActive(false);

			if (_spawnCoroutine != null)
				StopCoroutine(_spawnCoroutine);

			onEnd?.Invoke();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			foreach (var obj in _objectsToEnableOnStart)
				obj.SetActive(true);

			foreach (var obj in _objectsToDisableOnStart)
				obj.SetActive(false);

			_spawnCoroutine = StartCoroutine(EncounterCoroutine());
		}
	}
}
