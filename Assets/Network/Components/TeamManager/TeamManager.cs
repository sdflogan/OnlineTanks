using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace NetworkTanks
{
    public class TeamManager : Singleton<TeamManager>
    {
        public TankTeam Team1;
        public TankTeam Team2;

        public List<SpawnPoint> SpawnPointsTeam1;
        public List<SpawnPoint> SpawnPointsTeam2;

        public void InitTeams(List<TankController> tanks)
        {
            Team1.Init("Team_1", SpawnPointsTeam1);
            Team2.Init("Team_2", SpawnPointsTeam2);

            for (int i=0; i<tanks.Count; i++)
            {
                if (i % 2 == 0)
                {
                    Team1.AddMember(tanks[i]);
                }
                else
                {
                    Team2.AddMember(tanks[i]);
                }
            }
        }
    }
}