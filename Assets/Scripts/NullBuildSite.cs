using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class NullBuildSite : BuildSite
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}