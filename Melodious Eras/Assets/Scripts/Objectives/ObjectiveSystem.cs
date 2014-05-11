using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ObjectiveSystem : MonoBehaviour
{
    public TextAsset objectiveFile;
    public LinkedList<Objective> objectives;
    private XmlDocument xmlDoc;

    private LinkedListNode<Objective> currentObjectiveNode;
    private Objective currentObjective;

    public Objective CurrentObjective { get { return currentObjectiveNode.Value; } }


    // Use this for initialization
    void Start()
    {
        objectives = new LinkedList<Objective>();
        currentObjectiveNode = objectives.First;
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
                currentObjectiveNode = objectives.Last;
            }
        }
    }

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
    public Objective CompleteCurrentMainObjective()
    {
        currentObjectiveNode = currentObjectiveNode.Next;
        return null;
    }
}

