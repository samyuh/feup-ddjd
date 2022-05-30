using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    [SerializeField] private int puzzleXLength;
    [SerializeField] private int puzzleZLength;
    [SerializeField] private List<List<float>> solution = new List<List<float>>();
    private int numberSpecialCubes;
    private bool solved;
    [SerializeField] public GameObject Door;
    
    // Start is called before the first frame update
    void Start()
    {
        cubes.Clear();
        FindObjectwithTag("SpecialPuzzleCube");
        numberSpecialCubes = cubes.Count;
        FindObjectwithTag("PuzzleCube");
        CreateSolution(transform.position.x, transform.position.z);
        solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!solved){
            solved = CheckSolution();
            if(solved){
                Door.SendMessage("Open");
            }
        }

        if(solved){
            solved = CheckSolution();
            if(!solved){
                Door.SendMessage("Close");
            }
        }
    }

    public void EvaluateMove((float cubeX, float cubeZ, int directionX, int directionZ) values){
        (float x, float z) cubePosition = (values.cubeX, values.cubeZ);
        Debug.Log("EVALUATE MOVE: " + cubePosition.x + ", " + cubePosition.z);
        (int x, int z) direction = (values.directionX, values.directionZ);

        if(isMoveValid(direction, cubePosition)){
            for(int i = 0; i < cubes.Count; i ++){
                if(cubes[i].transform.position.x == cubePosition.x && cubes[i].transform.position.z == cubePosition.z){
                    cubes[i].SendMessage("Move", direction);
                }
            }
        }
    }

    public bool isMoveValid((int x, int z) direction, (float x, float z) blockPosition) {
        (float x, float z) finalPosition = (blockPosition.x + direction.x, blockPosition.z + direction.z);

        if (blockLeavesPuzzle(direction, (blockPosition.x, blockPosition.z))) {
            return false;
        }

        else{
            for(int i = 0; i < cubes.Count; i ++){
                /*if(cubes[i].transform.position.x == finalPosition.x && cubes[i].transform.position.z == finalPosition.z){
                    return false;
                }*/
                if(Mathf.Abs(cubes[i].transform.position.x - finalPosition.x) < 0.01 && Mathf.Abs(cubes[i].transform.position.z - finalPosition.z) < 0.01){
                    return false;
                }
            }
        }

        return true;
    }

    public bool blockLeavesPuzzle((int x, int z) direction, (float x, float z) blockPosition){
        (float x, float z) finalPosition = (blockPosition.x + direction.x, blockPosition.z + direction.z);
        Debug.Log("Block Position: " + blockPosition.x + ", " + blockPosition.z);
        Debug.Log("Direction: " + direction.x + ", " + direction.z);
        Debug.Log("Puzzle Position: " + transform.position.x + ", " + transform.position.z);
        Debug.Log("Final Position: " + finalPosition.x + ", " + finalPosition.z);
        if (blockPosition.x + direction.x < transform.position.x - 0.01 || blockPosition.x + direction.x >= puzzleXLength + transform.position.x - 0.01 || blockPosition.z + direction.z < transform.position.z - 0.01 || blockPosition.z + direction.z >= puzzleZLength + transform.position.z - 0.01){
            return true;
        }

        return false;
    }

    public void FindObjectwithTag(string _tag)
    {
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                cubes.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }

    void CreateSolution(float puzzleX, float puzzleZ){
        solution.Add(new List<float>{3 + puzzleX,3 + puzzleZ});
        solution.Add(new List<float>{3 + puzzleX,1 + puzzleZ});
        solution.Add(new List<float>{2 + puzzleX,2 + puzzleZ});
        solution.Add(new List<float>{4 + puzzleX,2 + puzzleZ});
        solution.Add(new List<float>{1 + puzzleX,3 + puzzleZ});
        solution.Add(new List<float>{5 + puzzleX,3 + puzzleZ});
        solution.Add(new List<float>{2 + puzzleX,4 + puzzleZ});
        solution.Add(new List<float>{4 + puzzleX,4 + puzzleZ});
    }

    bool CheckSolution(){
        for(int i = 0; i < solution.Count; i++){
            bool ok = false;
            for(int j = 0; j < cubes.Count; j++){
                if(i < numberSpecialCubes){
                    if(cubes[j].tag == "SpecialPuzzleCube" && solution[i][0] == cubes[j].transform.position.x && solution[i][1] == cubes[j].transform.position.z){
                        ok = true;
                        break;
                    }
                }
                else{
                    if(cubes[j].tag == "PuzzleCube" && solution[i][0] == cubes[j].transform.position.x && solution[i][1] == cubes[j].transform.position.z){
                        ok = true;
                        break;
                    }
                }
            }
            if(!ok){
                return false;
            }
        }
        return true;
    }
}
