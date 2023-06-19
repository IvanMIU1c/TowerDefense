using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 5f;
        private Turret[] turrets;
        private Destructible target = null;


        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;
            turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.turretProperties);
            }
            GetComponentInChildren<BuildSite>().SetBuildableTowers(asset.m_upgradesTo);
        }

        [SerializeField] private UpgradeAsset rangeAsset;
        private void Awake()
        {
            var level = Upgrades.GetUpgradeLevel(rangeAsset);
            m_Radius += level;
        }
        private void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position; 
                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}