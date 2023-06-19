using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrage : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        private int costNumber = 0;
        [SerializeField] private Text level, costText;
        [SerializeField] private Button BuyButton;

        public void Initialize()
        {
            upgradeIcon.sprite = asset.sprite;
            var savedlevel = Upgrades.GetUpgradeLevel(asset);

            if (savedlevel >= asset.costByLevel.Length)
            {
                level.text += $"Level: {savedlevel} (Max)";
                BuyButton.interactable = false;
                BuyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                BuyButton.transform.Find("Text").gameObject.SetActive(false);
                costText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                level.text = $"Level: {savedlevel + 1}";
                costNumber = asset.costByLevel[savedlevel];
                costText.text = costNumber.ToString();
            }
        }


        public void CheckCost(int money)
        {
            BuyButton.interactable = money >= costNumber;
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
    }
}