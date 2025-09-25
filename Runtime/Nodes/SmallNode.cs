using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{

    [System.Serializable]
    public class SmallNode : SingleObject<SmallNode, SmallNodeData, SmallNodeVisual>
    {
        public bool IsValid
        {
            get
            {
                return IsSelected && !IsDeactivated;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected == value)
                {
                    return;
                }
                isSelected = value;
                OnIsValidChanged?.Invoke(this, IsValid);
            }
        }
        private bool isSelected;

        public bool IsDeactivated
        {
            get
            {
                return isDeactivated;
            }
            set
            {
                if (isDeactivated == value)
                {
                    return;
                }
                isDeactivated = value;
                OnIsValidChanged?.Invoke(this, IsValid);
            }
        }
        private bool isDeactivated;

        public bool IsStartNode
        {
            get
            {
                return isStartNode;
            }
            set
            {
                isStartNode = value;
            }
        }
        private bool isStartNode;

        public int Multiplier
        {
            get
            {
                return multiplier;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                multiplier = value;
                OnMultiplierChanged?.Invoke(this, multiplier);
            }
        }
        private int multiplier = 1;


        public bool IsChangeable
        {
            get
            {
                return isChangeable;
            }
            set
            {
                isChangeable = value;
            }
        }
        private bool isChangeable;

        public event Action<SmallNode, bool> OnIsValidChanged;
        public event Action<SmallNode, int> OnMultiplierChanged;
        //public event Action<SmallNode> OnSetupFinished;

        public SmallNode(SmallNodeData data, Coord center, int rotation = 0, GridObjectSaveData saveData = null) : base(data, center, rotation, saveData)
        {
        }

        //public void Setup()
        //{
        //    OnSetupFinished?.Invoke(this);
        //}
        public void UpdateColor()
        {
            // Selected & Not Deactivated = green
            // Selected & Deactivated = green/gray mix
            // Not Selected & IsChangable = light green
            // Nothing = black

            if (IsValid)
            {
                Color = Data.SelectedColor;
            }
            else if (IsSelected && IsDeactivated)
            {
                Color = Color.Lerp(Data.SelectedColor, Data.Color, 0.5f);
            }
            else if (IsChangeable)
            {
                Color = Data.ClickableColor;
            }
            else Color = Data.Color;
        }
        public override void LoadSaveData(GridObjectSaveData saveData)
        {
            base.LoadSaveData(saveData);
            if (saveData is SmallNodeSaveData smallNodeSaveData)
            {
                IsSelected = smallNodeSaveData.isSelected;
                Multiplier = smallNodeSaveData.multiplier;
            }
        }
        public override GridObjectSaveData GetSaveData()
        {
            return new SmallNodeSaveData(this);
        }
    }
}