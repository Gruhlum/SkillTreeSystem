using System.Collections;
using System.Collections.Generic;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [System.Serializable]
    public class BigNodeSaveData : MultiObjectSaveData<BigNode, BigNodeData, BigNodeVisual>
    {
        public ScriptableObject SO;
        public int increasePerNode = 2;

        public BigNodeSaveData(BigNode bigNode) : base(bigNode)
        {
            this.SO = bigNode.ScriptableObject as ScriptableObject;
            this.increasePerNode = bigNode.IncreasePerNode;
        }
    }
}