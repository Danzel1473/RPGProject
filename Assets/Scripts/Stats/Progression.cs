using System;
using System.Collections.Generic;
using RPG.Stats;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RPG.Stats{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
    public class Progression : ScriptableObject {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null; //효율적인 검색을 위한 Dictionary 선언


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLokkup();
            float[] levels =  lookupTable[characterClass][stat];

            if(levels.Length < level)
            {
                return 0;
            }

            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLokkup();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLokkup()
        {
            if (lookupTable != null) return; //이미 lookupTable이 있다면 얼리리턴

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [Serializable]
        class ProgressionCharacterClass {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;


        }
        [Serializable]
        class ProgressionStat {
            public Stat stat;
            public float[] levels;

        }
    }
}
