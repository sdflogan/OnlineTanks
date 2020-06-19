using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankWars
{ 
    public class TankController : TankBase
    {
        #region VARIABLES
        
        // References
        private Rigidbody m_RigidBody;
        private Camera m_Camera;

        // AXIS - Left Joystick - MOVE
        private float m_MoveX;
        private float m_MoveY;

        // AXIS - Right Joystick - AIM
        private float m_RotX;
        private float m_RotY;

        #endregion

        public override void Awake()
        {
            base.Awake();
            m_RigidBody = GetComponent<Rigidbody>();
            m_Camera = Camera.main;
            InitTank();
        }

        private void Update()
        {
            GetInputs();
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            MoveTank();
            RotateBase();
            RotateCanon();
            FixPhysics();
        }

        private void FixPhysics()
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }

        private void InitTank()
        {
            Canon = new CanonController(this, CanonRotationSpeed);
            Laser(false);
        }

        #region MOVE

        private void MoveTank()
        {
            // Movimiento X-Z del input
            Vector3 movement = new Vector3(m_MoveX, 0f, m_MoveY);

            // Obtenemos el desplazamiento del Input
            CurrentSpeed = (movement.magnitude > 1 ? 1 : movement.magnitude);

            // Normalizamos y lo hacemos proporcional a la velocidad por segundo
            movement = movement.normalized * MaxSpeed * Time.deltaTime;

            // Rotamos el vector para que se ajuste a la rotación de la cámara
            movement = Quaternion.Euler(0, m_Camera.transform.eulerAngles.y, 0) * movement;

            // Desplazamos el personaje
            m_RigidBody.MovePosition(transform.position + (movement * CurrentSpeed));
        }

        private void RotateBase()
        {
            // Rotación X-Z del input
            Vector3 rotation = new Vector3(m_MoveX, 0f, m_MoveY);

            // Rotamos el vector para que se ajuste a la rotación de la cámara
            rotation = Quaternion.Euler(0, m_Camera.transform.eulerAngles.y, 0) * rotation;

            if (rotation != Vector3.zero)
            {
                // Obtenemos la rotación final
                Quaternion quatR = Quaternion.LookRotation(rotation);

                // Interpolación para que la rotación se realice de forma suave
                BaseTransform.rotation = (Quaternion.Lerp(BaseTransform.rotation, quatR, Time.deltaTime * MaxRotationSpeed));
            }
        }

        private void RotateCanon()
        {
            Canon.RotateCanon(m_RotX, m_RotY);
        }

        #endregion

        #region INPUTS

        private void GetInputs()
        {
            GetMoveInputs();
            GetShootInput();
            GetLaserInput();
        }

        private void GetShootInput()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Canon.Shoot();
            }
        }

        private void GetLaserInput()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Laser(true);
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                Laser(false);
            }
        }

        private void GetMoveInputs()
        {
            m_MoveX = Input.GetAxis("Horizontal");
            m_MoveY = Input.GetAxis("Vertical");
            m_RotX = Input.GetAxis("Mouse X");
            m_RotY = Input.GetAxis("Mouse Y");
        }

        #endregion
    }
}