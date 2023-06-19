using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Точка патрулирования.
    /// </summary>
    public class AIPointPatrol : MonoBehaviour
    {
        /// <summary>
        /// радиус зоны патрулирования.
        /// </summary>
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;

            Gizmos.DrawSphere(transform.position, m_Radius);
        }
    }
}