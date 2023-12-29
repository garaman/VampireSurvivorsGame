using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region PlayerData
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }
    [Serializable]
    public class PlayerDataLoader : ILoader<int, PlayerData>
    {
        public List<PlayerData> stats = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDict()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
            foreach (PlayerData stat in stats)
            {
                dict.Add(stat.level, stat);
            }
            return dict;
        }
    }
    #endregion
}