using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.SkillTreeSystem;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class BigNodeWindow : NodeWindow
    {
        [SerializeField] private NumberInputField increaseInput = default;
        [SerializeField] private TMP_Dropdown dropdown = default;
        [Space]
        [SerializeField] private BigNodeStatsCollection statsCollection = default;

        private BigNode currentNode;

        private List<IBigNodeStat> stats;

        private void Start()
        {
            stats = statsCollection.GetStats();
            PopulateDropdown();
        }

        public void Setup(BigNode node)
        {
            coordGUI.text = node.Center.ToString();
            increaseInput.SetTextWithoutNotify(node.IncreasePerNode);

            currentNode = node;

            SetDropDownIndex(node);
            gameObject.SetActive(true);
        }

        private void SetDropDownIndex(BigNode node)
        {
            int index = 0;

            if (node.ScriptableObject != null)
            {
                for (int i = 0; i < dropdown.options.Count; i++)
                {
                    if (dropdown.options[i].text == node.ScriptableObject.Name)
                    {
                        index = i;
                    }
                }
            }
            dropdown.SetValueWithoutNotify(index);
        }

        public void IncreaseInput_Changed(int input)
        {
            currentNode.IncreasePerNode = input;
        }

        public void DropDownValue_Changed(int index)
        {
            currentNode.ScriptableObject = stats[index];
        }

        private void PopulateDropdown()
        {
            dropdown.ClearOptions();

            dropdown.AddOptions(stats.Select(x => x.Name).ToList());
        }
    }
}