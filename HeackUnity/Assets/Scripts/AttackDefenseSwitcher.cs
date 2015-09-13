using UnityEngine;
using System.Collections;

namespace Heack
{
    public class AttackDefenseSwitcher : MonoBehaviour
    {

        [SerializeField]
        float maxSwitchTime;

        [SerializeField]
        PlayerAttack[] players;

        float switchTime;

        void Awake()
        {
            switchTime = maxSwitchTime;
        }

        void Update()
        {
            switchTime -= Time.deltaTime;
            if(switchTime <= 0)
            {
                Switch();
                switchTime = maxSwitchTime;
            }
        }

        void Switch()
        {
            foreach(var p in players)
            {
                if(p.status == PlayerAttack.Status.Attack)
                {
                    p.SetStatus(PlayerAttack.Status.Defense);
                }
                else
                {
                    p.SetStatus(PlayerAttack.Status.Attack);
                }
            }
        }

    }

}