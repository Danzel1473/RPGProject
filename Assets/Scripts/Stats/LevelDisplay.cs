using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats stat;

        private void Awake() {
            stat = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update() {
            GetComponent<Text>().text = string.Format("{0:0}", stat.CalculateLevel());
        }
    }
}