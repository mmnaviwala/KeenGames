using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Objective
{
    #region variables
    protected string title, description;
    protected bool completed;
    public List<ObjectiveCondition> conditions;

    public string Title { get { return title; } }
    public string Description { get { return description; } }
    public bool Completed { get { return completed; } }
    #endregion

    public Objective(string titleP)
    {
        this.title = titleP;
    }
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

/// <summary>
/// Conditions used in each objective
/// </summary>
public class ObjectiveCondition : MonoBehaviour
{
    public bool completed;
}
/// <summary>
/// Completed once the player reaches a checkpoint
/// </summary>
public class CheckpointObjectiveCondition : ObjectiveCondition
{

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.tag == Tags.PLAYER && other is CapsuleCollider)
        {
            this.completed = true;
        }
    }
}
/// <summary>
/// Completed/incremented when the player picks up quest items
/// </summary>
public class ItemObjectiveCondition : ObjectiveCondition
{
    Item[] questItems;
}

/// <summary>
/// Completed when the player reads a document, email, etc.
/// </summary>
public class DocumentObjectiveCondition : ObjectiveCondition
{
    
}