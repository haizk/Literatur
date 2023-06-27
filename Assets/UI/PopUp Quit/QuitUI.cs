using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

namespace EasyUI.Wins {


    public class QuitUI : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] TMP_Text titleUIText;
        [SerializeField] Button yesUIButton;
        [SerializeField] Button noUIButton;

        //Singleton pattern
        public static QuitUI Instance;

        void Awake () {
            Instance = this;

            // Add close event listener
            noUIButton.onClick.RemoveAllListeners();
            noUIButton.onClick.AddListener(Hide);
        }

        // Show Quit
        public void Show() {
            canvas.SetActive (true);
        }

        // Hide Quit
        public void Hide() {
            canvas.SetActive (false);
        }
    }
}
