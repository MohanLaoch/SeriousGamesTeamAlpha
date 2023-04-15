using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace OlayScripts
{
    public class RunningGameHighScore : MonoBehaviour
    {
        public TextMeshProUGUI text;
        private bool resetScore;
        public int TemporaryHighScore;
        private void Start()
        {
            RunningGameManager.instance.GameOverEvent += OnGameOver;
            
        }

        void OnGameOver()
        {
            SetHighScore(RunningGameManager.instance.walkingScore);
        }

        public void SetHighScore(int score)
        {
            bool isHighScore = false;
            int currentHighScore = 0;
            currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
            
            
            

            if (score > currentHighScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                isHighScore = true;
            }


            int highScore = PlayerPrefs.GetInt("HighScore");
            string colour = "";

            switch (isHighScore)
            {
                case true:
                    text.text = $"<color=white>Final Score: <color=green>{highScore} (IT'S A NEW HIGH SCORE!)</color>";
                    break;
                default:
                    text.text = $"Final Score: <color=white>{score}</color> \n \n <color=green>High Score: {highScore}</color>";
                    break;
            }
            
            
            

            RunningGameManager.instance.GameOverEvent -= OnGameOver;
        }
        
        

        public void SetHighScoreTemp(int value)
        {
            PlayerPrefs.SetInt("HighScore", value);
        }
    }

    [CustomEditor(typeof(RunningGameHighScore))]
    public class HighScoreEditor : Editor
    {
        override public void OnInspectorGUI()
        {

            DrawDefaultInspector();
            
            RunningGameHighScore script = (RunningGameHighScore)target;
            if (GUILayout.Button("Reset High Scores"))
            {
                if (PlayerPrefs.HasKey("HighScore"))
                {
                    PlayerPrefs.DeleteKey("HighScore");
                    Debug.Log("High Score Reset!");
                }

                else
                {
                    Debug.LogError("No High Score!");
                }
            }

            if (GUILayout.Button("Make Temporary HighScore"))
            {
                script.SetHighScoreTemp(script.TemporaryHighScore);
                Debug.Log("Score Set to Be " + script.TemporaryHighScore);
            }
        }
    }
}