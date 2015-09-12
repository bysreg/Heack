using UnityEngine;
using System.Collections;

namespace Heack
{
    public class Player : MonoBehaviour
    {                
        float playerZ;

        public int index;

        [SerializeField]
        float speed;

        Vector3 direction;

        void Awake()
        {
            playerZ = transform.position.z;

            Vector2 tilePos = new Vector2();
            switch(index)
            {
                case 1:
                    tilePos.x = 0; tilePos.y = 0;
                    break;
                case 2:
                    tilePos.x = GridArena.Instance.Width - 1; tilePos.y = 0;
                    break;
                case 3:
                    tilePos.x = GridArena.Instance.Width - 1; tilePos.y = GridArena.Instance.Height - 1;
                    break;
                case 4:
                    tilePos.x = 0; tilePos.y = GridArena.Instance.Height-1;
                    break;
            }
            MoveToTile(tilePos);
        }

        void Update()
        {
            //test
            if(index == 1)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {                    
                    direction.y = 1;
                    direction.x = 0;

                    this.gameObject.GetComponent<Animator>().CrossFade("Run_Back_A", 0f);
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

                    this.gameObject.GetComponent<Animator>().CrossFade("Run_Front_A", 0f);
                }
                else
                {
                    direction.y = 0;
                    direction.x = 0;
                }
            }            
            
            MoveToward(direction);

            //check if this player is out of bounds
            if (CheckOutOfBounds())
            {
                print("dead");
            }
        }

        void MoveToward(Vector3 direction)
        {
            transform.position += direction * Time.deltaTime * speed;            
        }

        void MoveToTile(Vector2 tilePos)
        {
            transform.position = new Vector3(tilePos.x, tilePos.y, playerZ);
        }

        bool CheckOutOfBounds()
        {

            if (transform.position.x + 0.5f < GridArena.Instance.GetLeft())
            {
                return true;
            }
            if(transform.position.x - 0.5f > GridArena.Instance.GetRight())
            {
                return true;
            }
            if(transform.position.y + 0.5f < GridArena.Instance.GetDown())
            {
                return true;
            }
            if(transform.position.y - 0.5f > GridArena.Instance.GetTop())
            {
                return true;
            }

            return false;
        }
    }

}

