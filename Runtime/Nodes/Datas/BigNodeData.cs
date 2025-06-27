using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [CreateAssetMenu(menuName = "HexTecGames/Sandbox/BigNodeData")]
    public class BigNodeData : MultiObjectData<BigNode, BigNodeData, BigNodeVisual>, IGridObjectCreator
    {
        public Color fullColor;

        public GridObject CreateGridObject(BaseGrid grid, Coord center, int rotation, GridObjectSaveData saveData = null)
        {
            var result = new BigNode(this, center, rotation, saveData);
            result.AddToGrid(grid);
            return result;
        }
    }
}