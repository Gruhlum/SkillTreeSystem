using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.SkillTree
{
    [System.Serializable]
    public class SkillTreeSaveData
    {
        public List<Coord> activeNodes = new List<Coord>();
    }
}