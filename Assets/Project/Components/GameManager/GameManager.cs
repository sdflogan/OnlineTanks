using System.Collections;
using System.Collections.Generic;
using TankWars.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace TankWars
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            LoadMainMenu();
        }

        public void StartGame()
        {
            LoadBattleScene();
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void LoadMainMenu()
        {
            StartCoroutine(LoadMainMenuCoroutine());
        }

        private void LoadBattleScene()
        {
            StartCoroutine(LoadBattleSceneCoroutine());
        }

        private IEnumerator LoadMainMenuCoroutine()
        {
            LoadingScreen.Instance.Enable();
            AsyncOperation loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            while (!loading.isDone)
            {
                yield return new WaitForSeconds(0.25f);
            }

            LoadingScreen.Instance.Disable();
        }

        private IEnumerator LoadBattleSceneCoroutine()
        {
            LoadingScreen.Instance.Enable();
            AsyncOperation loading = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            AsyncOperation unloading = SceneManager.UnloadSceneAsync(1);

            while (!loading.isDone || !unloading.isDone)
            {
                yield return new WaitForSeconds(0.25f);
            }

            LoadingScreen.Instance.Disable();

            TankManager.Instance.LoadTanks();
        }

    }
}