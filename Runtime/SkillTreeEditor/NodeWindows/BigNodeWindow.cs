using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class BigNodeWindow : NodeWindow
    {
        [SerializeField] private NumberInputField increaseInput = default;
        [SerializeField] private TMP_Dropdown dropdown = default;
        [Space]
        [SerializeField] private List<ScriptableObject> scriptableObjects = default;

        private BigNode currentNode;

        private void Start()
        {
            PopulateDropdown();
        }

        public void Setup(BigNode node)
        {
            coordGUI.text = node.Center.ToString();
            increaseInput.SetTextWithoutNotify(node.IncreasePerNode);

            currentNode = node;
            gameObject.SetActive(true);
        }

        public void IncreaseInput_Changed(int input)
        {
            currentNode.IncreasePerNode = input;
        }

        public void DropDownValue_Changed(int index)
        {
            currentNode.ScriptableObject = scriptableObjects[index];
        }

        private void PopulateDropdown()
        {
            dropdown.ClearOptions();

            dropdown.AddOptions(scriptableObjects.Select(x => x.name).ToList());
        }
    }
}