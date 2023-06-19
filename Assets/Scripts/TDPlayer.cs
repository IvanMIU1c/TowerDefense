using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        { get 
            { 
                return Player.Instance as TDPlayer; 
            } 
        }
        private event Action<int> OnGoldUpdate;
        public event Action<int> OnLifeUpdate;
        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        public void LifeUpdateSubscribe(Action<int> act)
        {
            act(Instance.NumLives);
            OnLifeUpdate += act;

        }

        [SerializeField] private int m_gold = 0;

        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }
        public void ReduceLife(int change)
        {
            takeDamage(change);
            OnLifeUpdate(NumLives);
        }

        [SerializeField] private Tower m_towerPrefab;

        
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(towerAsset.towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            Destroy(buildSite.gameObject);
        }

        [SerializeField] private UpgradeAsset healthUpdate;
        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(healthUpdate);
            takeDamage(-level * 5);
        }
    }
}