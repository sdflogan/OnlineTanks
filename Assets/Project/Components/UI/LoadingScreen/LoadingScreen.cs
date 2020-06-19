using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TankWars.UI
{
    public class LoadingScreen : Singleton<LoadingScreen>
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}