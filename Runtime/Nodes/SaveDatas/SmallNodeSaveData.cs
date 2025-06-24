using System.Collections;
using System.Collections.Generic;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [System.Serializable]
    public class SmallNodeSaveData : SingleObjectSaveData<SmallNode, SmallNodeData, SmallNodeVisual>
    {
        public bool isSelected;
        public int multiplier = 1;

        public SmallNodeSaveData(SmallNode smallNode) : base(smallNode)
        {
            this.multiplier = smallNode.Multiplier;
            this.isSelected = smallNode.IsSelected;
        }
    }
}