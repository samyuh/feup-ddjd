using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    [SerializeField] private int puzzleXLength;
    [SerializeField] private int puzzleZLength;
    [SerializeField] private float puzzleXOffset;
    [SerializeField] private float puzzleZOffset;
    [SerializeField] private float pushStep;
    [SerializeField] private List<List<float>> solution = new List<List<float>>();
    private int numberSpecialCubes;
    private bool solved;
    [SerializeField] public GameObject Door;
    private List<(float cubeX, float cubeZ, float directionX, float directionZ)> moves = new List<(float cubeX, float cubeZ, float directionX, float directionZ)>();
    bool reseting;
    
    // Start is called before the first frame update
    void Start()
    {
        cubes.Clear();
        FindObjectwithTag("SpecialPuzzleCube");
        numberSpecialCubes = cubes.Count;
        FindObjectwithTag("PuzzleCube");
        CreateSolution(transform.position.x, transform.position.z);
        solved = false;
        reseting = false;
        for(int i = 0; i < cubes.Count; i++){
            cubes[i].SendMessage("SetPushStep", pushStep);
        }
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
        (int x, int z) direction = (values.directionX, values.directionZ);

        if(isMoveValid(direction, cubePosition)){
            Debug.Log("MOVE WAS VALID");
            for(int i = 0; i < cubes.Count; i ++){
                if(cubes[i].transform.position.x == cubePosition.x && cubes[i].transform.position.z == cubePosition.z){
                    moves.Add((cubes[i].transform.position.x + direction.x, cubes[i].transform.position.z + direction.z, direction.x, direction.z));
                    for(int j = 0; j < moves.Count; j++){
                        Debug.Log("Cube Position: " + moves[j].cubeX + ", " + moves[j].cubeZ);
                        Debug.Log("Move Direction: " + moves[j].directionX + ", " + moves[j].directionZ);
                    }
                    cubes[i].SendMessage("Move", direction);
                }
            }
        }
    }

    public bool isMoveValid((int x, int z) direction, (float x, float z) blockPosition) {
        (float x, float z) finalPosition = (blockPosition.x + direction.x * pushStep, blockPosition.z + direction.z * pushStep);

        if (blockLeavesPuzzle(direction, (blockPosition.x, blockPosition.z))) {
            Debug.Log("BLOCK LEAVES PUZZLE");
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
        (float x, float z) finalPosition = (blockPosition.x + direction.x * pushStep, blockPosition.z + direction.z * pushStep);

        Debug.Log("Puzzle X: " + (transform.position.x + puzzleXOffset));
        Debug.Log("Puzzle Z: " + (transform.position.z + puzzleZOffset));
        Debug.Log("Cube X: " + finalPosition.x);
        Debug.Log("Cube Z: " + finalPosition.z);

        if (blockPosition.x + direction.x * pushStep < transform.position.x + puzzleXOffset - 0.01 || blockPosition.x + direction.x * pushStep >= puzzleXLength * pushStep + transform.position.x + puzzleXOffset - 0.01 || blockPosition.z + direction.z * pushStep < transform.position.z + puzzleZOffset - 0.01 || blockPosition.z + direction.z * pushStep >= puzzleZLength * pushStep + transform.position.z + puzzleZOffset - 0.01){
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

    void Reset(){
        if(reseting){
            Debug.Log(moves.Count);
            if(moves.Count > 0){
                (float cubeX, float cubeZ, float directionX, float directionZ) move = moves[moves.Count - 1];
                Debug.Log("Move:" + move.cubeX + ", " + move.cubeZ + ", " + move.directionX + ", " + move.directionZ);
                for(int i = 0; i < cubes.Count; i ++){
                    if(cubes[i].transform.position.x == move.cubeX && cubes[i].transform.position.z == move.cubeZ){
                        (int x, int z) direction = ((int)-move.directionX, (int)-move.directionZ);
                        cubes[i].SendMessage("Move", direction);
                        moves.RemoveAt(moves.Count - 1);
                        if(moves.Count == 0){
                            reseting = false;
                        }
                        return;
                    }
                }
            }
        }
    }

    void StartReseting(){
        reseting = true;
        Reset();
    }
}
