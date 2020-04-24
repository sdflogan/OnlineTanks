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
        public bool AlreadyInUse { get; private set; }

        public void Take()
        {
            AlreadyInUse = true;
        }

        public void Release()
        {
            AlreadyInUse = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}