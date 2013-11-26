public var tex : Cubemap;

function Start () 
{
	var filter : MeshFilter = gameObject.GetComponent(MeshFilter);
	var mesh : Mesh = filter.mesh;
	var colors : Color[] = mesh.colors;
	
	
	
	for (var i = 0 ; i < colors.Length ; i ++) 
	    {
	        var col : Color = colors[i]; 
	        var a : float = (col.b + col.g + col.r) / 3.0;
	        //colors[i] = Color(col.r, col.g, col.b, a * a * glowAmount);
	    }
}