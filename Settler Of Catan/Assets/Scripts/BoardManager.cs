using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

// Consider changing z to y because we are working in 2d
public class BoardManager : MonoBehaviour
{
    public GameObject hexPrefab;

    public int current_hex_x = 0;
    public int current_hex_y = 0;

    public Canvas boardDecisionCanvas;
    public Canvas boardCreationCanvas;
    public Canvas boardSelctionCanvasMOD; // The board selection canvas for MODifying a board
    public Canvas boardSelctionCanvasPG;  // The board selection canvas for Pre Game selection
    public Canvas HexCanvas;
    public InputField mapNameField;
    public Text mapNameTextMOD;           // Text box displaying the map names IN MODIFY BOARD SELCTION MENU
    public Text mapNameTextPG;            // Text box displaying the map names IN PRE GAME SELECTION MENU

    public const string DefaultMapsPath = FileHandler.DefaultMapsPath;
    public const string SavedMapsPath = FileHandler.SavedMapsPath;

    private int savedMapsStartindex;
    private int board_index;

    public static bool startingGame = false;

    const int WIDTH = HexTemplate.WIDTH;
    const int HEIGHT = HexTemplate.HEIGHT;

    const int LEFT = -1;
    const int RIGHT = -2;

    private string[] maps;

    const int ALL_BOARDS = 1;
    const int DEFAULT_BOARDS = 2;
    const int SAVED_BOARDS = 3;

    public static HexTemplate template = new HexTemplate();

    float zOffset = 12f;
    float xOffset = 14f;

    float initial_x = 240;
    float initial_y = 50;

    void Awake()
    {
        boardCreationCanvas.enabled = false;
        boardSelctionCanvasPG.enabled = false;
        boardSelctionCanvasMOD.enabled = false;
        boardDecisionCanvas.enabled = true;
        if (startingGame == true)
        {
            preGameBoardSelectOn();
            startingGame = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);

            if (didHit)
            {
                GameObject ourHitObject = rhInfo.collider.transform.gameObject;

                current_hex_x = ourHitObject.GetComponent<HexData>().x_index;
                current_hex_y = ourHitObject.GetComponent<HexData>().y_index;
            }
        }
    }

    public void CreateNewBoard()
    {
        boardDecisionCanvas.enabled = false;
        boardCreationCanvas.enabled = true;
        SpawnBoard(null);
    }

    public void ModifyMap()
    {
        boardSelctionCanvasMOD.enabled = false;
        boardCreationCanvas.enabled = true;
        FileHandler reader = new FileHandler();
        if (board_index < savedMapsStartindex)
            template = reader.retrieveMap(DefaultMapsPath + "/" + maps[board_index] + ".txt");
        else
            template = reader.retrieveMap(SavedMapsPath + "/" + maps[board_index] + ".txt");
        SpawnBoard(template);
    }

    public void changeHex(int resourceNum)
    {
        template.hex[current_hex_x, current_hex_y].setResource(resourceNum);

        switch (resourceNum)
        {
            case 0:
                template.hex[current_hex_x, current_hex_y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.black;
                break;
            case 1:
                template.hex[current_hex_x, current_hex_y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.grey;
                break;
            case 2:
                template.hex[current_hex_x, current_hex_y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                break;
            case 3:
                template.hex[current_hex_x, current_hex_y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.white;
                break;
            case 4:
                template.hex[current_hex_x, current_hex_y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.red;
                break;
        }
    }

    public void changeDiceNumber(int diceNum)
    {
        template.hex[current_hex_x, current_hex_y].setDiceNum(diceNum);
    }

    public void ChangeDisplayedMap(int desiredIndex)
    {
        if (desiredIndex == RIGHT && board_index < (maps.Length - 1))
            board_index += 1;
        else if (desiredIndex == LEFT && board_index > 0)
            board_index -= 1;
        else if (desiredIndex > 0 && desiredIndex < maps.Length)
            board_index = desiredIndex;

        if (boardSelctionCanvasPG.enabled)
           mapNameTextPG.text = maps[board_index];
        else
            mapNameTextMOD.text = maps[board_index];
    }

    public void SaveBoard()
    {
        string mapName;

        mapName = mapNameField.text;

        FileHandler writer = new FileHandler();
        writer.saveMap(template, mapName, "Bob");                    // Get user name for creator parameter

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Destroy(template.hex[x, y].hex_go);
            }
        }

        boardCreationCanvas.enabled = false;
        boardDecisionCanvas.enabled = true;
    }

    public void preGameBoardSelectOn()
    {
        boardDecisionCanvas.enabled = false;
        boardSelctionCanvasPG.enabled = true;
        displayMaps();
    }

    public void ModifyBoardSelectOn()
    {
        boardDecisionCanvas.enabled = false;
        boardSelctionCanvasMOD.enabled = true;
        displayMaps();
    }

    public void displayMaps()
    {
        FileHandler reader = new FileHandler();

        maps = reader.getAllMaps(out savedMapsStartindex).ToArray();
        board_index = 0;

        if (boardSelctionCanvasPG.enabled)
           mapNameTextPG.text = maps[board_index];
        else
            mapNameTextMOD.text = maps[board_index];
    }

    public void startGame()
    {
        boardSelctionCanvasPG.enabled = false;
        FileHandler reader = new FileHandler();
        if (board_index < savedMapsStartindex)
            template = reader.retrieveMap(DefaultMapsPath + "/" + maps[board_index] + ".txt");
        else
            template = reader.retrieveMap(SavedMapsPath + "/" + maps[board_index] + ".txt");
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void SpawnBoard(HexTemplate template)
    {
        if (template == null)
        {
            template = new HexTemplate();
            bool[] portSides = { false, false, false, false, false, false };

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    template.hex[x, y] = new Hex(-1, 2, portSides); // Be aware that these values coul have change
                }
            }
        }

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                hexPrefab.name = "hex " + x + "," + y;

                float xPos = x * xOffset + initial_x;
                if (y % 2 == 1 || y % 2 == -1)
                {
                    xPos += (xOffset * .5f);
                }

                switch (template.hex[x, y].resource)
                {
                    case 0:
                        template.hex[x, y].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, y * zOffset + initial_y, 0), Quaternion.identity); ;
                        template.hex[x, y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.black;
                        break;
                    case 1:
                        template.hex[x, y].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, y * zOffset + initial_y, 0), Quaternion.identity); ;
                        template.hex[x, y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.grey;
                        break;
                    case 2:
                        template.hex[x, y].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, y * zOffset + initial_y, 0), Quaternion.identity); ;
                        template.hex[x, y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.yellow;
                        break;
                    case 3:
                        template.hex[x, y].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, y * zOffset + initial_y, 0), Quaternion.identity); ;
                        template.hex[x, y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.white;
                        break;
                    case 4:
                        template.hex[x, y].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, y * zOffset + initial_y, 0), Quaternion.identity); ;
                        template.hex[x, y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.red;
                        break;
                    default:
                        template.hex[x, y].hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, y * zOffset + initial_y, 0), Quaternion.identity); ;
                        template.hex[x, y].hex_go.GetComponentInChildren<Renderer>().material.color = Color.blue;
                        break;
                }
                template.hex[x, y].hex_go.name = x + "," + y;
                template.hex[x, y].hex_go.transform.SetParent(HexCanvas.transform);
                //template.hex[x, y].hex_go.AddComponent<Canvas>();

                template.hex[x, y].hex_go.AddComponent<HexData>();
                template.hex[x, y].hex_go.GetComponent<HexData>().x_index = x;
                template.hex[x, y].hex_go.GetComponent<HexData>().y_index = y;
            }
        }
        BoardManager.template = template;
    }
}

