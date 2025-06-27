using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.SkillTreeSystem
{
    public interface IBigNodeStat
    {
        public string Name
        {
            get;
        }

        public bool IsPercent();

    }
}