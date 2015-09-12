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
            for(int i=0;i<height;i++)
            {
                for(int j=0;j<width;j++)
                {
                    CreateTile(j, i, 0);
                }
            }
        }

        void CreateTile(int x, int y, int type)
        {
            Transform tilePrefab = tilePrefabs[type];
            Transform t = GameObject.Instantiate(tilePrefabs[type], new Vector3(x, y, 0), Quaternion.identity) as Transform;
            t.name = tilePrefab.name + (y * height + x);
            t.parent = tilesLayer.transform;
            tilesArr[y, x] = t;
        }
    }

}
