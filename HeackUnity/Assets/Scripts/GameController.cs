using UnityEngine;
using System.Collections;
using BladeCast;

namespace Heack
{
    public class GameController : MonoBehaviour
    {
        void Start()
        {
            InitControllerListeners();
        }

        void InitControllerListeners()
        {
            BCMessenger.Instance.RegisterListener("connect", 0, this.gameObject, "HandleControllerConnected");
        }

        void HandleControllerConnected()
        {
            print("Connected to controller");
        }
    }

}