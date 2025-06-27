using System.Collections;
using System.Collections.Generic;
using HexTecGames.SkillTreeSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [CreateAssetMenu(menuName = "HexTecGames/Sandbox/StatType")]
    public class StatType : ScriptableObject, IBigNodeStat
    {
        [SerializeField] private bool isPercent = default;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool IsPercent()
        {
            return isPercent;
        }
    }
}