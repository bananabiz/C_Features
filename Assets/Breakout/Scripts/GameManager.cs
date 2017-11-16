using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Breakout
{
    public class GameManager : MonoBehaviour
    {

        public int width = 20;
        public int height = 15;
        //public int total;
        public Vector2 spacing = new Vector2(25f, 10f);
        public Vector2 offset = new Vector2(-25f, 0f);
        public GameObject[] blockPrefabs;
        [Header("Debugging")]
        public bool isDebugging = false;

        private GameObject[,] spawnedBlocks;

      
        // Use this for initialization
        void Start()
        {
            GenerateBlocks();
            //total = width * height;
        }
        // Function with arguments
        //<return-type> <function-name> (arguments)
        GameObject GetBlockByIndex(int index)
        {
            // Error handling
            if (index > blockPrefabs.Length || index < 0)
                return null;
            // Create a new block at given index
            GameObject clone = Instantiate(blockPrefabs[index]);
            // ... and return it 
            return clone;
        }


        GameObject GetRandomBlock()
        {
            // Randomly Spawn a new GameObject
            int randomIndex = Random.Range(0, blockPrefabs.Length);
            GameObject randomPrefab = blockPrefabs[randomIndex];
            GameObject clone = Instantiate(randomPrefab);
            // ... and return it
            return clone;
        }

        void GenerateBlocks()
        {
            spawnedBlocks = new GameObject[width, height];
            // Loop through the width
            for (int x = 0; x < width; x++)
            { // Open brace
                for (int y = 0; y < height; y++)
                {
                    // Create new instance of the block
                    GameObject block = GetRandomBlock();
                    // Set the new position
                    Vector2 pos = new Vector2(x * spacing.x, y * spacing.y);
                    pos += offset;
                    block.transform.position = pos;
                    // Add spawned blocks to array
                    spawnedBlocks[x, y] = block;
                }
            } // Close brace
        }

        void UpdateBlocks()
        {
            // Loop through entire 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Update position
                    GameObject currentBlock = spawnedBlocks[x, y];
                    // Creat a new position
                    Vector2 pos = new Vector2(x * spacing.x, y * spacing.y);
                    // Add an offset to pos
                    pos += offset;
                    // Set currentBlock's position to new pos
                    currentBlock.transform.position = pos;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isDebugging)
            {
                UpdateBlocks();
            }
        }
    }
}
