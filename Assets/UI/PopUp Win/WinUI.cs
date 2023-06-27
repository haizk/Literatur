using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace EasyUI.Wins {


    public class WinUI : MonoBehaviour
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Button nextUIButton;
        [SerializeField] Button exitWinUIButton;

        //Singleton pattern
        public static WinUI Instance;

        void Awake () {
            Instance = this;

            // Add close event listener
            exitWinUIButton.onClick.RemoveAllListeners();
            exitWinUIButton.onClick.AddListener(Hide);
        }

        // Show Win
        public void Show() {
            canvas.SetActive (true);
        }

        // Hide Win
        public void Hide() {
            canvas.SetActive (false);
        }
    }
}
