using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ObjectiveSystem : MonoBehaviour 
{
    public TextAsset objectiveFile;
    private XmlDocument xmlDoc;
    List<Objective> objectives;
    Objective currentObjective;
	// Use this for initialization
	void Start () {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(objectiveFile.text);
        //Debug.Log(xmlDoc.FirstChild.InnerText);
        //TraverseNodes(xmlDoc.FirstChild);
        SetObjectives(xmlDoc);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void TraverseNodes(XmlNode curr)
    {
        if (curr.HasChildNodes)
            TraverseNodes(curr.FirstChild);
        else
        {
            Debug.Log(curr.InnerText);
            while ((curr = curr.NextSibling) != null)
            {
                Debug.Log(curr.InnerText);
                if (curr.HasChildNodes)
                    TraverseNodes(curr.FirstChild);
            }
        }

    }
    private void SetObjectives(XmlDocument doc)
    {
        foreach (XmlNode node in doc.SelectNodes("objectives/objective"))
        {
            string title = node.SelectSingleNode("title").InnerText;
            string description = node.SelectSingleNode("description").InnerText;
            objectives.Add(new Objective(title, description));
        }
    }

    public Objective CurrentObjective { get { return currentObjective; } }
    public Objective CompleteObjective(string id)
    {

        return null;
    }
}

public class Objective
{
    private string title, description;
    private bool completed;

    public Objective(string titleP, string descriptionP)
    {
        this.title = titleP;
        this.description = descriptionP;
        this.completed = false;
    }
    public Objective(string titleP, string descriptionP, bool completedP)
    {
        this.title = titleP;
        this.description = descriptionP;
        this.completed = completedP;
    }
}