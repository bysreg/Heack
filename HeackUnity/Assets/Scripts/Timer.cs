using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Haeck
{
    public class Timer : MonoBehaviour
    {

        float timeLeft;
        [SerializeField]
        float maxTime;

        [SerializeField]
        Text timerText;

        void Awake()
        {
            timeLeft = maxTime;
        }

        void Update()
        {
            timeLeft -= Time.deltaTime;

            timerText.text = (((int)(timeLeft * 100f)) / 100f) + "";
        }

    }

}