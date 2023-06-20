using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace EasyUI.Loses {

    public class Lose
    
    {
        public string Title = "Title";
        public string Definition = "Definition";
    }

    public class LoseUI : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] TMP_Text titleUIText;
        [SerializeField] TMP_Text definitionUIText;
        [SerializeField] Button restartUIButton;
        [SerializeField] Button exitLoseUIButton;

        Lose lose = new Lose ();

        //Singleton pattern
        public static LoseUI Instance;

        void Awake () {
            Instance = this;

            // Add close event listener
            exitLoseUIButton.onClick.RemoveAllListeners();
            exitLoseUIButton.onClick.AddListener(Hide);
        }

        // Set lose Title
        public LoseUI Title (string title) {
            lose.Title = title;
            return Instance;
        }

        // Set lose Definiton
        public LoseUI Definition (string definition) {
            lose.Definition = definition;
            return Instance;
        }

        // Show lose
        public void Show() {
            titleUIText.text = lose.Title;
            definitionUIText.text = lose.Definition;

            canvas.SetActive (true);
        }

        // Hide lose
        public void Hide() {
            canvas.SetActive (false);

            // Reset lose
            lose = new Lose();
        }
    }
}
