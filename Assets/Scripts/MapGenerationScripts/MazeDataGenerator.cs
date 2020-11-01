using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MazeDataGenerator
{
    //determines whether a space is empty or not
    public float placementThreshhold;

    public MazeDataGenerator()
    {
        placementThreshhold = .1f;
    }
    public int[,] FromDimensions(int Rows, int Cols)
    {
        /*
        Algorithm: 
        1. Iterate over *every other* space in the grid, not each space
        2. Place a wall
        3. Choose an random adjacent space to place a wall
        4. Also a chance that the space can be "skipped" to provide more variation in the maze
        */
        //Initialize maze 2d array
        int[,] maze = new int[Rows,Cols];
        
        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);
        
        //for every row in the maze
        for(int r=0;r<=rowMax;r++)
        {
            //for every column
            for(int c=0;c<=colMax;c++)
            {
                if(r == 0 || c == 0 || r == rowMax || c == colMax)
                {
                    /* 
                        if the location of the grid we are on lies on the outer part of the maze
                        such as is shown in the grid below (outer = o, inner = i)
                        if on outer edge of maze, we want to place a wall so all 
                        tiles on the grid below marked with an o would have a wall
                        o | o | o | o | o
                        o | i | i | i | o
                        o | i | i | i | o
                        o | i | i | i | o
                        o | o | o | o | o
                    */ 
                    maze[r,c] = 1;   
                }
                // 1. coords must be divisible by two in order to iterate over every other space, rather than every space
                else if(r % 2 == 0 && c % 2 == 0)
                {
                    //4. random skip factor. If this doesn't execute, we just skip this tile
                    if(Random.value > placementThreshhold)
                    {
                        //2. Place a wall on this tile 
                        maze[r,c] = 1; 
                        //3. now choose a random adjacent space
                        
                        /* Random space is chosen by generating a random value, 
                        //if that random value is less than .5, set it to 0. Otherwise,
                        //generate a second random value, if less than .5, set it as -1, else 1
                        //Grid cases (o is original tile. n is adjacent tile. x is surrounding tiles)
                        //Original case: 
                        // | x | x | x |
                        // | x | o | x |
                        // | x | x | x |
                        //adjRow = 0, adjCol = 1
                        // | x | x | x |
                        // | x | o | n |
                        // | x | x | x |
                        //adjRow = 1, adjCol = 1
                        // | x | x | x |
                        // | x | o | x |
                        // | x | x | n |
                        //adjRow = -1, adjCol = 1
                        // | x | x | n |
                        // | x | o | x |
                        // | x | x | x |
                        //adjRow = 1, adjCol = 0
                        // | x | x | x |
                        // | x | o | x |
                        // | x | n | x |
                        //adjRow = -1, adjCol = 0
                        // | x | n | x |
                        // | x | o | x |
                        // | x | x | x |
                        //adjRow = 1, adjCol = -1
                        // | x | x | x |
                        // | x | o | x |
                        // | n | x | x |
                        //adjRow = -1, adjCol = -1
                        // | n | x | x |
                        // | x | o | x |
                        // | x | x | x |
                        //adjRow = 0, adjCol = -1
                        // | x | x | x |
                        // | n | o | x |
                        // | x | x | x |
                        */
                        int adjRow = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int adjCol = adjRow != 0 ? 0 :(Random.value < .5 ? -1 : 1);
                        
                        maze[r+adjRow,c+adjCol] = 1;
                    }
                }
            }
        }
        return maze;
    }
    public Tilemap RenderMap(int[,] dataMap, Tilemap tileMap, TileBase groundTile, TileBase wallTile)
    {
        //clear current tilemap to prevent tilemap overlap doing funky chicken stuff
        tileMap.ClearAllTiles();
        int rowMax = dataMap.GetUpperBound(0);
        int colMax = dataMap.GetUpperBound(1);
        //loop rows
        for(int r = 0; r <= rowMax; r++)
        {
            //loop cols
            for(int c = 0;c <= colMax; c++)
            {
                /*
                check value of the position in the tilemap
                if value = 0, place a floor tile
                otherwise value = 1, so place a wall tile :)
                */
                if(dataMap[r,c] == 0)
                {
                    tileMap.SetTile(new Vector3Int(r,c,0),groundTile);
                }
                else
                {
                    tileMap.SetTile(new Vector3Int(r,c,0),wallTile);
                }
            }
        }
        return tileMap;
    }

}
