using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        // store x and y coordinate in array for later use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // is the current tile a mine?
        public bool isRevealed = false; // has the tile already been revealed?

        [Header("References")]
        public Sprite[] emptySprites; // list of empty sprites
        public Sprite[] mineSprites; // the mine sprites

        private SpriteRenderer rend; // reference to the sprite renderer

        void Awake()
        {
            // grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void Start()
        {
            // randomly decide if it's a min or not
            isMine = Random.value < 0.1f;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // flags the tile as being revealed
            isRevealed = true;
            // Check if tile is a mine
            if (isMine)
            {
                print("You Lose");
                // sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // sets sprite to appropriate texture base on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }

    }
}