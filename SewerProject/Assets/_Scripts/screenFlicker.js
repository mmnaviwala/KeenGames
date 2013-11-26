private var initialColor : Color;

private var flicker = 0.0;

function Start()
{
	initialColor = renderer.material.GetColor("_TintColor");
}

function Update () 
{
	if (Random.value >= 0.7)
	{
	
		flicker = Random.value + 0.2;
		
		renderer.material.SetColor ("_TintColor", Color(initialColor.r * flicker, initialColor.g * flicker, initialColor.b * flicker));
	}
}