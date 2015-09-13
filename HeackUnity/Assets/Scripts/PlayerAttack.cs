using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Heack
{
    public class PlayerAttack : MonoBehaviour
    {
        public bool isAfterAttack;

        public enum Status
        {
            Attack,
            Defense,
        }    

        public Status status;
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

        public GameObject lastHitFrom; // record who lands the last hit on this player, will reset back to null if lastHitExpireTime hits zero
        float lastHitExpireTime;
        float HIT_EXPIRE_TIME = 2f;

        void Awake()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
            RandomizeStatus();
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
            SetStatus(status);
        }

        public bool IsKnocked()
        {
            return isKnocked;
        }

        void KnockedDown(Vector2 direction, GameObject from)
        {         
            isKnocked = true;
            knockDirection = direction;
            KnockMagnitude = knockMaxMagnitude;
            knockTime = knockMaxTime;
            lastHitFrom = from;

            print("Knocked " + this.gameObject.name);

            if (direction.x == -1) //if bumped to the left
            {
                GetComponent<Animator>().CrossFade("Dragged_Left_A", 0f);
            }
            else
            if (direction.x == 1) //if bumped to the right
            {
                GetComponent<Animator>().CrossFade("Dragged_Right_A", 0f);
            }
            else
            if (direction.y == 1) //if bumped to the right
            {
                GetComponent<Animator>().CrossFade("Dragged_Front_A", 0f);
            }
            else
            if (direction.y == -1) //if bumped to the right
            {
                GetComponent<Animator>().CrossFade("Dragged_Back_A", 0f);
            }
        }

        void Bumped(Vector2 direction, GameObject from)
        {
            isKnocked = true;
            knockDirection = direction;
            KnockMagnitude = bumpMaxMagnitude;
            knockTime = bumpMaxTime;
            lastHitFrom = from;

            print("Bumped " + this.gameObject.name);

            switch (player.faceDir)
            {
                case Player.FaceDir.Up:
                    GetComponent<Animator>().CrossFade("Run_Attack_Front_A", 0f);
                    transform.Find("Attack").gameObject.SetActive(true);
                    transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Front_Back_A", 0f);
                    isAfterAttack = true;

                    StartCoroutine(WaitToMoveAfterAttack());
                    break;
                case Player.FaceDir.Down:
                    GetComponent<Animator>().CrossFade("Run_Attack_Back_A", 0f);
                    transform.Find("Attack").gameObject.SetActive(true);
                    transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Front_Back_A", 0f);
                    isAfterAttack = true;

                    StartCoroutine(WaitToMoveAfterAttack());
                    break;
                case Player.FaceDir.Left:
                    GetComponent<Animator>().CrossFade("Run_Attack_Left_A", 0f);
                    transform.Find("Attack").gameObject.SetActive(true);
                    transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Right_Left_A", 0f);
                    isAfterAttack = true;

                    StartCoroutine(WaitToMoveAfterAttack());
                    break;
                case Player.FaceDir.Right:
                    GetComponent<Animator>().CrossFade("Run_Attack_Right_A", 0f);
                    transform.Find("Attack").gameObject.SetActive(true);
                    transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Right_Left_A", 0f);
                    isAfterAttack = true;

                    StartCoroutine(WaitToMoveAfterAttack());
                    break;
            }
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

            if (lastHitExpireTime > 0)
            {
                lastHitExpireTime -= Time.deltaTime;

                if (lastHitExpireTime <= 0)
                {
                    lastHitFrom = null;
                }
            }
        }

        public void SetStatus(Status status)
        {
            this.status = status;

            if(player.IsDied())
            {
                return;
            }

            switch (status)
            {
                case Status.Attack:
                    //GetComponent<SpriteRenderer>().color = Color.red;
                    GetComponent<Transform>().Find("Hunt").gameObject.SetActive(true); //activate the hunt effect animation
                    break;
                case Status.Defense:                    
                    GetComponent<Transform>().Find("Hunt").gameObject.SetActive(false); //deactivate the hunt effect animation
                    break;
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

                switch (player.faceDir)
                {
                    case Player.FaceDir.Up:
                        GetComponent<Animator>().CrossFade("Run_Attack_Front_A", 0f);
                        transform.Find("Attack").gameObject.SetActive(true);
                        transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Front_Back_A", 0f);
                        isAfterAttack = true;

                        StartCoroutine(WaitToMoveAfterAttack());
                        break;
                    case Player.FaceDir.Down:
                        GetComponent<Animator>().CrossFade("Run_Attack_Back_A", 0f);
                        transform.Find("Attack").gameObject.SetActive(true);
                        transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Front_Back_A", 0f);
                        isAfterAttack = true;

                        StartCoroutine(WaitToMoveAfterAttack());
                        break;
                    case Player.FaceDir.Left:
                        GetComponent<Animator>().CrossFade("Run_Attack_Left_A", 0f);
                        transform.Find("Attack").gameObject.SetActive(true);
                        transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Right_Left_A", 0f);
                        isAfterAttack = true;

                        StartCoroutine(WaitToMoveAfterAttack());
                        break;
                    case Player.FaceDir.Right:
                        GetComponent<Animator>().CrossFade("Run_Attack_Right_A", 0f);
                        transform.Find("Attack").gameObject.SetActive(true);
                        transform.Find("Attack").GetComponent<Animator>().CrossFade("Attack_Right_Left_A", 0f);
                        isAfterAttack = true;

                        StartCoroutine(WaitToMoveAfterAttack());
                        break;
                }

                otherAttack.KnockedDown(direction, this.gameObject);
            }
            else if((status == Status.Attack && otherAttack.status == Status.Attack) || (status == Status.Defense && otherAttack.status == Status.Defense) )
            {
                otherAttack.Bumped(direction, this.gameObject);
            }            
        }

        IEnumerator WaitToMoveAfterAttack()
        {
            yield return new WaitForSeconds(1f);

            isAfterAttack = false;

            transform.Find("Attack").gameObject.SetActive(false);
        }
    }

}