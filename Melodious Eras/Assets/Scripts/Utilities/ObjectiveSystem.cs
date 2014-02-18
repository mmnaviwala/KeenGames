using UnityEngine;
using System.Collections;
using System.Xml;

public class ObjectiveSystem : MonoBehaviour 
{
    public TextAsset objectiveFile;
    private XmlDocument xmlDoc;
	// Use this for initialization
	void Start () {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(objectiveFile.text);
        Debug.Log(xmlDoc.FirstChild.InnerText);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
