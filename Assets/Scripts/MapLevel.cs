using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEngine.UI;
using System;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resultImage;

        public bool IsComplete { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

        public int Initialise()
        {
            var score = MapComplettion.Instance.GetEpisodeScore(m_episode);
            resultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                resultImage[i].color = Color.white;
            }
            return score;
        }

        //[SerializeField] private Text text;
        public void LoadLevel()
        {
            if (m_episode)
            {
                LevelSequenceController.Instance.StartEpisode(m_episode);
            }
            else
            {
                print("ошибка - нет эпизода");
            }
        }
    }
}