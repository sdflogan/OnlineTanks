using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NetworkTanks
{
    public class SpawnPoint : MonoBehaviour
    {
        public TankController Owner { get; private set; }

        public bool IsEmpty()
        {
            return Owner == null;
        }

        public void Take(TankController tank)
        {
            Owner = tank;
        }

        public void Release()
        {
            Owner = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}