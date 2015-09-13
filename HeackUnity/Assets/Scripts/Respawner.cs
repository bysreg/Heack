using UnityEngine;
using System.Collections;

namespace Heack
{
    public class Respawner : MonoBehaviour
    {

        [SerializeField]
        public float maxSpawnTime;

        float curSpawnTime;
        bool isCounting;

        Player player;

        [SerializeField]
        float maxBlinkTime;
        bool isSpawning;
        float blinkTime;

        [SerializeField]
        float maxFlipTime;
        float flipTime;

        Vector3 spawnPoint;

        SpriteRenderer renderer;
        Animator animator;

        void Awake()
        {
            player = GetComponent<Player>();
            renderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            spawnPoint = new Vector3();
            switch (player.index)
            {
                case 1:
                    spawnPoint.x = 0;
                    spawnPoint.y = 0;
                    break;
                case 2:
                    spawnPoint.x = GridArena.Instance.Width - 1;
                    spawnPoint.y = 0;
                    break;
                case 3:
                    spawnPoint.x = GridArena.Instance.Width - 1;
                    spawnPoint.y = GridArena.Instance.Height - 1;
                    break;
                case 4:
                    spawnPoint.x = 0;
                    spawnPoint.y = GridArena.Instance.Height - 1;
                    break;
            }
        }

        void Update()
        {
            if(isCounting)
            {
                print("time to spawn : " + curSpawnTime);
                curSpawnTime -= Time.deltaTime;
                if (curSpawnTime <= 0)
                {
                    Respawn();
                    isCounting = false;
                }
            }
            
            if(isSpawning)
            {
                //print("time to finish blinking : " + blinkTime);
                blinkTime -= Time.deltaTime;
                flipTime += Time.deltaTime;

                if(flipTime > maxFlipTime)
                {
                    flipTime = 0;
                    if (renderer.enabled)
                        renderer.enabled = false;
                    else
                        renderer.enabled = true;
                }

                if(blinkTime < 0)
                {
                    renderer.enabled = true;
                    player.Reset();
                    player.transform.position = spawnPoint;
                    isSpawning = false;
                }
            }
        }

        public void StartCountDown()
        {
            isCounting = true;
            curSpawnTime = maxSpawnTime;
        }

        void Respawn()
        {
            // blink the player for certain amount of time
            blinkTime = maxBlinkTime;
            isSpawning = true;
            flipTime = 0;
            animator.CrossFade("Run_Front_A", 0);
            player.transform.position = spawnPoint;
        }        
    }

}
