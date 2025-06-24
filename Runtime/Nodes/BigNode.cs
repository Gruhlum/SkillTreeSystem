using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.EaseFunctions;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [System.Serializable]
    public class BigNode : MultiObject<BigNode, BigNodeData, BigNodeVisual>
    {
        private ScriptableObject scriptableObject;
        private int increasePerNode = 2;
        private int currentIncrease;

        public int IncreasePerNode
        {
            get
            {
                return this.increasePerNode;
            }
            set
            {
                this.increasePerNode = value;
                OnUpdateRequired?.Invoke(this);
            }
        }
        public int CurrentIncrease
        {
            get
            {
                return this.currentIncrease;
            }
            private set
            {
                this.currentIncrease = value;
                OnCurrentIncreaseChanged?.Invoke(this.currentIncrease);
            }
        }
        public ScriptableObject ScriptableObject
        {
            get
            {
                return this.scriptableObject;
            }
            set
            {
                this.scriptableObject = value;
                OnUpdateRequired?.Invoke(this);
            }
        }

        private HashSet<SmallNode> activeNeighbourNodes;

        private int totalNeighbourNodes;

        public event Action<int> OnCurrentIncreaseChanged;
        public event Action<BigNode> OnUpdateRequired;

        public BigNode(BigNodeData data, BaseGrid grid, Coord center, int rotation = 0, GridObjectSaveData saveData = null)
            : base(data, grid, center, rotation, saveData)
        {
        }

        public void Setup()
        {
            var results = Grid.GetGridObjects<SmallNode>(Data.Layer, GetNeighbourCoords());
            totalNeighbourNodes = results.Count;
            activeNeighbourNodes = new HashSet<SmallNode>();
            foreach (var result in results)
            {
                if (result.IsValid)
                {
                    activeNeighbourNodes.Add(result);
                }
                result.OnIsValidChanged += Result_OnIsValidChanged;
            }
            UpdateCurrentIncrease();
        }

        private void UpdateColor()
        {
            Color = Color.Lerp(Data.Color, Data.fullColor, EaseFunction.EaseOutQuad(activeNeighbourNodes.Count / (float)totalNeighbourNodes));
        }

        private void Result_OnIsValidChanged(SmallNode smallNode, bool isValid)
        {
            if (isValid)
            {
                if (!activeNeighbourNodes.Contains(smallNode))
                {
                    activeNeighbourNodes.Add(smallNode);
                }
            }
            else
            {
                activeNeighbourNodes.Remove(smallNode);
            }

            UpdateCurrentIncrease();
        }

        private void UpdateCurrentIncrease()
        {
            int totalIncrease = 0;
            foreach (var node in activeNeighbourNodes)
            {
                totalIncrease += IncreasePerNode * node.Multiplier;
            }

            CurrentIncrease = totalIncrease;
            UpdateColor();
            OnUpdateRequired?.Invoke(this);
        }

        public override void LoadSaveData(GridObjectSaveData saveData)
        {
            if (saveData is BigNodeSaveData bigNodeSaveData)
            {
                ScriptableObject = bigNodeSaveData.SO;
                IncreasePerNode = bigNodeSaveData.increasePerNode;
            }
            base.LoadSaveData(saveData);
        }
        public override GridObjectSaveData GetSaveData()
        {
            return new BigNodeSaveData(this);
        }
    }
}