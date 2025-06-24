using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    public abstract class NodeWindow : MonoBehaviour
    {
        [SerializeField] protected TMP_Text coordGUI = default;

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}