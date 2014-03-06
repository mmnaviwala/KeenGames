using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ObjectiveSystem : MonoBehaviour
{
    public TextAsset objectiveFile;
    private XmlDocument xmlDoc;
    LinkedList<Objective> objectives;
    Objective currentObjective;
    // Use this for initialization
    void Start()
    {
        objectives = new LinkedList<Objective>();
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(objectiveFile.text);
        SetObjectives(xmlDoc);
    }

    private void SetObjectives(XmlDocument doc)
    {
        foreach (XmlNode node in doc.SelectNodes("objectives/objective"))
        {
            string title = node.SelectSingleNode("title").InnerText;
            string description = node.SelectSingleNode("description").InnerText;
            Objective newObjective = new Objective(title, description);
            objectives.AddLast(newObjective);

            //sets the current objective to the first uncompleted one (when loading a save file)
            if (currentObjective == null && node.Attributes["completed"].Value == "false")
            {
                currentObjective = newObjective;
                Debug.Log("Current objective: " + currentObjective.ToString());
            }
            Debug.Log(objectives.Last.Value);
        }
    }

    public Objective CurrentObjective { get { return currentObjective; } }
    /// <summary>
    /// Completes the specified objective in this level and moves on to the next, if any.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Objective CompleteObjective(string id)
    {

        return null;
    }
    /// <summary>
    /// Completes current MAIN storyline objective in this level and moves on to the next, if any
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Objective CompleteCurrentMainObjective(string id)
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

    public override string ToString()
    {
        return this.title + '\n' + this.description;
    }
}