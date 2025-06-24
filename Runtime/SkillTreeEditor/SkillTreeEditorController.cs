using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.GridBaseSystem;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class SkillTreeEditorController : MonoBehaviour
    {
        [SerializeField] private GridPlacementController placementController = default;
        [SerializeField] private BaseGrid grid = default;
        [SerializeField] private GridEventSystem gridEventSystem = default;
        [Space]
        [SerializeField] private SmallNodeWindow smallNodeWindow = default;
        [SerializeField] private BigNodeWindow bigNodeWindow = default;

        private NodeWindow activeWindow;

        private void OnEnable()
        {
            gridEventSystem.OnMouseClicked += GridEventSystem_OnMouseClicked;
            placementController.OnSelectedObjectChanged += PlacementController_OnSelectedObjectChanged;
        }

        private void OnDisable()
        {
            gridEventSystem.OnMouseClicked -= GridEventSystem_OnMouseClicked;
            placementController.OnSelectedObjectChanged -= PlacementController_OnSelectedObjectChanged;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                DeactivateCurrentWindow();
                
            }
        }
        private void PlacementController_OnSelectedObjectChanged(PlacementData obj)
        {
            if (obj != null)
            {
                DeactivateCurrentWindow();
            }
        }
        private void DeactivateCurrentWindow()
        {
            if (activeWindow != null)
            {
                activeWindow.Deactivate();
                activeWindow = null;
            }
        }

        private void GridEventSystem_OnMouseClicked(Coord coord, int mouseBtn)
        {
            var result = grid.GetGridObject(coord);
            if (result is SmallNode smallNode)
            {
                if (activeWindow != smallNodeWindow)
                {
                    DeactivateCurrentWindow();
                }
                smallNodeWindow.Setup(smallNode);
                activeWindow = smallNodeWindow;
            }
            else if (result is BigNode bigNode)
            {
                if (activeWindow != bigNodeWindow)
                {
                    DeactivateCurrentWindow();
                }
                bigNodeWindow.Setup(bigNode);
                activeWindow = bigNodeWindow;
            }
        }
    }
}