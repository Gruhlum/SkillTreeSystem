using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.SkillTree
{
    public class SmallNodeWindow : NodeWindow
    {
        [SerializeField] private NumberInputField multiplierInput = default;
        [SerializeField] private Toggle toggle = default;

        private SmallNode currentNode;


        public void Setup(SmallNode node)
        {
            this.currentNode = node;
            toggle.SetIsOnWithoutNotify(node.IsSelected);

            coordGUI.text = node.Center.ToString();
            multiplierInput.SetTextWithoutNotify(node.Multiplier);
            gameObject.SetActive(true);
            multiplierInput.Select();
        }

        public void InputField_Changed(int input)
        {
            currentNode.Multiplier = input;
        }

        public void Toggle_Changed(bool active)
        {
            currentNode.IsSelected = active;
        }
    }
}