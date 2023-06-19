using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        private List<TowerBuyControl> m_ActiveControl;
        private RectTransform m_rectTransform;

        #region События Unity
        private void Awake()
        {
            m_rectTransform = GetComponent<RectTransform>();
            BuildSite.OnclickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }


        private void OnDestroy()
        {
            BuildSite.OnclickEvent -= MoveToBuildSite;
        }
        #endregion 

        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_rectTransform.anchoredPosition = position;
                m_ActiveControl = new List<TowerBuyControl>();
                foreach (var asset in buildSite.buildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }
                if (m_ActiveControl.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / m_ActiveControl.Count;
                    for (int i = 0; i < m_ActiveControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 100);
                        m_ActiveControl[i].transform.position += offset;
                    }
                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }
                }
            }
            else
            {
                if (m_ActiveControl != null)
                {
                    foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                    m_ActiveControl.Clear();
                }
                gameObject.SetActive(false);
            }
        }
    }
}