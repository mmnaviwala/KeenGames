using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//                                      Y        Y           X         X          Z          Z
public enum FaceRestriction { None = 0, Top = 1, Bottom = 2, Left = 3, Right = 4, Front = 5, Back = 6 };

public class DepthChanging : MonoBehaviour
{
    public float z_depth = 0f;
    public float speed = 5f;
    public int destroyAfterTriggers = -1;
    public FaceRestriction faceRestriction = FaceRestriction.None;
    public bool onEnter = true, onLeave = false, playerOnly = false;

    private LinkedList<GameObject> affectedCharacters;
    private LinkedListNode<GameObject> currentChar;
    private AsyncOperation async;
    private int numTriggers = 0;
    private bool setToDestroy = false;

    // Use this for initialization
    void Start()
    {
        affectedCharacters = new LinkedList<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (affectedCharacters.Count > 0)
        {
            currentChar = affectedCharacters.First;
            while (currentChar != null)
            {
                if (Mathf.Abs(z_depth - currentChar.Value.transform.position.z) > .1f)
                {
                    currentChar.Value.transform.position = Vector3.Lerp(currentChar.Value.transform.position, new Vector3(currentChar.Value.transform.position.x, currentChar.Value.transform.position.y, z_depth), speed * Time.deltaTime);
                }
                else
                {
                    currentChar.Value.transform.position = new Vector3(currentChar.Value.transform.position.x, currentChar.Value.transform.position.y, z_depth);
                    if (currentChar.Value.tag == Tags.PLAYER)
                        currentChar.Value.GetComponent<PlayerMovementBasic>().jumping = false; //avoiding any bugs
                    affectedCharacters.RemoveLast();
                }
                currentChar = (currentChar != null) ? currentChar.Next : null;
            }
        }
        else if (setToDestroy)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (onEnter)
        {
            if (playerOnly && other.tag != Tags.PLAYER)
                return;

            if (faceRestriction != FaceRestriction.None)
            {
                if (faceRestriction == FaceRestriction.Bottom)
                {
                    //  return;
                }
                if (faceRestriction == FaceRestriction.Top)
                {
                    //if(NOT coming from BELOW)
                    //  return;
                }
                if (faceRestriction == FaceRestriction.Left)
                {
                    //if(NOT coming from BELOW)
                    //  return;
                }
                if (faceRestriction == FaceRestriction.Top)
                {
                    //if(NOT coming from BELOW)
                    //  return;
                }
            }
            if (other.rigidbody != null && other.rigidbody.useGravity)
                affectedCharacters.AddFirst(other.gameObject);

            numTriggers++;
            if (numTriggers == destroyAfterTriggers)
                setToDestroy = true;
            //yield return async;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (onEnter)
        {
            if (playerOnly && col.collider.tag != Tags.PLAYER)
                return;

            switch (faceRestriction)
            {
                case FaceRestriction.None: return;
                case FaceRestriction.Bottom:
                    if (col.contacts[0].normal.y < .9f)
                        return; 
                    break;
                case FaceRestriction.Top:
                    if (col.contacts[0].normal.y > -.9f)
                        return;
                    break;
                case FaceRestriction.Left:
                    if (col.contacts[0].normal.x < .9f)
                        return;
                    break;
                case FaceRestriction.Right:
                    if (col.contacts[0].normal.x > -.9f)
                        return; 
                    break;
                case FaceRestriction.Front: 
                    if (col.contacts[0].normal.z < .9f)
                        return;
                    break;
                case FaceRestriction.Back: 
                    if (col.contacts[0].normal.z > -.9f)
                        return;
                    break;
            }

            if (col.collider.rigidbody != null && col.collider.rigidbody.useGravity)
                affectedCharacters.AddFirst(col.collider.gameObject);

            numTriggers++;
            if (numTriggers == destroyAfterTriggers)
                setToDestroy = true;
        }
    }
    void OnTriggerLeave(Collider other)
    {

        if (onLeave)
        {
            if (playerOnly && other.tag != Tags.PLAYER)
                return;

            if (faceRestriction != FaceRestriction.None)
            {
                if (faceRestriction == FaceRestriction.Bottom)
                {
                    //if(NOT coming from ABOVE)
                    //  return;
                }
                if (faceRestriction == FaceRestriction.Top)
                {
                    //if(NOT coming from BELOW)
                    //  return;
                }
                if (faceRestriction == FaceRestriction.Left)
                {
                    //if(NOT coming from BELOW)
                    //  return;
                }
                if (faceRestriction == FaceRestriction.Top)
                {
                    //if(NOT coming from BELOW)
                    //  return;
                }
            }

            if (other.rigidbody != null && other.rigidbody.useGravity)
                affectedCharacters.AddFirst(other.gameObject);

            numTriggers++;
            if (numTriggers == destroyAfterTriggers)
                setToDestroy = true;
            //yield return async;
        }
    }
    void OnCollisionLeave(Collision col)
    {
        if (onLeave)
        {
            if (playerOnly && col.collider.tag != Tags.PLAYER)
                return;

            if (col.collider.rigidbody != null && col.collider.rigidbody.useGravity)
                affectedCharacters.AddFirst(col.collider.gameObject);

            numTriggers++;
            if (numTriggers == destroyAfterTriggers)
                setToDestroy = true;
        }
    }
}
