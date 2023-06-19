using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] buildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers)
        {
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                buildableTowers = towers;
            }
        }
        public static event Action<BuildSite> OnclickEvent;
        public static void HideControls()
        {
            OnclickEvent(null);
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnclickEvent(this);
        }
    }
}