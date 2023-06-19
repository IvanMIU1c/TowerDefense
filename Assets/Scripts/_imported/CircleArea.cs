using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// 2Д зона с радиусом.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        /// <summary>
        /// Радиус 2д зоны.
        /// </summary>
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;
        
        /// <summary>
        /// Возвращает рандомную позицию внутри круга.
        /// </summary>
        public Vector2 RandomInsideZone
        {
            get
            {
                return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
            }
        }

        public bool IsInside(Vector2 p)
        {
            return ((Vector2)transform.position - p).sqrMagnitude < m_Radius * m_Radius;
        }

        /// <summary>
        /// Здесь отрисовка через хендлы в редакторе, для удобства визуализации.
        /// Не забываем утащить апи редактора под дефайны как и using в начале файла.
        /// </summary>
#if UNITY_EDITOR
        private static readonly Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}