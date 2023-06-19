using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;

        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapComplettion.filename); 
        }
        public void NewGame()
        {
            FileHandler.Reset(MapComplettion.filename);
            FileHandler.Reset(Upgrades.filename);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}