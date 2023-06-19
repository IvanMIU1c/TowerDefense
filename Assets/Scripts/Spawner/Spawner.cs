using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{

    public abstract class Spawner : MonoBehaviour
    {
        protected abstract GameObject GenerateSpawnedEntity();


        /// <summary>
        /// Зона спавна.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// Режим спавна.
        /// </summary>
        public enum SpawnMode
        {
            /// <summary>
            /// На методе Start()
            /// </summary>
            Start,

            /// <summary>
            /// Периодически используя время m_RespawnTime
            /// </summary>
            Loop
        }

        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// Кол-во объектов которые будут разом заспавнены.
        /// </summary>
        [SerializeField] private int m_NumSpawns;

        /// <summary>
        /// Время респавна. Здесь важно заметить что респавн таймер должен быть больше времени жизни объектов.
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        [SerializeField] private float m_StartDelay;

        private float m_Timer;

        private void Start()
        {
            if(m_SpawnMode == SpawnMode.Start)
            {
                m_Timer = m_StartDelay;
                //SpawnEntities();
            }
            if(m_SpawnMode == SpawnMode.Loop)
            {
                m_Timer = m_StartDelay;
            }
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if(m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for(int i = 0; i < m_NumSpawns; i++)
            {
                var e = GenerateSpawnedEntity();
                e.transform.position = m_Area.RandomInsideZone;

            }
        }
    }
}