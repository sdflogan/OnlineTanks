using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace TankWars
{
    public class CanonBase
    {
        public Transform Head;
        public Transform ProjectileSpawn;
        public float RotationSpeed = 1f;
        public bool Reloading { get; private set; }

        public TankBase Tank;

        public CanonBase(TankBase tank, float rotationSpeed)
        {
            this.Tank = tank;
            this.Head = tank.CanonTransform;
            this.ProjectileSpawn = tank.ProjectileSpawn;
            this.RotationSpeed = rotationSpeed;
        }

        public void Shoot()
        {
            if (!Reloading)
            {
                ProjectileReflection projectile = ProjectileManager.Instance.SpawnProjectile();
                projectile.Init(ProjectileSpawn);
                // Temporal
                Reload();
            }
        }

        public void Reload()
        {
            Reloading = true;
            Tank.Reload();
        }

        public void FinishReloading()
        {
            Reloading = false;
        }

        public void ResetState()
        {
            Reloading = false;
            Head.rotation = Quaternion.Euler(Vector3.zero);
        }

        public virtual void RotateCanon(float axisValueX, float axisValueY)
        {

        }
    }
}