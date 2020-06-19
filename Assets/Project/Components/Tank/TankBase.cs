using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankWars
{
    public abstract class TankBase : MonoBehaviour
    {
        #region VARIABLES

        // Setup
        [Header("Tank")]
        public Transform BaseTransform;
        public float MaxSpeed;
        public float MaxRotationSpeed;

        [Header("Canon")]
        public Transform CanonTransform;
        public Transform ProjectileSpawn;
        public float CanonRotationSpeed = 1f;
        public GameObject LaserGameObject;

        protected CanonBase Canon;

        // Other
        protected float CurrentSpeed;
        protected float m_SpeedDampTime = 0.1f;

        private Animator m_Animator;

        #endregion

        public virtual void Awake()
        {
            m_Animator = GetComponent<Animator>();
        }

        public void Laser(bool enable)
        {
            LaserGameObject.SetActive(enable);
        }

        public void UpdateAnimator()
        {

        }

        public void DestroyTank(bool scorePoint = false)
        {
            // TODO
            // FX
            //DelayedSpawn(2f);
            TankManager.Instance.DestroyTank(this);
            gameObject.SetActive(false);
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            Canon.ResetState();
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            ResetState();

            transform.position = position;
            transform.rotation = rotation;
        }

        // Temporal hasta que incluyamos animación
        public Coroutine Reload()
        {
            return StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return new WaitForSeconds(2);
            Canon.FinishReloading();
        }
    }
}