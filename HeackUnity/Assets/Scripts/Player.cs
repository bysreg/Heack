using UnityEngine;
using System.Collections;
using BladeCast;
using System.Collections.Generic;

namespace Heack
{
    public class Player : MonoBehaviour
    {
        float playerZ;

        public int index;

        [SerializeField]
        float speed;

        [SerializeField]
        float acceleroThreshold;
        
        public float spawnOffset;

        Vector3 direction;
        Vector2 recentLRFB;

        bool isDied;

        PlayerAttack playerAttack;
        public Respawner respawner;
        [SerializeField]
        PlayerMessanger playerMessanger;
 
        public enum FaceDir
        {
            Down = 0,
            Up, 
            Right, 
            Left,             
        }

        public FaceDir faceDir;

        Animator animator;
        [SerializeField]
        Collider2D collider;
        [SerializeField]
        Collider2D trigger;

        void Awake()
        {
            BCMessenger.Instance.RegisterListener("tiltLR", 0, this.gameObject, "HandleTiltLR");
            BCMessenger.Instance.RegisterListener("tiltFB", 0, this.gameObject, "HandleTiltFB");

            playerAttack = GetComponent<PlayerAttack>();
            animator = GetComponent<Animator>();
            respawner = GetComponent<Respawner>();            
        }

        void Start()
        {
            playerZ = transform.position.z;

            Vector2 tilePos = new Vector2();
            switch (index)
            {
                case 1:
                    tilePos.x = 0  + spawnOffset; tilePos.y = 0  + spawnOffset;
                    break;
                case 2:
                    tilePos.x = GridArena.Instance.Width - 1 - spawnOffset; tilePos.y = 0  +spawnOffset;
                    break;
                case 3:
                    tilePos.x = GridArena.Instance.Width - 1 - spawnOffset; tilePos.y = GridArena.Instance.Height - 1  - spawnOffset;
                    break;
                case 4:
                    tilePos.x = 0  +spawnOffset; tilePos.y = GridArena.Instance.Height - 1 - spawnOffset;
                    break;
            }

            MoveToTile(tilePos);
        }        

        void Update()
        {
            if(!isDied && !playerAttack.IsKnocked())
            {
                MoveViaAccelero(recentLRFB);
                MoveToward(direction); // for the accelerometer
                MoveViaKeyboard();
                MoveToward(direction); // for the keyboard
            }            

            //check if this player is out of bounds
            if (CheckOutOfBounds() && !isDied)
            {
                Died();                
            }
        }

        void Died()
        {
            this.animator.CrossFade("Fell_A", 0f);
            this.transform.Find("Hunt").gameObject.SetActive(false);
            isDied = true;

            //disable collider and trigger, so that it cant be interacted physically
            collider.enabled = false;
            trigger.enabled = false;

            if (playerAttack.lastHitFrom != null)
            {                
                ScoreManager.Instance.IncScore(playerAttack.lastHitFrom.GetComponent<Player>().index);
            }

            playerMessanger.SendDiedSignalToController(index);
            respawner.StartCountDown();
        }

        public void Reset()
        {
            animator.CrossFade("Run_Front_A", 0);
            isDied = false;
            collider.enabled = true;
            trigger.enabled = true;

            playerAttack.Reset();
        }

        public bool IsDied()
        {
            return isDied;
        }

        void MoveViaKeyboard()
        {
            if (!playerAttack.isAfterAttack)
            {
                //test
                if (index == 1)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        direction.y = 1;
                        direction.x = 0;
                    }
                    else if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        direction.y = 0;
                        direction.x = -1;
                    }
                    else if (Input.GetKey(KeyCode.RightArrow))
                    {
                        direction.y = 0;
                        direction.x = 1;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        direction.y = -1;
                        direction.x = 0;
                    }
                    else
                    {
                        direction.y = 0;
                        direction.x = 0;
                    }
                }
                else if (index == 2)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        direction.y = 1;
                        direction.x = 0;
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        direction.y = 0;
                        direction.x = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        direction.y = 0;
                        direction.x = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        direction.y = -1;
                        direction.x = 0;
                    }
                    else
                    {
                        direction.y = 0;
                        direction.x = 0;
                    }
                }
            }
        }

        void MoveToward(Vector3 direction)
        {
            if (!playerAttack.isAfterAttack)
            {
                if (direction.x == 0 && direction.y == 1)
                {
                    faceDir = FaceDir.Up;
                    this.gameObject.GetComponent<Animator>().CrossFade("Run_Back_A", 0f);
                }
                else if (direction.x == -1 && direction.y == 0)
                {
                    faceDir = FaceDir.Left;
                    this.gameObject.GetComponent<Animator>().CrossFade("Run_Left_A", 0f);
                }
                else if (direction.x == 1 && direction.y == 0)
                {
                    faceDir = FaceDir.Right;
                    this.gameObject.GetComponent<Animator>().CrossFade("Run_Right_A", 0f);
                }
                else if (direction.x == 0 && direction.y == -1)
                {
                    faceDir = FaceDir.Down;
                    this.gameObject.GetComponent<Animator>().CrossFade("Run_Front_A", 0f);
                }

                transform.position += direction * Time.deltaTime * speed;
            }                     
        }

        void MoveToTile(Vector2 tilePos)
        {
            transform.position = new Vector3(tilePos.x, tilePos.y, playerZ);
        }

        bool CheckOutOfBounds()
        {
            if (transform.position.x + 0.1f < GridArena.Instance.GetLeft())
            {                
                return true;
            }
            if (transform.position.x - 0.1f > GridArena.Instance.GetRight())
            {                
                return true;
            }
            if (transform.position.y + 0.1f < GridArena.Instance.GetDown())
            {                
                return true;
            }
            if (transform.position.y - 0.2f > GridArena.Instance.GetTop())
            {               
                return true;
            }

            return false;
        }

        void MoveViaAccelero(Vector2 recentLRFB)
        {
            if (Mathf.Abs(recentLRFB.x) < acceleroThreshold && Mathf.Abs(recentLRFB.y) < acceleroThreshold)
            {
                direction.x = 0;
                direction.y = 0;
            }
            else if (Mathf.Abs(recentLRFB.x) > Mathf.Abs(recentLRFB.y))
            {
                if (recentLRFB.x > 0)
                {
                    direction.y = -1;
                    direction.x = 0;
                }
                else
                {
                    direction.y = 1;
                    direction.x = 0;
                }
            }
            else
            {
                if (recentLRFB.y > 0)
                {
                    direction.x = -1;
                    direction.y = 0;
                }
                else
                {
                    direction.x = 1;
                    direction.y = 0;
                }
            }

            recentLRFB.x = 0;
            recentLRFB.y = 0;
        }

        void HandleTiltLR(ControllerMessage msg)
        {
            string val_raw = msg.Payload.GetField("val").ToString();
            float val_parsed;
            if (float.TryParse(val_raw, out val_parsed))
            {
                //print("LR : (" + msg.ControllerSource + ") " + val_parsed);
                if (msg.ControllerSource == index)
                {
                    recentLRFB.x = val_parsed;
                }
            }
        }

        void HandleTiltFB(ControllerMessage msg)
        {
            string val_raw = msg.Payload.GetField("val").ToString();
            float val_parsed;
            if (float.TryParse(val_raw, out val_parsed))
            {
                //print("FB : (" + msg.ControllerSource + ") " + val_parsed);
                if (msg.ControllerSource == index)
                {
                    recentLRFB.y = val_parsed;
                }
            }
        }        
    }

}

