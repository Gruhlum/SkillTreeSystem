using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SkillTreeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/SkillTreeSystem/BigNodeStatsCollection")]
    public class BigNodeStatsCollection : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> bigNodeStats = new List<ScriptableObject>();


        private void OnValidate()
        {
            if (bigNodeStats == null || bigNodeStats.Count <= 0)
            {
                return;
            }

            for (int i = bigNodeStats.Count - 1; i >= 0; i--)
            {
                if (bigNodeStats[i] != null && bigNodeStats[i] is not IBigNodeStat)
                {
                    Debug.Log($"{bigNodeStats[i]} has to inherit from {nameof(IBigNodeStat)}");
                    bigNodeStats.RemoveAt(i);
                }
            }
        }

        public List<IBigNodeStat> GetStats()
        {
            List<IBigNodeStat> results = new List<IBigNodeStat>();

            foreach (var item in bigNodeStats)
            {
                results.Add(item as IBigNodeStat);
            }

            return results;
        }
    }
}