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

        bool isKnocked;
        Vector3 knockDirection;
        float KnockMagnitude;
        float knockTime;

        [SerializeField]
        float knockMaxMagnitude = 10;        
        [SerializeField]
        float knockMaxTime = 0.3f;

        [SerializeField]
        float bumpMaxMagnitude = 10;
        [SerializeField]
        float bumpMaxTime = 0.15f;

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

        void KnockedDown(Vector2 direction, GameObject from)
        {         
            isKnocked = true;
            knockDirection = direction;
            KnockMagnitude = knockMaxMagnitude;
            knockTime = knockMaxTime;
        }

        void Bumped(Vector2 direction, GameObject from)
        {
            isKnocked = true;
            knockDirection = direction;
            KnockMagnitude = bumpMaxMagnitude;
            knockTime = bumpMaxTime;
        }

        void Update()
        {
            if(isKnocked)
            {
                transform.position += knockDirection * Time.deltaTime * KnockMagnitude;                
                knockTime -= Time.deltaTime;
                if(knockTime < 0)
                {
                    isKnocked = false;
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            PlayerAttack otherAttack = collider.gameObject.GetComponent<PlayerAttack>();

            if (otherAttack == null)
                return;

            Vector2 direction = new Vector2();

            switch (player.faceDir)
            {
                case Player.FaceDir.Up:
                    direction.y = 1;
                    break;
                case Player.FaceDir.Down:
                    direction.y = -1;
                    break;
                case Player.FaceDir.Left:
                    direction.x = -1;
                    break;
                case Player.FaceDir.Right:
                    direction.x = 1;
                    break;
            }

            if (status == Status.Attack && otherAttack.status == Status.Defense)
            {
                //print("attacking");

                otherAttack.KnockedDown(direction, this.gameObject);
            }
            else if((status == Status.Attack && otherAttack.status == Status.Attack) || (status == Status.Defense && otherAttack.status == Status.Defense) )
            {
                otherAttack.Bumped(direction, this.gameObject);
            }            
        }        

    }

}