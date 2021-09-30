using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _newGame;
        [SerializeField] private Button _exit;

		private void Awake()
		{
            _newGame.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;
            });
            _exit.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
