using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [CreateAssetMenu(menuName = "HexTecGames/Sandbox/SmallNodeData")]
    public class SmallNodeData : SingleObjectData<SmallNode, SmallNodeData, SmallNodeVisual>, IGridObjectCreator
    {
        public Color SelectedColor
        {
            get
            {
                return this.selectedColor;
            }

            set
            {
                this.selectedColor = value;
            }
        }
        [SerializeField] private Color selectedColor = Color.green;

        public Color ClickableColor
        {
            get
            {
                return clickableColor;
            }
            set
            {
                clickableColor = value;
            }
        }
        [SerializeField] private Color clickableColor = Color.cyan;

        public GridObject CreateGridObject(BaseGrid grid, Coord center, int rotation, GridObjectSaveData saveData = null)
        {
            var result = new SmallNode(this, center, rotation, saveData);
            result.AddToGrid(grid);
            return result;
        }
    }
}