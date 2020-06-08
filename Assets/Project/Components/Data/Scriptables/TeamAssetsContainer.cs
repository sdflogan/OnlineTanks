using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankWars
{
    [CreateAssetMenu(fileName = "TeamAssetsData", menuName = "DataObjects/TeamAssetsData")]
    public class TeamAssetsContainer : ScriptableObject
    {
        public Material TankMaterial;
        public Material ProjectileMaterial;
    }
}