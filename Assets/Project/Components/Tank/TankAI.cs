using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankWars
{
    public class TankAI : TankBase
    {
        public override void Awake()
        {
            base.Awake();
            InitTank();
        }

        private void InitTank()
        {
            Canon = new CanonAI(this, CanonRotationSpeed);
            Laser(false);
        }
    }
}