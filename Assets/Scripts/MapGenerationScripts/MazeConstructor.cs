using UnityEngine;
using UnityEngine.Tilemaps;
public class MazeConstructor : MonoBehaviour
{ 
    private Vector3Int startPoint;
    private Vector3Int exitPoint;
    private Vector3Int crabStart;
    private MazeDataGenerator dataGenerator;
    // References to the materials to be used.
    public Tilemap tilemap;
    public TileBase exitHole;
    public TileBase groundTile;
    public TileBase wallTile;
    // Initialize a 2d int array. 
    // private set so the maze cannot be accessed from outside
    public int[,] data { get; private set; }
    void Awake() 
    {
        dataGenerator = new MazeDataGenerator();
        // 1 indicates the cell will have a wall
        // 0 indicates an empty cell
        data = new int[,]
        {
            {1,1,1},
            {1,0,1},
            {1,1,1}
        };
    }
    public void GenerateMaze(int Rows, int Cols)
    {
        //Ideally keep the rows / columns at odd numbers.
        // just makes the maze work better
        if(Rows %2 == 0 && Cols %2 == 0) 
        {
            Debug.LogError("Odd numbers are better for maze gen");
        }

        //Get the maze datamap (2d grid 0's and 1's)
        data = dataGenerator.FromDimensions(Rows,Cols);
        
        //Render the physical map
        /*
        @Johnathan - the 2d aray 'data' is probably what you are looking to transfer between clients.
        How you do that? I have no clue. But, I think you do and I have full confidence. 
        You're probably gonna wanna make sure you're rendering the same map, so if each client is running this exact method
        shit's gonna get fucked up because it's gonna be generating a bunch of different maps. So i'd take the line below, toss that
        bad boy in a different method and maybe use this method on start or something. Hope this makes some form of sense :) 
        */
        tilemap = dataGenerator.RenderMap(data,tilemap,groundTile,wallTile);
        
        //Generate the exit point and start points of the players and hunter 
        exitPoint = getExitPoint();
        startPoint = getPlayerSpawn();
        crabStart = getCrabSpawn();
    }
    
    /*
    All the spawn point generation is handled below
    One improvement i'd suggest that I didn't really have time for, was ensuring there was a 
    path out of the spawn point, it should be fairly simple to implement - I got started
    */
    
    
    //spawns players in the top left
    private Vector3Int getPlayerSpawn()
    {
        int[,] maze = data;
        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);
        for(int r = 0; r <= rowMax; r++)
        {
            for(int c = 0; c <= colMax; c++)
            {
                if(maze[r,c]==0)
                {
                    //return player spawn
                    return new Vector3Int(r,c,0);
                }
            }    
        }

        // This should never be reached. if it is, something is mega-broken and 
        // I will cry myself to sleep
        return new Vector3Int(0,0,0);
    }
    //spawns the 'hunter' (crab) near the center of the maze
    private Vector3Int getCrabSpawn()
    {
        int[,] maze = data;
        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);
        for(int r = rowMax/2; r <= 0; r++)
        {
            for(int c = colMax/2; c >= 0; c++)
            {
                if(maze[r,c]==0)
                {
                    //return crab spawn
                    return new Vector3Int(r,c,0);
                }
            }
        }
        // This should never be reached. if it is, something is mega-broken and 
        // I will cry myself to sleep
        return new Vector3Int(0,0,0);
    }
    //places exit point in bottom right
    private Vector3Int getExitPoint()
    {
        int[,] maze = data;
        int rowMax = maze.GetUpperBound(0);
        int colMax = maze.GetUpperBound(1);
        //looping from top to bottom, right to left 
        //to find an open spawn point
        for(int r = rowMax; r >= 0; r--)
        {
            for(int c = colMax; c >= 0; c--)
            {
                if(maze[r,c]==0)
                {
                    tilemap.SetTile(new Vector3Int(r,c,0),exitHole);
                    return new Vector3Int(r,c,0);
                }
            }
        }
        //if this condition is met, something went seriously wrong and i don't know what to do about that
        return new Vector3Int(rowMax,colMax,0);
    }


    //Incomplete method atm. Yall should update this so that things can spawn in the right spots
    private bool pathOut(int[,] maze, int r, int c)
    {
        return true;
    }
}
