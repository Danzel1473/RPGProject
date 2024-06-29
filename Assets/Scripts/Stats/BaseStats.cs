using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Stats{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifier = false;

        public event Action onLevelUp;
        Experience experience;

        int currentLV = 0;

        private void Awake() {
            experience = GetComponent<Experience>();
        }

        private void Start() {
            currentLV = CalculateLevel();

            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();

            if(newLevel > currentLV) {
                currentLV = newLevel;
                LevelUpEffect();
                print("LevelupEffect Done");
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        { 
            Instantiate(levelUpParticleEffect, transform);
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentEXP = experience.GetPoint();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for(int level = 1; level <= penultimateLevel; level++) {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentEXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (GetPercentageModifier(stat) / 100 + 1);
        }

        private float GetPercentageModifier(Stat stat)
        {
            if(!shouldUseModifier) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if(!shouldUseModifier) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel() {
            return currentLV;
        }
    }

}
