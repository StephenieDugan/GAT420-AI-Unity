using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject wall;
    public GameObject player;
    public NavMeshSurface surface;

    private bool playerSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();

        surface.BuildNavMesh();
    }

   void GenerateLevel()
    {
        for(int i = 0; i <= width; i+=2) 
        {
            for (int j = 0; j <= height; j+=2)
            {
                //place wall?
                if(Random.value > 0.7f)
                {
                    Vector3 pos = new Vector3(i - width / 2f, 1f, j - height / 2f);
                    Instantiate(wall, pos, Quaternion.identity,transform);
                }
                else if(!playerSpawned)
                {
                    Vector3 pos = new Vector3(i - width / 2f, 1.25f, j - height / 2f);
                    Instantiate(player, pos, Quaternion.identity);
                    playerSpawned = true;
                }
            }
        }
    }
}
