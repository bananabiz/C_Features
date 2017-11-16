using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper2D
{
    public class Grid : MonoBehaviour
    {
        public enum mineState
        {
            Loss = 0,
            Win = 1
        }
        public GameObject tilePrefab;
        public static int width = 10;
        public static int height = 10;
        public float spacing = 0.155f;
        public static Tile[,] tiles = new Tile[width, height];

        private float offset = 0.5f;

        // Use this for initialization
        void Start()
        {
            // Generate tiles on startup
            GenerateTiles();
        }

        // functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            // clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; // position tile
            Tile currentTile = clone.GetComponent<Tile>(); // get tile component
            return currentTile; // return it
        }
        // spawn tiles in a grid-like pattern
        void GenerateTiles()
        {
            // create new 2D array of size width * height
            //tiles = new Tile[width, height];

            // loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // store half size for later use
                    Vector2 halfSize = new Vector2(width / 2, height / 2);
                    // Pivot tiles around grid
                    Vector2 pos = new Vector2((x + offset) - halfSize.x, (y + offset) - halfSize.y); // applying offset to make it centre
                    // apply spacing
                    pos *= spacing;
                    // spawn the tile
                    Tile tile = SpawnTile(pos);
                    // attach newly spawn tile to
                    tile.transform.SetParent(transform);
                    // store it's array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // store tile in array at those coordinates
                    tiles[x, y] = tile;
                }
            }
        }
        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;
            // loop through all elements and have each axis go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                // calculate desired coordinates from ones attained
                for (int y = -1; y <= 1; y++)
                {
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;

                    if (desiredX >= 0 && desiredY >= 0 &&
                        desiredX < width && desiredY < height)
                    {
                        Tile tile = tiles[desiredX, desiredY];
                        if (tile.isMine)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;

        }

        public void FFuncover(int x, int y, bool[,] visited)
        {
            // If x >= 0 AND x < width AND y < height
            if (x >= 0 && x < width && y < height)
            {
                // If visited[x, y]
                Debug.Log(visited[x, y]); 
                if (visited[x, y])
                {
                    // RETURN
                    return;
                }
                // Let tile = tiles[x, y]
                Tile tile = tiles[x, y];
                // Let adjacentMines = GetAdjacentMineCountAt(tile)
                int adjacentMines = GetAdjacentMineCountAt(tile);
                // Call tile.Reveal(adjacentMines)
                tile.Reveal(adjacentMines);
                // If adjacentMines > 0
                if (adjacentMines > 0)
                {
                    // Return
                    return;
                }
                // Set visited[x, y] = true
                visited[x, y] = true;
                // Call FFuncover(x - 1, y, visited)
                FFuncover(x - 1, y, visited);
                // Call FFuncover(x + 1, y, visited)
                FFuncover(x + 1, y, visited);
                // Call FFuncover(x, y - 1, visited)
                FFuncover(x, y - 1, visited);
                // Call FFuncover(x, y + 1, visited)
                FFuncover(x, y + 1, visited);
            }
        }

        // Uncovers all mines that are in the grid
        public void UncoverMines(int mineState)
        {
            // For x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // For y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // Let currentTile = tiles[x, y]
                    Tile currentTile = tiles[x, y];
                    // If currentTile isMine
                    if (currentTile.isMine)
                    {
                        // Let adjacentMines = GetAdjecentMineCountAt(currentTile)
                        int adjacentMines = GetAdjacentMineCountAt(currentTile);
                        // Call currentTile.Reveal(adjacentMines, mineState)
                        currentTile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        // Detects if there are no more empty tiles in the game
        bool NoMoreEmptyTiles()
        {
            // Let emptyTileCount = 0
            int emptyTileCount = 0;
            // For x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // For y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // Let currentTile = tiles[x, y]
                    Tile currentTile = tiles[x, y];
                    // If !currentTile.isRevealed AND !currentTile.isMine
                    if (!currentTile.isRevealed && !currentTile.isMine)
                    {
                        //Set emptyTileCount = emptyTileCount + 1
                        emptyTileCount++;
                    }
                }
            }
            // Return emptyTileCount
            return emptyTileCount == 0;
        }

        // Takes in a tile selected by the user in some way to reveal it
        public void SelectTile(Tile selectedTile)
        {
            // Let adjacentMines = GetAdjacentMineCountAt(selectedTile)
            int adjacentMines = GetAdjacentMineCountAt(selectedTile);
            // Call selectedTile.Reveal(adjacentMines)
            selectedTile.Reveal(adjacentMines);
            // If selectedTile isMine
            if (selectedTile.isMine)
            {
                // Call UncoverMines(0)
                UncoverMines(0);
                // [Extra] Perform Game Over logic
            }
            // ElseIf adjacentMines == 0
            else if (adjacentMines == 0)
            {
                // Let x = selectedTile.x
                int x = selectedTile.x;
                // Let y = selectedTile.y
                int y = selectedTile.y;
                // Call FFuncover(x, y, new bool[width, height]
                FFuncover(x, y, new bool[width, height]);
            }
            // If NoMoreEmptyTiles()
            if (NoMoreEmptyTiles())
            {
                //Call UncoverMines(1)
                UncoverMines(1);
                // [Extra] Perform Win logic
            }
        }

        void FixedUpdate()
        {
            // IF mouse down (0)
            if (Input.GetMouseButtonDown(0))
            {
                // LET ray = Camera main ScreenPointToRay(mouse Position)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // LET hit = Raycast(ray origin, ray direction)
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // IF hit.collider != null
                if (hit.collider != null)
                {
                    // LET tile = hit collider's Tile component
                    Tile tile = hit.collider.GetComponent<Tile>();
                    // IF tile != null
                    // Call SelectedTile(hitTile)
                    if (tile != null)
                    {
                        // LET adjacentMines = GetAdjacentMineCountAt(tile)
                        int adjacentMines = GetAdjacentMineCountAt(tile);
                        // CALL tile.Reveal(adjacentMines)
                        tile.Reveal(adjacentMines);
                    }
                }
            }
        }

        private void Update()
        {
            //If Mouse Button 0 is down
            if (Input.GetMouseButtonDown(0))
            {
                //Let ray = Ray from Camera using Input.mousePosition
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Let hit = Physics2D RayCast (ray.origin, ray.direction)
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                //If hit's collider != null
                if (hit.collider != null)
                {
                    //Let hitTile = hit collider's Tile component
                    Tile hitTile = hit.collider.GetComponent<Tile>();

                    //If hitTile != null
                    if (hitTile != null)
                    {
                        //Call SelectTile(hitTile)
                        SelectTile(hitTile);
                    }
                }
            }
        }
    }
}