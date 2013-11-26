private var thePitch = 0.0;

function Update () 
{
	
	thePitch = Mathf.Sin (Time.time * 0.5) * 0.3 + 1.0;
	audio.pitch = thePitch;
}