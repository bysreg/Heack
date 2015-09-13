using UnityEngine;
using System.Collections;

namespace Heack
{
    public class GridArena : Common.Singleton<GridArena>
    {
        
        [SerializeField]
        int width;
        
        [SerializeField]
        int height;        

        [SerializeField]
        Transform[] tilePrefabs;

        [SerializeField]
        Transform tilesLayer;

        Transform[,] tilesArr;

        public int Width
        {
            get { return width; }
            private set { width = value; }
        }

        public int Height
        {
            get { return height; }
            private set { height = value; }
        }

        public float GetRight()
        {
            return GetLeft() + Width;
        }                                       

        public float GetLeft()
        {
            return -0.5f;
        }

        public float GetTop()
        {
            return GetDown() + Height;
        }

        public float GetDown()
        {
            return -0.5f; 
        }

        protected override void Awake()
        {
            base.Awake();
            tilesArr = new Transform[height, width];
        }

        void Start()
        {
            InitGrids(width, height);
        }

        void InitGrids(int width, int height)
        {
            CreateTile(0, 0, 0);
            CreateTile(width - 1, 0, 1);
            CreateTile(0, height - 1, 9);
            CreateTile(width - 1, height - 1, 10);

            for (int i = 1; i < height-1; i++) //create for left corner of the screen
            {
                CreateTile(0, i, 6);
            }

            for (int i = 1; i < height - 1; i++) //create for right corner of the screen
            {
                CreateTile(width - 1, i, 7);
            }

            for (int i = 1; i < width - 1; i++) //create for bottom corner of the screen
            {
                CreateTile(i, 0, 5);
            }

            for (int i = 1; i < width - 1; i++) //create for up corner of the screen
            {
                CreateTile(i, height - 1, 8);
            }

            for (int i=1;i<height-1;i++)
            {
                for(int j=1;j<width-1;j++)
                {
                    int randomID = Random.Range(1, 10);

                    if (randomID >= 1 && randomID < 5)
                    {
                        CreateTile(j, i, 2);
                    }
                    else
                    if (randomID >= 5 && randomID < 9)
                    {
                        CreateTile(j, i, 3);
                    }
                    else
                    if (randomID >= 9)
                    {
                        CreateTile(j, i, 4);
                    }
                }
            }

            for (int i = 7; i < 29; i++)
            {
                for (int j = 7; j < 29; j++)
                {
                    int randomID = Random.Range(1, 10);

                    if (randomID >= 1 && randomID < 8)
                    {
                        CreateWaterTile(j-11, i-11, 11);
                    }
                    else
                    if (randomID >= 8 && randomID < 11)
                    {
                        CreateWaterTile(j-11, i-11, 12);
                    }
                }
            }
        }

        void CreateTile(int x, int y, int type)
        {
            Transform tilePrefab = tilePrefabs[type];
            Transform t = GameObject.Instantiate(tilePrefabs[type], new Vector3(x, y - 0.25f, 0), Quaternion.identity) as Transform;
            t.name = tilePrefab.name + (y * height + x);
            t.parent = tilesLayer.transform;
            tilesArr[y, x] = t;
        }

        void CreateWaterTile(int x, int y, int type)
        {
            Transform tilePrefab = tilePrefabs[type];
            Transform t = GameObject.Instantiate(tilePrefabs[type], new Vector3(x, y - 0.25f, 0), Quaternion.identity) as Transform;
            t.name = tilePrefab.name + (y * height + x);
            t.parent = tilesLayer.transform;
            //tilesArr[y, x] = t;
        }
    }

}
