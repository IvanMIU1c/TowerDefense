using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave: MonoBehaviour
    {
        public static Action<float> OnWavePrepare;

        [SerializeField] private PathGroup[] groups;
        [SerializeField] private float m_prepareTime = 10f;
        public float GetRemainingTime() { return m_prepareTime - Time.time; }

        [Serializable] 
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }
        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }



        private void Awake()
        {
            enabled = false;
        }
        private event Action OnWaveReady;
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(m_prepareTime);
            m_prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies; 
        }
        private void Update()
        {
            if (Time.time >= m_prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }


        [SerializeField] private EnemyWave next;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if(next) next.Prepare(spawnEnemies);
            return next;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }
            }
        }
    }
}