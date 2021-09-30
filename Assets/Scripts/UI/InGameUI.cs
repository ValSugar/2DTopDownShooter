using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        public static InGameUI Instance;

        [SerializeField] private Text _scoreCount;
		[SerializeField] private Text _timer;
		[SerializeField] private Text _weaponName;
        [SerializeField] private Text _missileCount;
		[SerializeField] private GameObject _pressButtonGO;
		[SerializeField] private GameObject _inGameMenu;
		[SerializeField] private GameObject _gameOverWindow;

		private void Awake()
		{
			Instance = this;
		}

		public static void SetWeapon(WeaponBase weapon)
		{
			Instance._weaponName.text = weapon.Name;
			weapon.onMissileCountChanged = Instance.OnMissileCountChanged;
		}

		public static void SetScore(int count)
		{
			Instance._scoreCount.text = $"Score: {count}";
		}

		public static void SetTimer(int count)
		{
			Instance._timer.text = $"{count}s";
		}

		public static void SwitchPressButton(bool flag)
		{
			Instance._pressButtonGO.SetActive(flag);
		}

		public static void SwitchInGameMenu()
		{
			Instance._inGameMenu.SetActive(!Instance._inGameMenu.activeSelf);
			Time.timeScale = Instance._inGameMenu.activeSelf ? 0f : 1f;
		}

		public static void ShowGameOverWindow()
		{
			Instance._gameOverWindow.SetActive(true);
			Time.timeScale = 0f;
		}

		private void OnMissileCountChanged(int currentCount, int maxCount)
		{
			_missileCount.text = $"{currentCount}/{maxCount}";
		}
    }
}
