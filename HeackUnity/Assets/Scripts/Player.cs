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
                
            }
        }

        void Update()
        {
            //test
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                tilePos.y += 1;
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tilePos.x -= 1;
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                tilePos.x += 1;
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                tilePos.y -= 1;
            }

            MoveQuadToTile(tilePos);
        }

        void MoveQuadToTile(Vector2 tilePos)
        {
            transform.position = new Vector3(tilePos.x, tilePos.y, playerZ);
        }
    }

}

