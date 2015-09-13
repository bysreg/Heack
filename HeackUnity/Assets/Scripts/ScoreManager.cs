using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Heack
{
    public class ScoreManager : Common.Singleton<ScoreManager>
    {
        [SerializeField]
        Text[] scoresText;

        int[] scores;        

        protected override void Awake()
        {
            base.Awake();
            scores = new int[scoresText.Length];
        }

        public void IncScore(int playerIndex)
        {
            scores[playerIndex-1]++;
            scoresText[playerIndex-1].text = "" + scores[playerIndex-1];
        }
    }
}

