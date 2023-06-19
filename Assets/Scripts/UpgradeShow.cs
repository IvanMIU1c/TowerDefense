using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class UpgradeShow : MonoBehaviour
    {

        [SerializeField] private int money;
        [SerializeField] private Text moneyText;
        [SerializeField] private BuyUpgrage[] sales;

        private void Start()
        {

            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();
        }

        public void UpdateMoney()
        {
            money = MapComplettion.Instance.TotalScore;
            money -= Upgrades.GetTotalCost();
            moneyText.text = money.ToString();
            foreach (var slot in sales)
            {
                slot.CheckCost(money);
            }
        }
    }
}