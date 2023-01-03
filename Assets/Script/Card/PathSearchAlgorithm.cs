//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathSearchAlgorithm : MonoBehaviour
//{
//    private struct NodeInfo
//    {
//        public Vector2Int parent;
//        // f = g + h
//        public double f, g, h;
//    };
//    public static List<Vector2> AStar(Vector2 src, Vector2 dst)
//    {
//        List<Vector2> path = new List<Vector2>();
//        return path;
//    }

//    private bool IsValid(Vector2 pos)
//    {
//        return BattleData.enviromentData[pos] ==0;
//    }

//    private bool IsDestination(Vector2 src, Vector2 dst)
//    {
//        if (src== dst)
//            return (true);
//        else
//            return (false);
//    }

//    private float CalculateHValue(Vector2 src, Vector2 dst)
//    {
//        // Return using the distance formula
//        return ((float)Mathf.Sqrt(
//            (src.x - dst.x) * (src.x - dst.x)
//            + (src.y - dst.y) * (src.y - dst.y)));
//    }

//    private List<Vector2> TracePath(Dictionary<Vector2,NodeInfo> nodeInfos, Vector2Int dest)
//    {
//        Debug.Log("\nThe Path is ");
//        int row = dest.x;
//        int col = dest.y;

//        List<Vector2> path=new List<Vector2>();

//        while (!(nodeInfos[new Vector2(row,col)].parent.x == row
//                 && nodeInfos[new Vector2(row, col)].parent.y == col))
//        {
//            path.Add(new Vector2(row,col));
//            row = nodeInfos[new Vector2(row, col)].parent.x;
//            col = nodeInfos[new Vector2(row, col)].parent.y;
//        }

//        path.Add(new Vector2(row, col));
//        path.Reverse();

//        for(int i=0;i< path.Count; i++)
//        {
//            Debug.Log(" -> " + path[i].x+","+ path[i].y);
//        }

//        return path;
//    }

//    private void aStarSearch(int grid[][COL], Vector2Int src, Vector2Int dest)
//    {


//        // Create a closed list and initialise it to false which
//        // means that no cell has been included yet This closed
//        // list is implemented as a boolean 2D array
//        Dictionary<Vector2, bool> closedList=new Dictionary<Vector2, bool>();

//        memset(closedList, false, sizeof(closedList));

//        // Declare a 2D array of structure to hold the details
//        // of that cell
//        Dictionary<Vector2, NodeInfo> cellDetails = new Dictionary<Vector2, NodeInfo>();


//        for (int i = 0; i < BattleData.enviromentData.Count; i++)
//        {
//            NodeInfo info = new NodeInfo();
//            info.f = 999;
//            info.g = 999;
//            info.h = 999;
//            info.parent = new Vector2Int(-1, -1);
//        }

//        // Initialising the parameters of the starting node
//        NodeInfo startNodeInfo = new NodeInfo();
//        startNodeInfo.f = 0.0;
//        startNodeInfo.g = 0.0;
//        startNodeInfo.h = 0.0;
//        startNodeInfo.parent = src;

//        /*
//         Create an open list having information as-
//         <f, <i, j>>
//         where f = g + h,
//         and i, j are the row and column index of that cell
//         Note that 0 <= i <= ROW-1 & 0 <= j <= COL-1
//         This open list is implemented as a set of pair of
//         pair.*/
//        HashSet<Vector2> openList;

//        // Put the starting cell on the open list and set its
//        // 'f' as 0
//        openList.insert(make_pair(0.0, make_pair(i, j)));

//        // We set this boolean value as false as initially
//        // the destination is not reached.
//        bool foundDest = false;

//        while (openList.Count!=0)
//        {
//            pPair p = *openList.begin();

//            // Remove this vertex from the open list
//            openList.erase(openList.begin());

//            // Add this vertex to the closed list
//            i = p.second.first;
//            j = p.second.second;
//            closedList[i][j] = true;

//            /*
//             Generating all the 4 successor of this cell

//             Cell-->Popped Cell (i, j)
//             N -->  North       (i-1, j)
//             S -->  South       (i+1, j)
//             E -->  East        (i, j+1)
//             W -->  West           (i, j-1)

//            // To store the 'g', 'h' and 'f' of the 8 successors
//            double gNew, hNew, fNew;

//            //----------- 1st Successor (North) ------------

//            // Only process this cell if this is a valid one
//            */
//            if (isValid(i - 1, j) == true)
//            {
//                // If the destination cell is the same as the
//                // current successor
//                if (isDestination(i - 1, j, dest) == true)
//                {
//                    // Set the Parent of the destination cell
//                    cellDetails[i - 1][j].parent_i = i;
//                    cellDetails[i - 1][j].parent_j = j;
//                    printf("The destination cell is found\n");
//                    tracePath(cellDetails, dest);
//                    foundDest = true;
//                    return;
//                }
//                // If the successor is already on the closed
//                // list or if it is blocked, then ignore it.
//                // Else do the following
//                else if (closedList[i - 1][j] == false
//                         && isUnBlocked(grid, i - 1, j)
//                                == true)
//                {
//                    gNew = cellDetails[i][j].g + 1.0;
//                    hNew = calculateHValue(i - 1, j, dest);
//                    fNew = gNew + hNew;

//                    // If it isn��t on the open list, add it to
//                    // the open list. Make the current square
//                    // the parent of this square. Record the
//                    // f, g, and h costs of the square cell
//                    //                OR
//                    // If it is on the open list already, check
//                    // to see if this path to that square is
//                    // better, using 'f' cost as the measure.
//                    if (cellDetails[i - 1][j].f == FLT_MAX
//                        || cellDetails[i - 1][j].f > fNew)
//                    {
//                        openList.insert(make_pair(
//                            fNew, make_pair(i - 1, j)));

//                        // Update the details of this cell
//                        cellDetails[i - 1][j].f = fNew;
//                        cellDetails[i - 1][j].g = gNew;
//                        cellDetails[i - 1][j].h = hNew;
//                        cellDetails[i - 1][j].parent_i = i;
//                        cellDetails[i - 1][j].parent_j = j;
//                    }
//                }
//            }

//            //----------- 2nd Successor (South) ------------

//            // Only process this cell if this is a valid one
//            if (isValid(i + 1, j) == true)
//            {
//                // If the destination cell is the same as the
//                // current successor
//                if (isDestination(i + 1, j, dest) == true)
//                {
//                    // Set the Parent of the destination cell
//                    cellDetails[i + 1][j].parent_i = i;
//                    cellDetails[i + 1][j].parent_j = j;
//                    printf("The destination cell is found\n");
//                    tracePath(cellDetails, dest);
//                    foundDest = true;
//                    return;
//                }
//                // If the successor is already on the closed
//                // list or if it is blocked, then ignore it.
//                // Else do the following
//                else if (closedList[i + 1][j] == false
//                         && isUnBlocked(grid, i + 1, j)
//                                == true)
//                {
//                    gNew = cellDetails[i][j].g + 1.0;
//                    hNew = calculateHValue(i + 1, j, dest);
//                    fNew = gNew + hNew;

//                    // If it isn��t on the open list, add it to
//                    // the open list. Make the current square
//                    // the parent of this square. Record the
//                    // f, g, and h costs of the square cell
//                    //                OR
//                    // If it is on the open list already, check
//                    // to see if this path to that square is
//                    // better, using 'f' cost as the measure.
//                    if (cellDetails[i + 1][j].f == FLT_MAX
//                        || cellDetails[i + 1][j].f > fNew)
//                    {
//                        openList.insert(make_pair(
//                            fNew, make_pair(i + 1, j)));
//                        // Update the details of this cell
//                        cellDetails[i + 1][j].f = fNew;
//                        cellDetails[i + 1][j].g = gNew;
//                        cellDetails[i + 1][j].h = hNew;
//                        cellDetails[i + 1][j].parent_i = i;
//                        cellDetails[i + 1][j].parent_j = j;
//                    }
//                }
//            }

//            //----------- 3rd Successor (East) ------------

//            // Only process this cell if this is a valid one
//            if (isValid(i, j + 1) == true)
//            {
//                // If the destination cell is the same as the
//                // current successor
//                if (isDestination(i, j + 1, dest) == true)
//                {
//                    // Set the Parent of the destination cell
//                    cellDetails[i][j + 1].parent_i = i;
//                    cellDetails[i][j + 1].parent_j = j;
//                    printf("The destination cell is found\n");
//                    tracePath(cellDetails, dest);
//                    foundDest = true;
//                    return;
//                }

//                // If the successor is already on the closed
//                // list or if it is blocked, then ignore it.
//                // Else do the following
//                else if (closedList[i][j + 1] == false
//                         && isUnBlocked(grid, i, j + 1)
//                                == true)
//                {
//                    gNew = cellDetails[i][j].g + 1.0;
//                    hNew = calculateHValue(i, j + 1, dest);
//                    fNew = gNew + hNew;

//                    // If it isn��t on the open list, add it to
//                    // the open list. Make the current square
//                    // the parent of this square. Record the
//                    // f, g, and h costs of the square cell
//                    //                OR
//                    // If it is on the open list already, check
//                    // to see if this path to that square is
//                    // better, using 'f' cost as the measure.
//                    if (cellDetails[i][j + 1].f == FLT_MAX
//                        || cellDetails[i][j + 1].f > fNew)
//                    {
//                        openList.insert(make_pair(
//                            fNew, make_pair(i, j + 1)));

//                        // Update the details of this cell
//                        cellDetails[i][j + 1].f = fNew;
//                        cellDetails[i][j + 1].g = gNew;
//                        cellDetails[i][j + 1].h = hNew;
//                        cellDetails[i][j + 1].parent_i = i;
//                        cellDetails[i][j + 1].parent_j = j;
//                    }
//                }
//            }

//            //----------- 4th Successor (West) ------------

//            // Only process this cell if this is a valid one
//            if (isValid(i, j - 1) == true)
//            {
//                // If the destination cell is the same as the
//                // current successor
//                if (isDestination(i, j - 1, dest) == true)
//                {
//                    // Set the Parent of the destination cell
//                    cellDetails[i][j - 1].parent_i = i;
//                    cellDetails[i][j - 1].parent_j = j;
//                    printf("The destination cell is found\n");
//                    tracePath(cellDetails, dest);
//                    foundDest = true;
//                    return;
//                }

//                // If the successor is already on the closed
//                // list or if it is blocked, then ignore it.
//                // Else do the following
//                else if (closedList[i][j - 1] == false
//                         && isUnBlocked(grid, i, j - 1)
//                                == true)
//                {
//                    gNew = cellDetails[i][j].g + 1.0;
//                    hNew = calculateHValue(i, j - 1, dest);
//                    fNew = gNew + hNew;

//                    // If it isn��t on the open list, add it to
//                    // the open list. Make the current square
//                    // the parent of this square. Record the
//                    // f, g, and h costs of the square cell
//                    //                OR
//                    // If it is on the open list already, check
//                    // to see if this path to that square is
//                    // better, using 'f' cost as the measure.
//                    if (cellDetails[i][j - 1].f == FLT_MAX
//                        || cellDetails[i][j - 1].f > fNew)
//                    {
//                        openList.insert(make_pair(
//                            fNew, make_pair(i, j - 1)));

//                        // Update the details of this cell
//                        cellDetails[i][j - 1].f = fNew;
//                        cellDetails[i][j - 1].g = gNew;
//                        cellDetails[i][j - 1].h = hNew;
//                        cellDetails[i][j - 1].parent_i = i;
//                        cellDetails[i][j - 1].parent_j = j;
//                    }
//                }
//            }
//            if (foundDest == false)
//                printf("Failed to find the Destination Cell\n");

//            return;
//        }

//    }
//}
