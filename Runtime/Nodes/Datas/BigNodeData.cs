using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [CreateAssetMenu(menuName = "HexTecGames/Sandbox/BigNodeData")]
    public class BigNodeData : MultiObjectData<BigNode, BigNodeData, BigNodeVisual>
    {
        public Color fullColor;

        public override GridObject CreateGridObject(BaseGrid grid, Coord center, int rotation, GridObjectSaveData saveData = null)
        {
            return new BigNode(this, grid, center, rotation, saveData);
        }
    }
}