using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData dataStuff;

    private List<IGameData> dataPersistenceObjects;
    private FileDataManager dataHandler;


    /// <summary>
    /// To load anything using a button use the line ---------> DataManager.instance."Function that needs to be prefromed"();
    /// </summary>

    public static DataManager instance { get; private set; }

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("Found more than one Data Manager in the scene");

        }
        instance = this;


    }

    private void Start()
    {
        this.dataHandler = new FileDataManager(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        loadGame();

    }
    public void newGame()
    {
        this.dataStuff = new GameData();
        Debug.Log("New Game Ran");
    }

    public void loadGame()
    {

        this.dataStuff = dataHandler.Load();

        if (this.dataStuff == null)
        {
            Debug.Log("Loading....... ");
            newGame();
        }


        foreach (IGameData dataObject in dataPersistenceObjects)
        {
            dataObject.LoadData(dataStuff);

        }

        Debug.Log("Loaded Coins: " + dataStuff.CoinsCount);
        //Debug.Log("Load Mouse sens is: " + dataStuff.MouseSens);
        //Debug.Log("Load Time is: " + dataStuff.GameTime);
    }

    public void saveGame()
    {

        foreach (IGameData dataObject in dataPersistenceObjects)
        {
            dataObject.SaveData(ref dataStuff);

        }

        // Debug.Log("Save Mouse sens is: " + dataStuff.MouseSens);
         Debug.Log("Saved Coins: " + dataStuff.CoinsCount);
        dataHandler.Save(dataStuff);

    }


    private void OnApplicationQuit()
    {
        saveGame();

    }

    private List<IGameData> FindAllDataPersistenceObjects()
    {
        IEnumerable<IGameData> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IGameData>();


        return new List<IGameData>(dataPersistenceObjects);
    }


}


