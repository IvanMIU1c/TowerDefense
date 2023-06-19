using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    public class EnemySpawner : Spawner
    {
        [SerializeField] private Path m_path;
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private EnemyAsset[] m_EnemyAssets;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EnemyPrefab);
            e.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);
            e.GetComponent<TDPatrolController>().SetPath(m_path);
            return e.gameObject;
        }

    }
}