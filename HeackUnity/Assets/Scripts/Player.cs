using UnityEngine;
using System.Collections;

namespace Heack
{
    public class Player : MonoBehaviour
    {

        Vector2 tilePos;
        float playerZ;

        public int index;

        void Awake()
        {
            playerZ = transform.position.z;

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
            MoveQuadToTile(tilePos);
        }

        void Update()
        {
            //test
            if(index == 1)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    tilePos.y += 1;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    tilePos.x -= 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    tilePos.x += 1;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    tilePos.y -= 1;
                }
            }            

            MoveQuadToTile(tilePos);
        }

        void MoveQuadToTile(Vector2 tilePos)
        {
            transform.position = new Vector3(tilePos.x, tilePos.y, playerZ);
        }
    }

}

