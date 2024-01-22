using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    int[] possibleNoOfRooms = { 1, 2, 3, 4 };
    int[] probabilityDistribution = { 100, 0, 0, 0};
    List<int> weightedPossibleNoOfRooms = new List<int>();

    public int childRooms;
    
    void Start()
    {
        WeightArray();
        childRooms = GetRandomIndex(weightedPossibleNoOfRooms);

    }
    private void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            childRooms = GetRandomIndex(weightedPossibleNoOfRooms);
        }
    }

    static int GetRandomIndex(List<int> weightedPossibleNoOfRooms)
    {
        int childRooms;
        int randomIndex = Random.Range(0, weightedPossibleNoOfRooms.Count);
        childRooms = weightedPossibleNoOfRooms[randomIndex];
        Debug.Log(childRooms);
        return childRooms;
    }
    void WeightArray()
    {
        for (int i = 0; i < possibleNoOfRooms.Length; i++)
        {
            switch (i)
            {
                //Depending on the index value, adds the index of the possible number of rooms to the list the corresponding to amount of times as stated by the probability distribution
                case 0:
                    for (int x = 0; x < probabilityDistribution[0]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
                case 1:
                    for (int x = 0; x < probabilityDistribution[1]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
                case 2:
                    for (int x = 0; x < probabilityDistribution[2]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
                case 3:
                    for (int x = 0; x < probabilityDistribution[3]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
            }
        }
    }
}
