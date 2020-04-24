using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkTanks
{ 
    public class TankController : MonoBehaviour
    {
        #region VARIABLES
        public const string CLASS_TAG = "Tank";

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
        
        // Match
        public TeamList Team { get; private set; }

        // References
        private Rigidbody m_RigidBody;
        private Camera m_Camera;
        private Animator m_Animator;
        private CanonController m_Canon;

        // AXIS - Left Joystick - MOVE
        private float m_MoveX;
        private float m_MoveY;

        // AXIS - Right Joystick - AIM
        private float m_RotX;
        private float m_RotY;

        // Other
        private float m_CurrentSpeed;
        private float m_SpeedDampTime = 0.1f;

        #endregion

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Camera = Camera.main;
            m_Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            m_Canon = new CanonController(CanonTransform, ProjectileSpawn, CanonRotationSpeed);
            LaserGameObject.SetActive(false);
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
        }

        #region MOVE

        private void MoveTank()
        {
            // Movimiento X-Z del input
            Vector3 movement = new Vector3(m_MoveX, 0f, m_MoveY);

            // Obtenemos el desplazamiento del Input
            m_CurrentSpeed = (movement.magnitude > 1 ? 1 : movement.magnitude);

            // Normalizamos y lo hacemos proporcional a la velocidad por segundo
            movement = movement.normalized * MaxSpeed * Time.deltaTime;

            // Rotamos el vector para que se ajuste a la rotación de la cámara
            movement = Quaternion.Euler(0, m_Camera.transform.eulerAngles.y, 0) * movement;

            // Desplazamos el personaje
            m_RigidBody.MovePosition(transform.position + (movement * m_CurrentSpeed));
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
            m_Canon.RotateCanon(m_RotX, m_RotY);
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
                m_Canon.Shoot();
            }
        }

        private void GetLaserInput()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                LaserGameObject.SetActive(true);
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                LaserGameObject.SetActive(false);
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

        private void UpdateAnimator()
        {
            //m_Animator.SetFloat("Speed", m_CurrentSpeed, m_SpeedDampTime, Time.deltaTime);
        }

        public void DestroyTank(bool scorePoint = false)
        {
            if (scorePoint)
            {
                GameManager.Instance.ScoreRivalPoint(Team);
            }
        }

        public void ResetState()
        {
            m_Canon.ResetState();
        }

        public void Spawn()
        {
            ResetState();
            SetPositionToSpawn();
        }

        private void SetPositionToSpawn()
        {
            SpawnPoint spawn = TeamManager.Instance.GetSpawn(this);

            if (spawn != null)
            {
                transform.position = spawn.transform.position;
                transform.rotation = spawn.transform.rotation;
            }
        }

        public void SetTeam(TeamList team)
        {
            this.Team = team;
            InitTeamSetup();
        }

        private void InitTeamSetup()
        {
            //TeamManager.Instance.AssetsTeam1
        }
    }
}