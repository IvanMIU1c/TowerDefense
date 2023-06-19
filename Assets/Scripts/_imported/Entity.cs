using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Базовый класс всех интерактивных игровых объектов на сцене.
    /// Спавнер объектов будет работать именно с таким скриптом.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Название объекта для пользователя. Например для вывески кто вас убил.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname { get => m_Nickname; set => m_Nickname = value; }
    }
}