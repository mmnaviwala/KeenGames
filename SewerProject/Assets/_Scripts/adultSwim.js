var startPosition : Transform;

function OnTriggerEnter(theCollider : Collider) 
{
    theCollider.transform.position = startPosition.position;
}