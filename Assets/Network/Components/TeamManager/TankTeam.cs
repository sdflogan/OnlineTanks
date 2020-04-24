using System.Collections.Generic;

namespace NetworkTanks
{
    public class TankTeam
    {
        public string TeamID { get; private set; }
        public int Points { get; private set; }
        public List<TankController> Members { get; private set; }
        public List<SpawnPoint> SpawnPoints { get; private set; }

        public TankTeam(List<SpawnPoint> spawnPoints)
        {
            Init(string.Empty, new List<TankController>(), spawnPoints);
        }

        public TankTeam(string teamID, List<TankController> tanks, List<SpawnPoint> spawnPoints)
        {
            Init(teamID, tanks, spawnPoints);
        }

        public void Init(string teamID, List<SpawnPoint> spawnPoints)
        {
            Init(teamID, new List<TankController>(), spawnPoints);
        }

        public void Init(string teamID, List<TankController> tanks, List<SpawnPoint> spawnPoints)
        {
            this.Points = 0;
            this.TeamID = teamID;
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

        public SpawnPoint GetSpawnPoint(int index)
        {
            return SpawnPoints[index];
        }
    }
}