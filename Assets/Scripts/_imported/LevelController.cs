using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Интерфейс условия прохождения уровня.
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// True если условие выполнено.
        /// </summary>
        bool IsCompleted { get; }
    }

    /// <summary>
    /// Контроллер прохождения уровня.
    /// Определяет логику завершения уровня посредством проверки условий.
    /// Мы накидываем скрипты условий внутрь левел контроллера,
    /// которые потом автоматически подцепятся.
    /// 
    /// Если условий 0 то уровень будет играть вечно.
    /// Контроллер уровня является синглтоном, но включать опцию m_DoNotDestroyOnLoad нельзя.
    /// т.к. он должен удалится при смене уровня.
    /// </summary>
    public class LevelController : MonoSingleton<LevelController>
    {
        /// <summary>
        /// Время прохождения в секундах за которое будут начисляться очки.
        /// </summary>
        [SerializeField] protected float m_ReferenceTime;
        public float ReferenceTime => m_ReferenceTime;

        /// <summary>
        /// Событие которое будет вызвано когда уровень будет выполнен. Вызывается один раз.
        /// </summary>
        [SerializeField] protected UnityEvent m_EventLevelCompleted;

        /// <summary>
        /// Массив условий для успешного прохождения уровня.
        /// </summary>
        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted; // флаг отсылки события прохождения один раз.

        private float m_LevelTime; // текущее время прохождения уровня.
        public float LevelTime => m_LevelTime;

        #region Unity events

        protected void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }


        }

        #endregion

        /// <summary>
        /// Метод проверки условий прохождения.
        /// </summary>
        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;
            }

            if(numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                m_EventLevelCompleted?.Invoke();

                // Notify level sequence Unit3 code
                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }
    }
}