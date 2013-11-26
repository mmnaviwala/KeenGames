#pragma strict
#pragma implicit
#pragma downcast

var fps : float = 0.00;

var fpses = Array();

function Update ()
{
	fps = 1 / Time.smoothDeltaTime;
	fpses.Add(fps);
}

function Start ()
{
	while (true)	
	{
		var nfps : float = 0.00;
		for(var f : float in fpses)
		{
			nfps += f / fpses.length;
		}	
		guiText.text = "FPS: " + Mathf.Round(nfps);
		fpses.length = 0;
		yield WaitForSeconds(0.3);
	}
}