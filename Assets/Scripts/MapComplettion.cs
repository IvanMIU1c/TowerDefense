using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapComplettion : MonoSingleton<MapComplettion>
    {
        public const string filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log($"Episode complete with score {levelScore}");
            }
        }

        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in completionData)
            {
                if (data.episode == m_episode)
                {
                    return data.score;
                }
            }
            return 0;
        }

        [SerializeField] private EpisodeScore[] completionData;
        public int TotalScore { private set; get; }

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
            foreach (var episodeScore in completionData)
            {
               TotalScore += episodeScore.score;
            }
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score)
                    {
                        TotalScore += levelScore - item.score;
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, completionData); 
                    }
                }
            }
        }
    }
}