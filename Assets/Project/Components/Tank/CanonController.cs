using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankWars
{
    public class CanonController : CanonBase
    {
        private Camera m_Camera;

        public CanonController(TankBase tank, float rotationSpeed) : base(tank, rotationSpeed)
        {
            m_Camera = Camera.main;
        }

        public override void RotateCanon(float axisValueX, float axisValueY)
        {
            // Rotación X-Z del input
            Vector3 rotation = new Vector3(axisValueX, 0f, axisValueY);

            // Rotamos el vector para que se ajuste a la rotación de la cámara
            rotation = Quaternion.Euler(0, m_Camera.transform.eulerAngles.y, 0) * rotation;

            if (rotation != Vector3.zero)
            {
                // Obtenemos la rotación final
                Quaternion quatR = Quaternion.LookRotation(rotation);

                // Interpolación para que la rotación se realice de forma suave
                Head.rotation = Quaternion.Lerp(Head.rotation, quatR, Time.deltaTime * RotationSpeed);
            }
        }
    }
}