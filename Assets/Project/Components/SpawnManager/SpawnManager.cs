using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TankWars
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public List<SpawnPoint> AvailableSpawns { get; private set; }

        private List<SpawnPoint> m_PickedSpawns = new List<SpawnPoint>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            GrabSpawnReferences();
        }
        
        public SpawnPoint GetRandomSpawn(TankBase tank)
        {
            SpawnPoint spawn = GetRandomAvailableSpawn();
            PickSpawn(spawn, tank);
            return spawn;
        }

        public void LaunchSpawnSequence(SpawnPoint spawn)
        {
            // TODO:
            StartCoroutine(ReleaseSpawnAfterSeconds(spawn, 2));
        }

        private void ReleaseSpawn(SpawnPoint spawn)
        {
            AvailableSpawns.Add(spawn);
            m_PickedSpawns.Remove(spawn);

            spawn.Release();
        }

        private void PickSpawn(SpawnPoint spawn, TankBase tank)
        {
            AvailableSpawns.Remove(spawn);
            m_PickedSpawns.Add(spawn);

            spawn.Take(tank);
        }

        private SpawnPoint GetRandomAvailableSpawn()
        {
            return AvailableSpawns[Random.Range(0, AvailableSpawns.Count)];
        }

        private void GrabSpawnReferences()
        {
            SpawnPoint[] sceneSpawns = GameObject.FindObjectsOfType<SpawnPoint>();
            AvailableSpawns = new List<SpawnPoint>(sceneSpawns);
        }

        private IEnumerator ReleaseSpawnAfterSeconds(SpawnPoint spawn, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ReleaseSpawn(spawn);
        }
    }
}