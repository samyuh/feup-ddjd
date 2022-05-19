using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{
    [SerializeField] private List<List<int>> grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isMoveValid((int, int) direction, int id) {
        (int, int) blockPosition = findInGrid(id);
        (int, int) finalPosition = (blockPosition[0] + direction[0], blockPosition[1] + direction[1]);

        if (!isInGrid(finalPosition)) {
            return false;
        }

        else if (grid[finalPosition[0]][finalPosition[1]] != -1) {
            return false;
        }

        return true;
    }

    private (int, int) findInGrid(int blockID) {
        for (int i = 0; i < grid.Count; i++) {
            for (int j = 0; j < grid[i].Count; j++) {
                if (grid[i][j] == blockID) {
                    return (i, j);
                }
            }
        }
        return null;
    }

    private bool isInGrid((int, int) position) {
        return (position[0] < grid.Count) && (position[1] < grid[0].Count);
    }
}
