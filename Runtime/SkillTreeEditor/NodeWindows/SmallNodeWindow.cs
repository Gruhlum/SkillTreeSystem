using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class SmallNodeWindow : NodeWindow
    {
        [SerializeField] private NumberInputField multiplierInput = default;

        private SmallNode currentNode;


        public void Setup(SmallNode node)
        {
            this.currentNode = node;
            coordGUI.text = node.Center.ToString();
            multiplierInput.SetTextWithoutNotify(node.Multiplier);
            gameObject.SetActive(true);
            multiplierInput.Select();
        }

        public void InputField_Changed(int input)
        {
            currentNode.Multiplier = input;
        }
    }
}