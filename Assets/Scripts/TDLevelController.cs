using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;
        private new void Start()
        {
            base.Start();

            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);
            };

            m_ReferenceTime += Time.time;
            m_EventLevelCompleted.AddListener(() =>
           {
               StopLevelActivity();
               if (m_ReferenceTime <= Time.time)
               {
                   levelScore -= 1;
               }
               MapComplettion.SaveEpisodeResult(levelScore);
           });
            void LifeScoreChange(int _)
            {
                levelScore -= 1;
                TDPlayer.Instance.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.Instance.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            print(levelScore);
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            void DisableAll<T>() where T: MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
        }
    }
}