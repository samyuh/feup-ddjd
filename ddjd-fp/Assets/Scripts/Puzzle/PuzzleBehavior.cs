using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    [SerializeField] private int puzzleXLength;
    [SerializeField] private int puzzleZLength;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectwithTag("PuzzleCube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EvaluateMove((int cubeX, int cubeZ, int directionX, int directionZ) values){
        (int x, int z) cubePosition = (values.cubeX, values.cubeZ);
        (int x, int z) direction = (values.directionX, values.directionZ);

        if(isMoveValid(direction, cubePosition)){
            for(int i = 0; i < cubes.Count; i ++){
                if(cubes[i].transform.position.x == cubePosition.x && cubes[i].transform.position.z == cubePosition.z){
                    cubes[i].SendMessage("Move", direction);
                }
            }
        }
    }

    public bool isMoveValid((int x, int z) direction, (int x, int z) blockPosition) {
        (int x, int z) finalPosition = (blockPosition.x + direction.x, blockPosition.z + direction.z);

        if (blockLeavesPuzzle(direction, (blockPosition.x, blockPosition.z - 10))) {
            return false;
        }

        else{
            for(int i = 0; i < cubes.Count; i ++){
                if(cubes[i].transform.position.x == finalPosition.x && cubes[i].transform.position.z == finalPosition.z){
                    return false;
                }
            }
        }

        return true;
    }

    public bool blockLeavesPuzzle((int x, int z) direction, (int x, int z) blockPosition){
        if (blockPosition.x + direction.x < 0 || blockPosition.x + direction.x >= puzzleXLength || blockPosition.z + direction.z < 0 || blockPosition.z + direction.z >= puzzleXLength){
            return true;
        }

        return false;
    }

    public void FindObjectwithTag(string _tag)
    {
        cubes.Clear();
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
}
