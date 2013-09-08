using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float smoothness = 1.5f;
    private Transform player;
    private Vector3 relCameraPos;
    private float relCamPosMagnitude;
    private Vector3 newPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        relCameraPos = transform.position - player.position;
        relCamPosMagnitude = relCameraPos.magnitude - 0.5f;
    }

    void FixedUpdate()
    {
        Vector3 standardPos = player.position + relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCamPosMagnitude;

        Vector3[] checkPoints = new Vector3[5]; //zoom points
        checkPoints[0] = standardPos;
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f); //25% zoom
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
        checkPoints[4] = abovePos;

        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (ViewingPosCheck(checkPoints[i]) == true)
                break;
        }

        this.transform.position = Vector3.Lerp(transform.position, newPos, smoothness * Time.deltaTime);
        SmoothLook();
    }

    bool ViewingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCamPosMagnitude))
        {
            if (hit.transform != player) //if the raycast from the cam to the player isn't the player (there's an object in between)
            {
                return false;
            }
        }

        newPos = checkPos;
        return true;
    }

    void SmoothLook()
    {
        Vector3 relPlayerPosition = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, smoothness * Time.deltaTime);
    }
}
