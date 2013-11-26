var scrollSpeed = 0.1;

private var initialColor : Color;

private var flicker = 0.0;


function Start()
{
	initialColor = renderer.material.GetColor("_TintColor");
}

function Update () 
{   
	var offset = Time.time * scrollSpeed;
    renderer.material.SetTextureOffset ("_Interlace", Vector2(0.0, offset));
    
    var scaleY = Mathf.Sin (Time.time) * 0.2;
    renderer.material.SetTextureScale ("_Interlace", Vector2(2.0,scaleY  + 1.6));
    
    if (Random.value >= 0.7)
	{
		flicker = Random.value + 0.2;
		
		light.intensity = flicker;
		
		renderer.material.SetColor ("_TintColor", Color(initialColor.r * flicker, initialColor.g * flicker, initialColor.b * flicker));
	}
	
	audio.pitch = scaleY + 1.0;	
}