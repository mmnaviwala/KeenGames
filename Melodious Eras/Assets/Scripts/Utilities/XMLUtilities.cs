using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public static class XMLUtilities
{
    public static string currentDirectory;
    static string savePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments); //Windows location
                          //Will need different defaults for each platform
                          //May be necessary to use PlayerPrefs for consoles

    public static void SaveGame()
    {
        if (Directory.Exists(savePath))
        {
            //save game here
        }
    }

    public static void LoadGame(string saveFile)
    {
 
    }

    /// <summary>
    /// Gets all the relevant text for documents, computer terminals, etc in the level. Should be called in GameController.Awake() or Start()
    /// <para></para>
    /// </summary>
    /// <param name="levelName"></param>
    public static void GetTextForLevel(string levelName)
    {
 
    }
    
    public static void Test()
    {
        currentDirectory = Directory.GetCurrentDirectory();
        Debug.Log(currentDirectory);
        Debug.Log(savePath);

    }
}
