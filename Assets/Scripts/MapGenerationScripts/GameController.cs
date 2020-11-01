using UnityEngine;
// Ensure MazeConstructor component is added. 
// If it's not this whole dang mess will fall apart
[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    public int rows;
    public int cols;
    
    private MazeConstructor generator;
    void Start()
    {
        //get the MazeConstructor (which we know we have because of the requirecomponent tag)
        generator = GetComponent<MazeConstructor>();
        generator.GenerateMaze(rows, cols);
    }
}
