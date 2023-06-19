using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class ClickProtection : MonoSingleton<ClickProtection>, IPointerClickHandler
    {
        private Image blocker;
        private void Start()
        {
            blocker = GetComponent<Image>();
        }

        private Action<Vector2> m_OnclickAction;
        public void Activate (Action<Vector2> mouseAction)
        {
            blocker.enabled = true;
            m_OnclickAction = mouseAction;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            blocker.enabled = false;
            m_OnclickAction(eventData.pressPosition);
            m_OnclickAction = null;
        }
    }
}