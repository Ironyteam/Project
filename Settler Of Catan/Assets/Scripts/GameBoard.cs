using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{

    public GameObject hexPrefab;
    public GameObject hexBoard;

    string[] resources = new string[5]
    {
       "Wood", "Ore", "Grain", "Wool", "Brick"
    };

    const int WIDTH = HexTemplate.WIDTH;
    const int HEIGHT = HexTemplate.HEIGHT;

    HexTemplate template = new HexTemplate();

    float zOffset = .751f;
    float xOffset = .866f;

    // Use this for initialization
    //public void SpawnBoard(Hex[,] hex, int WIDTH, int HEIGHT)
    //  {
    void Start()
    {
        FileHandler reader = new FileHandler();
        template = BoardManager.template;
        // reader.saveMap(template);

        for (int x = 0; x < WIDTH; x++)
        {
            for (int z = 0; z < HEIGHT; z++)
            {
                hexPrefab.name = "hex " + x + "," + z;

                float xPos = x * xOffset;
                if (z % 2 == 1 || z % 2 == -1)
                {
                    xPos += (xOffset * .5f);
                }



                switch (template.hex[x, z].resource)
                {
                    case 0:
                        template.hex[x, z].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0.1f, z * zOffset), Quaternion.identity); ;
                        template.hex[x, z].hex_go.GetComponentInChildren<Renderer>().material.color = Color.black;
                        break;
                    case 1:
                        template.hex[x, z].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0.1f, z * zOffset), Quaternion.identity); ;
                        template.hex[x, z].hex_go.GetComponentInChildren<Renderer>().material.color = Color.grey;
                        break;
                    case 2:
                        template.hex[x, z].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0.1f, z * zOffset), Quaternion.identity); ;
                        template.hex[x, z].hex_go.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                        break;
                    case 3:
                        template.hex[x, z].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0.1f, z * zOffset), Quaternion.identity); ;
                        template.hex[x, z].hex_go.GetComponentInChildren<Renderer>().material.color = Color.white;
                        break;
                    case 4:
                        template.hex[x, z].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0.1f, z * zOffset), Quaternion.identity); ;
                        template.hex[x, z].hex_go.GetComponentInChildren<Renderer>().material.color = Color.red;
                        break;
                    default:
                        template.hex[x, z].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0.0f, z * zOffset), Quaternion.identity); ;
                        template.hex[x, z].hex_go.GetComponentInChildren<Renderer>().material.color = Color.blue;
                        break;
                }
                template.hex[x, z].hex_go.name = x + "," + z;
            }
        }
    }
}
