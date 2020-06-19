using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TankWars
{
    public class TankManager : Singleton<TankManager>
    {
        public List<TankBase> Tanks { get; private set; }

        public Transform TanksContainer;

        public TankController PlayerTank;
        public TankAI EnemyTank;

        private void Awake()
        {
            Tanks = new List<TankBase>();
        }

        public void LoadTanks()
        {
            CreateTanks();
        }

        private void CreateTanks()
        {
            CreatePlayerTank();
            CreateEnemyTanks();
            SpawnTanks();
        }

        private void SpawnTanks()
        {
            foreach(TankBase tank in Tanks)
            {
                SpawnTank(tank);
            }
        }

        private void CreatePlayerTank()
        {
            TankController playerTank = Instantiate(PlayerTank, TanksContainer);
            playerTank.gameObject.SetActive(false);
            StoreTank(playerTank);
        }

        private void CreateEnemyTanks()
        {
            for (int i = 0; i < 3; i++)
            {
                TankAI enemyTank = Instantiate(EnemyTank, TanksContainer);
                enemyTank.gameObject.SetActive(false);
                StoreTank(enemyTank);
            }
        }

        public void DestroyTank(TankBase tank)
        {
            DelayedSpawn(tank, 2f);
        }

        private void StoreTank(TankBase tank)
        {
            Tanks.Add(tank);
        }

        public void SpawnTank(TankBase tank)
        {
            SpawnPoint spawn = SpawnManager.Instance.GetRandomSpawn(tank);
            SpawnManager.Instance.LaunchSpawnSequence(spawn);

            tank.Spawn(spawn.transform.position, spawn.transform.rotation);
        }

        private Coroutine DelayedSpawn(TankBase tank, float delaySeconds)
        {
            return StartCoroutine(DelayedSpawnCoroutine(tank, delaySeconds));
        }

        private IEnumerator DelayedSpawnCoroutine(TankBase tank, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            SpawnTank(tank);
        }
    }
}