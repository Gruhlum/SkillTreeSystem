using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public class PointsWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text pointsTextGUI = default;

        public void SetText(string text)
        {
            pointsTextGUI.text = text;
        }
        public void SetText(int value)
        {
            SetText(value.ToString());
        }
    }
}