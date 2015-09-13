using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Heack
{
    public class Timer : MonoBehaviour
    {

        float timeLeft;
        [SerializeField]
        float maxTime;

        [SerializeField]
        Text timerText;

        [SerializeField]
        Text winnerText;

        [SerializeField]
        ScoreManager scoreManager;

        bool isFinished;

        void Awake()
        {
            timeLeft = maxTime;
        }

        void Update()
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;                

                if(timeLeft <= 0)
                {
                    timeLeft = 0;                    
                }

                timerText.text = (((int)(timeLeft * 100f)) / 100f) + "";                
            }            
            else if(timeLeft <=0 && !isFinished)
            {
                FinishGame();
                isFinished = true;
            }
        }

        void FinishGame()
        {
            winnerText.gameObject.active = true;

            int maxScore = -1;
            int maxPlayerId = -1;
            for(int i=0;i<4;i++)
            {
                int playerScore = scoreManager.GetPlayerScore(i+1);
                if(maxScore < playerScore)
                {
                    maxScore = playerScore;
                    maxPlayerId = i+1;
                }
            }

            winnerText.text = "Winner : Player " + maxPlayerId;
        }

    }

}