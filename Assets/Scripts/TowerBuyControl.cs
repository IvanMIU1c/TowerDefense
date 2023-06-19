using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_ta;
        public void SetTowerAsset(TowerAsset asset){ m_ta = asset; }
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;
        [SerializeField] private Transform buildSite;
        public void SetBuildSite(Transform value)
        {
            buildSite = value;
        }
        [SerializeField] private UpgradeAsset rangeAsset;
        
        /*
        private void Awake()
        {
            if (rangeAsset != null)
            {
                var level = Upgrades.GetUpgradeLevel(rangeAsset);
                if(level >= 1)
                {
                }
                else
                {
                    gameObject.transform.position = new Vector3(3000,10,10);
                }
            }
        }*/

        private void Start()
        {
            TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);
            m_text.text = m_ta.goldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_ta.towerGui;
        }
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_ta.goldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_ta, buildSite);
            BuildSite.HideControls();
        }
    }
}