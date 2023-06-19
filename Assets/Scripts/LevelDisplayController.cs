using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;
        [SerializeField] private BranchLevel[] branchLevels;

        private void Start()
        {
            var score = 1;
            var drawLevel = 0;
            while (score !=0 && drawLevel < levels.Length)
            {
                score = levels[drawLevel].Initialise();
                drawLevel++;
            }
            for (int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
            for (int i = 0; i< branchLevels.Length; i++)
            {
                branchLevels[i].TryActivate();
            }
        }
    }
}