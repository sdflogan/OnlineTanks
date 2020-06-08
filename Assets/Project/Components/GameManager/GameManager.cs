using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace NetworkTanks
{
    public class GameManager : Singleton<GameManager>
    {
        public void StartGame()
        {
            TeamManager.Instance.InitTeams(GetPlayerTanks());
            SpawnTanks(GetPlayerTanks());
        }

        public void ScoreRivalPoint(TeamList destroyedTankTeam)
        {
            TeamManager.Instance.ScoreRivalPoint(destroyedTankTeam);
        }

        // TODO: GET ALL PLAYER TANKS
        private List<TankController> GetPlayerTanks()
        {
            return null;
        }

        private void SpawnTanks(List<TankController> tanks)
        {
            foreach (TankController tank in tanks)
            {
                tank.Spawn();
            }
        }
    }
}