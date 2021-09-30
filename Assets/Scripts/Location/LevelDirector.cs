using System.Collections;
using UI;
using UnityEngine;

namespace Location
{
    public class LevelDirector : MonoBehaviour
    {
        public static LevelDirector Instance;

        private int _encountersCount;
        private int _score;
        private int _timer;

		private void Awake()
		{
            Instance = this;
            StartCoroutine(GameTimer());
        }

		private void Update()
		{
            if (Input.GetKeyDown(KeyCode.Escape))
                InGameUI.SwitchInGameMenu();
		}

		public void AddEncounter(Encounter encounter)
		{
            _encountersCount++;
            encounter.onEnd = OnEnconterEnd;
        }

        public void AddScore(int count)
		{
            _score += count;
            InGameUI.SetScore(_score);
        }

        private void OnEnconterEnd()
		{
            if (--_encountersCount <= 0)
                InGameUI.ShowGameOverWindow();
        }

        private IEnumerator GameTimer()
		{
            while (true)
            {
                _timer++;
                InGameUI.SetTimer(_timer);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
