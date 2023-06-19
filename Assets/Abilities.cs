using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class Abilities : MonoSingleton<Abilities>
    {
        [SerializeField] private UpgradeAsset upgradeSlow;
        [SerializeField] private UpgradeAsset upgradeFire;

        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_cost = 10;
            [SerializeField] private int m_damage = 2;
            [SerializeField] private Color m_TargetingColor;





            public void Use()
            {
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    TDPlayer.Instance.ChangeGold(-m_cost);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
            }
        }
        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_Cooldown = 15;

            [SerializeField] private float m_duration = 5f;
            public void Use()
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();
                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());
                
                
                IEnumerator TimeAbilityButton()
                {
                    Instance.TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.TimeButton.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());
            }
        }
        [SerializeField] private Image m_TargetInCircle;
        [SerializeField] private Button TimeButton;
        [SerializeField] private Button FireButton;
        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();
        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();


        private void GoldStatusCheck(int gold)
        {
            if (gold >= 10 != FireButton.interactable)
            {
                FireButton.interactable = !FireButton.interactable;
            }
        }


        private void Start()
        {
            TDPlayer.Instance.GoldUpdateSubscribe(GoldStatusCheck);



            if (Upgrades.GetUpgradeLevel(upgradeSlow) < 1)
            {
                TimeButton.interactable = false;
            }
            if (Upgrades.GetUpgradeLevel(upgradeFire) < 1)
            {
                FireButton.interactable = false;
            }
        }

    }
}