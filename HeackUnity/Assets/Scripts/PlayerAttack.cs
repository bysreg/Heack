using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Heack
{
    public class PlayerAttack : MonoBehaviour
    {

        enum Status
        {
            Attack,
            Defense,
        }    

        Status status;
        static List<Status> statusList;

        Rigidbody2D rigidBody2D;

        Player player;

        void Awake()
        {
            RandomizeStatus();
            rigidBody2D = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
        }

        void RandomizeStatus()
        {
            if (statusList == null)
            {
                statusList = new List<Status>();
                statusList.Add(Status.Attack);
                statusList.Add(Status.Attack);
                statusList.Add(Status.Defense);
                statusList.Add(Status.Defense);
                //shuffle the list
                for (int i = 0; i < statusList.Count; i++)
                {
                    Status temp = statusList[i];
                    int shuffleIndex = Random.Range(i, statusList.Count);
                    statusList[i] = statusList[shuffleIndex];
                    statusList[shuffleIndex] = temp;
                }
            }

            int random = Random.Range(0, statusList.Count);
            status = statusList[random];
            statusList.RemoveAt(random);
            //print(index + " " + status);
            switch (status)
            {
                case Status.Attack:
                    GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case Status.Defense:
                    // TODO : 
                    break;
            }
        }

        void KnockedDown(Vector3 direction, GameObject from)
        {            
            //rigidBody2D.AddForce();
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            PlayerAttack otherAttack = coll.gameObject.GetComponent<PlayerAttack>();
            if(otherAttack != null && status == Status.Attack && otherAttack.status == Status.Defense)
            {
                print("attacking");

                Vector3 direction = new Vector3();
                if(otherAttack.player.faceDir == Player.FaceDir.Up)
                {

                }

                //otherAttack.KnockedDown();
            }
        }

    }

}