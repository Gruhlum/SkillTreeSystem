using System.Collections;
using System.Collections.Generic;
using HexTecGames.GridBaseSystem;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class SmallNodeVisual : SingleObjectVisual<SmallNode, SmallNodeData, SmallNodeVisual>
    {
        [SerializeField] private Canvas canvas = default;
        [SerializeField] private TMP_Text textGUI = default;

        public override void Setup(GridObject gridObj, BaseGrid grid)
        {
            base.Setup(gridObj, grid);
            if (gridObj != null)
            {
                UpdateText(gridObj as SmallNode);
            }
        }

        protected override void AddEvents(SmallNode node)
        {
            base.AddEvents(node);
            node.OnMultiplierChanged += Node_OnMultiplierChanged;

        }
        protected override void RemoveEvents(SmallNode node)
        {
            base.RemoveEvents(node);
            node.OnMultiplierChanged -= Node_OnMultiplierChanged;
        }
        private void Node_OnMultiplierChanged(SmallNode node, int multi)
        {
            UpdateText(node);
        }

        private void UpdateText(SmallNode node)
        {
            if (node.Multiplier > 1)
            {
                canvas.gameObject.SetActive(true);
                textGUI.text = $"x{node.Multiplier}";
            }
            else canvas.gameObject.SetActive(false);
        }
    }
}