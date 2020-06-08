using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace TankWars
{
    public class TeamManager : Singleton<TeamManager>
    {
        public TankTeam Team1;
        public TankTeam Team2;

        public List<SpawnPoint> SpawnPointsTeam1;
        public List<SpawnPoint> SpawnPointsTeam2;

        public TeamAssetsContainer AssetsTeam1;
        public TeamAssetsContainer AssetsTeam2;

        public void InitTeams(List<TankController> tanks)
        {
            InitTeamsParameters();
            PopulateTeams(tanks);
            AssignSpawnPoints();
        }

        private void InitTeamsParameters()
        {
            Team1.Init(TeamList.Team1, SpawnPointsTeam1);
            Team2.Init(TeamList.Team2, SpawnPointsTeam2);
        }

        private void PopulateTeams(List<TankController> tanks)
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                if (i % 2 == 0)
                {
                    SetTankTeam(tanks[i], TeamList.Team1);
                }
                else
                {
                    SetTankTeam(tanks[i], TeamList.Team2);
                }
            }
        }

        private void AssignSpawnPoints()
        {
            Team1.AssignSpawnPoints();
            Team2.AssignSpawnPoints();
        }

        public void SetTankTeam(TankController tank, TeamList team)
        {
            GetTeam(team).AddMember(tank);
            tank.SetTeam(team);
        }

        public SpawnPoint GetSpawn(TankController tank)
        {
            return (GetTeam(tank.Team).GetSpawnPoint(tank));
        }

        public void ScoreRivalPoint(TeamList destroyedTankTeam)
        {
            GetRivalTeam(destroyedTankTeam).IncrementPoint();
        }

        private TankTeam GetRivalTeam(TeamList team)
        {
            TankTeam rivalTeam;

            switch (team)
            {
                case TeamList.Team1:
                    rivalTeam = Team2;
                    break;

                case TeamList.Team2:
                    rivalTeam = Team1;
                    break;

                default:
                    rivalTeam = null;
                    break;
            }

            return rivalTeam;
        }

        private TankTeam GetTeam(TeamList team)
        {
            TankTeam tankTeam;

            switch (team)
            {
                case TeamList.Team1:
                    tankTeam = Team1;
                    break;

                case TeamList.Team2:
                    tankTeam = Team2;
                    break;

                default:
                    tankTeam = null;
                    break;
            }

            return tankTeam;
        }
    }
}