using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using HexTecGames.GridBaseSystem;
using UnityEngine;
using UnityEngine.Events;

namespace HexTecGames.SkillTree
{
    public class SkillTreeController : MonoBehaviour
    {
        [SerializeField] private BaseGrid grid = default;
        [SerializeField] private GridLoader gridLoader = default;
        [SerializeField] private GridEventSystem gridEventSystem = default;
        [SerializeField] private ConfirmWindow confirmWindow = default;

        public int TotalPoints
        {
            get
            {
                return totalPoints;
            }
            set
            {
                if (totalPoints == value)
                {
                    return;
                }
                totalPoints = value;
                OnTotalPointsChanged?.Invoke(totalPoints);
            }
        }
        [SerializeField] private int totalPoints = default;

        public int AvailablePoints
        {
            get
            {
                return availablePoints;
            }
            private set
            {
                if (availablePoints == value)
                {
                    return;
                }
                availablePoints = value;
                OnAvailablePointsChanged?.Invoke(availablePoints);
            }
        }
        private int availablePoints;

        [Space]
        public UnityEvent<int> OnAvailablePointsChanged;
        public UnityEvent<int> OnTotalPointsChanged;

        private SmallNode startNode;
        private bool lastDragState;

        private const string SAVE_KEY = "SKILL_TREE";
        private const string ACTIVE_NODES = "ACTIVE_NODES";

        private HashSet<SmallNode> smallNodes = new HashSet<SmallNode>();

        private bool isInvalidTree;
        private SkillTreeSaveData saveData;

        private void Awake()
        {
            gridLoader.OnGridLoaded += GridLoader_OnGridLoaded;
        }
        private void Start()
        {
            saveData = SaveSystem.LoadJSON<SkillTreeSaveData>(SAVE_KEY);
            if (saveData != null)
            {
                AvailablePoints = TotalPoints - saveData.activeNodes.Count;
            }
            else AvailablePoints = TotalPoints;
        }


        private void OnDestroy()
        {
            gridLoader.OnGridLoaded -= GridLoader_OnGridLoaded;
        }

        private void GridLoader_OnGridLoaded(BaseGrid obj)
        {
            gridEventSystem.OnMouseClicked += GridEventSystem_OnMouseClicked;
            gridEventSystem.OnDraggingMoved += GridEventSystem_OnDraggingMoved;
            smallNodes = new HashSet<SmallNode>(grid.GetAllGridObjects<SmallNode>(0));
            FindStartNode();
            LoadSkillTreeData();
            CalculateNodes();
            SetupBigNodes();
        }

        public void LoadSkillTreeData()
        {
            if (saveData == null)
            {
                return;
            }
            foreach (var coord in saveData.activeNodes)
            {
                var node = grid.GetGridObject<SmallNode>(0, coord);
                node.IsSelected = true;
            }
            UpdateSmallNodeColors();
        }

        public void SaveSkillTreeData()
        {
            SkillTreeSaveData data = new SkillTreeSaveData();
            foreach (var node in smallNodes)
            {
                if (node.IsValid)
                {
                    data.activeNodes.Add(node.Center);
                }
            }

            SaveSystem.SaveJSON(data, SAVE_KEY);
        }

        //private void SetupSmallNodes()
        //{
        //    foreach (var node in smallNodes)
        //    {
        //        node.Setup();
        //    }
        //}
        private void SetupBigNodes()
        {
            var results = grid.GetAllGridObjects<BigNode>(0);
            foreach (var result in results)
            {
                result.Setup();
            }
        }
        private void FindStartNode()
        {
            var results = grid.GetAllGridObjects<SmallNode>(0);
            foreach (var result in results)
            {
                if (result.IsSelected)
                {
                    result.IsStartNode = true;
                    startNode = result;
                    SetNeighboursAsAllowed(result, true);
                    return;
                }
            }
        }

        // When a node gets set active/inactive recalculate changeablity for every node
        // All the nodes that connect to the start node or to a node that connects to the start node are valid
        // All other nodes are deactivated

        private void CalculateNodes()
        {
            List<SmallNode> connectedNodes = new List<SmallNode>() { startNode };
            GetConnectedNeighbours(startNode.Center, ref connectedNodes);
            UpdateSkillPointsCount();
            foreach (var node in smallNodes)
            {
                node.IsChangeable = false;
            }

            foreach (var node in smallNodes)
            {
                if (connectedNodes.Contains(node))
                {
                    node.IsDeactivated = false;
                    if (AvailablePoints > 0)
                    {
                        SetNeighboursAsAllowed(node, true);
                    }
                    connectedNodes.Remove(node);
                }
                else
                {
                    node.IsDeactivated = true;
                }
            }

            UpdateSmallNodeColors();
        }

        private void UpdateSkillPointsCount()
        {
            int spentPoints = 0;
            foreach (var node in smallNodes)
            {
                if (node.IsSelected)
                {
                    spentPoints++;
                }
            }
            AvailablePoints = TotalPoints - spentPoints;
        }

        private void UpdateSmallNodeColors()
        {
            foreach (var node in smallNodes)
            {
                node.UpdateColor();
            }
        }

        private void GetConnectedNeighbours(Coord center, ref List<SmallNode> connectedNodes)
        {
            List<SmallNode> neighbours = grid.GetNeighbourGridObjects<SmallNode>(0, center);
            foreach (var neighbour in neighbours)
            {
                if (neighbour.IsSelected && !connectedNodes.Contains(neighbour))
                {
                    connectedNodes.Add(neighbour);
                    GetConnectedNeighbours(neighbour.Center, ref connectedNodes);
                }
            }
        }

        private void GridEventSystem_OnDraggingMoved(Coord coord, int mouseBtn)
        {
            HandleMouseInput(coord, true);
        }
        private void GridEventSystem_OnMouseClicked(Coord coord, int mouseBtn)
        {
            HandleMouseInput(coord, false);
        }

        private void HandleMouseInput(Coord coord, bool isDrag = false)
        {
            GridObject gridObject = grid.GetGridObject(0, coord);

            if (gridObject == null)
            {
                return;
            }
            if (gridObject is not SmallNode node)
            {
                return;
            }

            if (node.IsStartNode && node.IsSelected)
            {
                return;
            }
            if (!node.IsSelected && AvailablePoints <= 0)
            {
                return;
            }

            if (isDrag && node.IsSelected == lastDragState)
            {
                return;
            }

            if (node.IsChangeable || node.IsSelected)
            {
                node.IsSelected = !node.IsSelected;
                if (!isDrag)
                {
                    lastDragState = node.IsSelected;
                }
                CalculateNodes();
            }
        }

        private void SetNeighboursAsAllowed(SmallNode node, bool changeable)
        {
            var results = grid.GetNeighbourGridObjects<SmallNode>(0, node.Center);

            foreach (var result in results)
            {
                result.IsChangeable = changeable;
            }
        }

        public void ResetTree()
        {
            foreach (var result in smallNodes)
            {
                if (!result.IsStartNode)
                {
                    result.IsSelected = false;
                }
            }
            CalculateNodes();
        }

        private void OnSaveClicked()
        {
            SaveSkillTreeData();
        }
        private void OnDiscardClicked()
        {
            ReturnToLastScene();
        }
        private void ReturnToLastScene()
        {
            SceneUtility.ReloadCurrentScene();
        }
        public void ReturnButtonClicked()
        {
            if (smallNodes.Any(x => x.IsDeactivated && x.IsSelected))
            {
                confirmWindow.Setup($"Invalid!{Environment.NewLine}Discard changes?", null, null, "Discard", "Cancel");
                isInvalidTree = true;
            }
            else
            {
                confirmWindow.Setup("Save changes?", OnSaveClicked, null, "Save", "Discard");
                isInvalidTree = false;
            }
        }
    }
}