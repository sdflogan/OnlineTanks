using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TankWars.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("References")]
        public GameObject MainMenu;
        public GameObject TankSelector;

        private GameObject m_CurrentMenu;

        private void Awake()
        {
            m_CurrentMenu = MainMenu;
        }

        public void ShowMenu()
        {
            ChangeCurrentMenu(MainMenu);
        }

        public void ShowTankSelector()
        {
            ChangeCurrentMenu(TankSelector);
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }

        public void CloseGame()
        {
            GameManager.Instance.Quit();
        }

        private void ChangeCurrentMenu(GameObject newMenu)
        {
            m_CurrentMenu.SetActive(false);
            m_CurrentMenu = newMenu;
            m_CurrentMenu.SetActive(true);
        }
    }
}