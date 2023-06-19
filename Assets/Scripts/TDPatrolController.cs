using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_path;
        private int pathIndex;
        [SerializeField] private UnityEvent OnEndPath;
        internal void SetPath(Path newpath)
        {
            m_path = newpath;
            pathIndex = 0;
            SetPatrolBehaviour(m_path[pathIndex]);

        }
        protected override void GetNewPoint()
        {
            pathIndex += 1;
            if (m_path.LEngth > pathIndex)
                SetPatrolBehaviour(m_path[pathIndex]);
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
