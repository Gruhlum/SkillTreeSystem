using System.Collections;
using System.Collections.Generic;
using HexTecGames.GridBaseSystem;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class BigNodeVisual : MultiObjectVisual<BigNode, BigNodeData, BigNodeVisual>
    {
        [SerializeField] private TMP_Text nameGUI = default;
        [SerializeField] private TMP_Text valueGUI = default;
        [Space]
        [SerializeField] private Color inactiveTextColor = Color.black;
        [SerializeField] private Color normalTextColor = Color.black;

        protected override void OnSetup(GridObject tileObject, BaseGrid grid)
        {
            base.OnSetup(tileObject, grid);
            if (tileObject is BigNode bigNode)
            {
                UpdateVisual(bigNode);
            }
        }

        private void UpdateVisual(BigNode bigNode)
        {
            if (bigNode.ScriptableObject != null)
            {
                nameGUI.text = bigNode.ScriptableObject.Name;
            }
            else nameGUI.text = string.Empty;

            UpdateIncreaseText(bigNode);
        }

        protected override void AddEvents(BigNode node)
        {
            base.AddEvents(node);
            node.OnColorChanged += Node_OnColorChanged;
            node.OnUpdateRequired += Node_OnUpdateRequired;
        }

        private void Node_OnUpdateRequired(BigNode node)
        {
            UpdateVisual(node);
        }

        protected override void RemoveEvents(BigNode node)
        {
            base.RemoveEvents(node);
            node.OnColorChanged -= Node_OnColorChanged;
            node.OnUpdateRequired -= Node_OnUpdateRequired;
        }
        private void Node_OnColorChanged(BigNode node, Color color)
        {
            SetColor(color);
        }

        private void UpdateIncreaseText(BigNode bigNode)
        {
            string suffix;
            if (bigNode.ScriptableObject != null && bigNode.ScriptableObject.IsPercent())
            {
                suffix = "%";
            }
            else suffix = string.Empty;

            if (bigNode.CurrentIncrease == 0)
            {
                valueGUI.color = inactiveTextColor;
                valueGUI.text = $"+{bigNode.IncreasePerNode}{suffix}";
            }
            else
            {
                valueGUI.text = $"+{bigNode.CurrentIncrease}{suffix}";
                valueGUI.color = normalTextColor;
            } 
        }
    }
}