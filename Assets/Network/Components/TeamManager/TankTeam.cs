using System.Collections.Generic;
using UnityEngine;

namespace NetworkTanks
{
    public class TankTeam
    {
        public TeamList Team { get; private set; }
        public int Points { get; private set; }
        public List<TankController> Members { get; private set; }
        public List<SpawnPoint> SpawnPoints { get; private set; }

        public TankTeam(List<SpawnPoint> spawnPoints)
        {
            Init(TeamList.Undefined, new List<TankController>(), spawnPoints);
        }

        public TankTeam(TeamList teamID, List<TankController> tanks, List<SpawnPoint> spawnPoints)
        {
            Init(teamID, tanks, spawnPoints);
        }

        public void Init(TeamList teamID, List<SpawnPoint> spawnPoints)
        {
            Init(teamID, new List<TankController>(), spawnPoints);
        }

        public void Init(TeamList teamID, List<TankController> tanks, List<SpawnPoint> spawnPoints)
        {
            this.Points = 0;
            this.Team = teamID;
            this.Members = tanks;
            this.SpawnPoints = spawnPoints;
        }

        public void IncrementPoint()
        {
            Points++;
        }

        public void AddMember(TankController tank)
        {
            Members.Add(tank);
        }

        public void RemoveMember(TankController tank)
        {
            Members.Remove(tank);
        }

        public SpawnPoint GetSpawnPoint(TankController tank)
        {
            return SpawnPoints.Find(x => x.Owner == tank);
        }

        public bool Find(TankController tank)
        {
            return Members.Contains(tank);
        }

        public void AssignSpawnPoints()
        {
            if (SpawnPoints.Count >= Members.Count)
            {
                int currentSpawnIndex = 0;

                for (int memberIndex=0; memberIndex<Members.Count; memberIndex++)
                {
                    TankController currentTank = Members[memberIndex];
                    bool foundEmptySpawn = false;

                    for (int spawnIndex=currentSpawnIndex; spawnIndex<SpawnPoints.Count && !foundEmptySpawn; spawnIndex++)
                    {
                        SpawnPoint currentSpawn = SpawnPoints[spawnIndex];

                        if (SpawnPoints[spawnIndex].IsEmpty())
                        {
                            currentSpawn.Take(currentTank);
                            currentSpawnIndex++;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("[SpawnsError]: There are more tanks than spawns.");
            }
        }
    }
}