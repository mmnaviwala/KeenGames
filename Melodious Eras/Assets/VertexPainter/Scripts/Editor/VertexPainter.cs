using UnityEngine;
using UnityEditor;
using System.Collections;


public enum ColorPaintMode
{
    Default,
    Multiply,
    Darken,
    Lighten,
    Color,
    Add,
    Substract,
    ColorBurn,
    Screen,
    Hue
}

/// <author>Patrick Neum�ller</author>
/// <Date>13.04.2012</Date>
/// <summary>
/// F�gt ein Editorfenster hinzu, mit dem man durch einen Pinsel Vertex Colors auf das ausgew�hlte Objekt zeichnen kann.
/// </summary>
/// <remarks>
///     15.04.2012      Patrick Neum�ller        Funktion um auf Explozite Channel zu malen implementiert
/// </remarks>
/// <Todo> Shader Spezifische Namen?  - Patrick Neum�ller </Todo>
public static class VertexShaders
{
    static private Material vertexMaterial;
    static public Material ShowVertexColor
    {
        get
        {
            if (vertexMaterial != null) return vertexMaterial;
            string shader =
                "Shader \"Vertex Data\" {" +
                 "Properties {	_MainTex (\"Texture\", 2D) = \"white\" {} }" +
                 "Category {" +
                 "	Tags { \"Queue\"=\"Geometry\"}" +
                 "	Lighting Off" +
                 "	BindChannels {" +
                 "	Bind \"Color\", color" +
                 "	Bind \"Vertex\", vertex" +
                 "	Bind \"TexCoord\", texcoord" +
                 "}" +
                 "SubShader {" +
                 "	Pass {" +
                 "	SetTexture [_MainTex] {" +
                 "		combine primary + primary" +
                 "	}" +
                 "	}" +
                 "}" +
                 "SubShader {" +
                 "	Pass {" +
                 "	SetTexture [_MainTex] {" +
                 "		constantColor (1,1,1,1)" +
                 "		combine texture lerp(texture) constant" +
                 "	}" +
                 "}" +
                 "}" +
                 "}}";

            Shader tempShader = Shader.Find("Hidden/Vertex Data");
            if (tempShader != null) vertexMaterial = new Material(tempShader);
            else vertexMaterial = new Material(shader);
           return vertexMaterial;
        }
      set { }
    }
}

public class VertexPainter : EditorWindow
{
    public struct PaintChannels
    {
        private bool _red, _green, _blue, _alpha;
        public bool Red
        {
            get{return _red;}
            set
            {
                if (value != _red)
                {
                    _red = value;
                    if(VertexShaders.ShowVertexColor.HasProperty("_Color"))
                    {
                        Vector4 tempVect = VertexShaders.ShowVertexColor.GetVector("_Color");
                        tempVect.x = _red ? 1 : 0;
                        VertexShaders.ShowVertexColor.SetVector("_Color",tempVect);
                    }
                }
            }
        }
        public bool Green
        {
            get { return _green; }
            set
            {
                if (value != _green)
                {
                    _green = value;
                    if (VertexShaders.ShowVertexColor.HasProperty("_Color"))
                    {
                        Vector4 tempVect = VertexShaders.ShowVertexColor.GetVector("_Color");
                        tempVect.y = _green ? 1 : 0;
                        VertexShaders.ShowVertexColor.SetVector("_Color", tempVect);
                    }
                }
            }
        }
        public bool Blue
        {
            get { return _blue; }
            set
            {
                if (value != _blue)
                {
                    _blue = value;
                    if (VertexShaders.ShowVertexColor.HasProperty("_Color"))
                    {
                        Vector4 tempVect = VertexShaders.ShowVertexColor.GetVector("_Color");
                        tempVect.z = _blue ? 1 : 0;
                        VertexShaders.ShowVertexColor.SetVector("_Color", tempVect);
                    }
                }
            }
        }
        public bool Alpha
        {
            get { return _alpha; }
            set
            {
                if (value != _alpha)
                {
                    _alpha = value;
                    if (VertexShaders.ShowVertexColor.HasProperty("_Color"))
                    {
                        Vector4 tempVect = VertexShaders.ShowVertexColor.GetVector("_Color");
                        tempVect.w = _alpha ? 1 : 0;
                        VertexShaders.ShowVertexColor.SetVector("_Color", tempVect);
                    }
                }
            }
        }
    };

    static public PaintChannels ChannelsToPaint  = new PaintChannels();

    enum Mode
    {
        None,
        Painting
    }
    static Mode currentMode = Mode.None;
    static GameObject currentSelection;
    static Mesh currentSelectionMesh;
    static MeshFilter currentSelectionMeshFilter;
    static Color currentColor = Color.white;
    static float radius = 1f;
    static float blendFactor = 0.5f;
    static SceneView.OnSceneFunc onSceneGUIFunc;
    static VertexPainter window;
    static bool RenderVertexColors;
    static Material oldMaterial;
    static int VertexPainterHash;
    static ColorPaintMode ColorPaintMode = ColorPaintMode.Default;
    static bool GotCollider = false;

    GUIStyle boxBackground;

    [MenuItem("Tools/VertexPainter")]
    static void Init()
    {
        window = (VertexPainter)EditorWindow.GetWindow(typeof(VertexPainter));

        onSceneGUIFunc = new SceneView.OnSceneFunc(OnSceneGUI);
        SceneView.onSceneGUIDelegate += onSceneGUIFunc;

        VertexPainterHash = window.GetHashCode();
        currentSelection = Selection.activeGameObject;
        if (currentSelection != null)
        {
            currentSelectionMeshFilter = currentSelection.GetComponent<MeshFilter>();

            if (currentSelectionMeshFilter != null)
                currentSelectionMesh = currentSelectionMeshFilter.sharedMesh;
            else
                Debug.Log("Meshfilter null");
        }

        ChannelsToPaint.Red = true;
        ChannelsToPaint.Blue = true;
        ChannelsToPaint.Green = true;

    }

    static void PaintVertexColors(bool delete)
    {

        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, float.MaxValue))
        {
            Vector3[] vertices = currentSelectionMesh.vertices;
            Color[] colrs = currentSelectionMesh.colors;

            Undo.RegisterUndo(currentSelectionMesh, "Vertex Paint");
            Vector3 pos = currentSelection.transform.InverseTransformPoint(hit.point);
            float finalBlendFactor = blendFactor * 0.2f; if (finalBlendFactor >= 0.2f) finalBlendFactor = 1;
            for (int i = 0; i < vertices.Length; i++)
            {
                float sqrMagnitude = (vertices[i] - pos).magnitude;
                if (sqrMagnitude > radius)
                    continue;
                float rc = colrs[i].r, gc = colrs[i].g, bc = colrs[i].b, ac = colrs[i].a;

                switch (ColorPaintMode)
                {
                        //(Background Layer inverted, divided by Active Layer, and the quotient is then inverted).
                    case global::ColorPaintMode.ColorBurn:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : (1 - (1 - rc) / currentColor.r), finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : (1 - (1 - gc) / currentColor.g), finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : (1 - (1 - bc) / currentColor.b), finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : (1 - (1 - ac) / currentColor.a), finalBlendFactor);
                        } break;
                    case global::ColorPaintMode.Screen:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : (1 - (1 - currentColor.r) * (1 - rc)), finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : (1 - (1 - currentColor.g) * (1 - gc)), finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : (1 - (1 - currentColor.b) * (1 - bc)), finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : (1 - (1 - currentColor.a) * (1 - ac)), finalBlendFactor);
                        } break;
                    case global::ColorPaintMode.Lighten:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : (rc > currentColor.r) ? rc : currentColor.r, finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : (gc > currentColor.g) ? gc : currentColor.g, finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : (bc > currentColor.b) ? bc : currentColor.b, finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : (ac > currentColor.a) ? ac : currentColor.a, finalBlendFactor);
                        } break;
                    case global::ColorPaintMode.Darken:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : (rc < currentColor.r) ? rc : currentColor.r, finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : (gc < currentColor.g) ? gc : currentColor.g, finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : (bc < currentColor.b) ? bc : currentColor.b, finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : (ac < currentColor.a) ? ac : currentColor.a, finalBlendFactor);
                        } break;
                    case global::ColorPaintMode.Hue:
                        {
                            float H, H2, S, S2, V, V2;
                            EditorGUIUtility.RGBToHSV(colrs[i], out H, out S, out V);
                            EditorGUIUtility.RGBToHSV(currentColor, out H2, out S2, out V2);

                            colrs[i] = EditorGUIUtility.HSVToRGB(Mathf.Lerp(H, H2, finalBlendFactor), S, V);
                            rc = colrs[i].r; gc = colrs[i].g; bc = colrs[i].b; ac = colrs[i].a;

                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : rc - currentColor.r, 0);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : gc - currentColor.g, 0);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : bc - currentColor.b, 0);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : ac - currentColor.a, 0);
                        } break;
                    case global::ColorPaintMode.Color:
                        {
                            float H, H2, S, S2, V, V2;
                            EditorGUIUtility.RGBToHSV(colrs[i], out H, out S, out V);
                            EditorGUIUtility.RGBToHSV(currentColor, out H2, out S2, out V2);

                            colrs[i] = EditorGUIUtility.HSVToRGB(Mathf.Lerp(H, H2, finalBlendFactor), Mathf.Lerp(S, S2, finalBlendFactor), V);
                            rc = colrs[i].r; gc = colrs[i].g; bc = colrs[i].b; ac = colrs[i].a;

                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : rc - currentColor.r, 0);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : gc - currentColor.g, 0);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : bc - currentColor.b, 0);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : ac - currentColor.a, 0);
                        } break;
                    case global::ColorPaintMode.Substract:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : rc - currentColor.r, finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : gc - currentColor.g, finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : bc - currentColor.b, finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : ac - currentColor.a, finalBlendFactor);
                        } break;
                    case global::ColorPaintMode.Add:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : rc + currentColor.r, finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : gc + currentColor.g, finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : bc + currentColor.b, finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : ac + currentColor.a, finalBlendFactor);
                        } break;
                    case global::ColorPaintMode.Multiply:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : currentColor.r * rc, finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : currentColor.g * gc, finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : currentColor.b * bc, finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : currentColor.a + ac, finalBlendFactor);
                        } break;
                    default:
                        {
                            if (ChannelsToPaint.Red)
                                rc = Mathf.Lerp(rc, delete ? 0 : currentColor.r, finalBlendFactor);
                            if (ChannelsToPaint.Green)
                                gc = Mathf.Lerp(gc, delete ? 0 : currentColor.g, finalBlendFactor);
                            if (ChannelsToPaint.Blue)
                                bc = Mathf.Lerp(bc, delete ? 0 : currentColor.b, finalBlendFactor);
                            if (ChannelsToPaint.Alpha)
                                ac = Mathf.Lerp(ac, delete ? 0 : currentColor.a, finalBlendFactor);
                        } break;
                }

                colrs[i] = new Color(rc,gc,bc,ac);
            }
            currentSelectionMesh.colors = colrs;
        }
    }

    static void DrawHandle()
    {
        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, float.MaxValue))
        {
            Handles.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Max(blendFactor*0.2f,0.05f));
            Handles.DrawSolidDisc(hit.point, hit.normal, radius*5);
            Handles.color = new Color(currentColor.r,currentColor.g,currentColor.b,1);
            Handles.DrawWireDisc(hit.point, hit.normal, radius*5);
        }
    }

    static void SetMaterial()
    {
        if (RenderVertexColors)
        {
            if (oldMaterial == null)
            {
                oldMaterial = currentSelection.GetComponent<Renderer>().sharedMaterial;
                currentSelection.GetComponent<Renderer>().sharedMaterial = VertexShaders.ShowVertexColor;
            }
        }
        else
        {
            if (oldMaterial != null)
            {
                currentSelection.GetComponent<Renderer>().sharedMaterial = oldMaterial;
                oldMaterial = null;
            }

        }
    }

    public static void OnSceneGUI(SceneView sceneview)
    {
        if ((currentSelection == null) || (currentSelectionMesh == null) || (currentSelectionMeshFilter == null))
            return;

        SetMaterial();

        int ctrlID = GUIUtility.GetControlID(VertexPainterHash, FocusType.Passive);
        Event current = Event.current;

        switch (current.type)
        {
            case EventType.keyDown:
                {
                    if (current.keyCode == (KeyCode.Plus) || current.keyCode == (KeyCode.KeypadPlus))
                        blendFactor += 0.01f;
                    else if (current.keyCode == (KeyCode.Minus) || current.keyCode == (KeyCode.KeypadMinus))
                        blendFactor -= 0.01f;
                    if (current.keyCode == (KeyCode.Alpha1)) radius = 1;
                    else if (current.keyCode == (KeyCode.Alpha2)) radius = 2;
                    else if (current.keyCode == (KeyCode.Alpha3)) radius = 3;
                    else if (current.keyCode == (KeyCode.Alpha4)) radius = 4;
                    else if (current.keyCode == (KeyCode.Alpha5)) radius = 5;

                    VertexPainter.window.Repaint();
                    break;
                }
            case EventType.keyUp:
                if ((current.keyCode == KeyCode.Q) && (current.control))
                {
                    RenderVertexColors = !RenderVertexColors;
                    VertexPainter.window.Repaint();
                }
                if (current.keyCode == (KeyCode.Tab))
                {
                    if (ChannelsToPaint.Alpha)
                    {
                        ChannelsToPaint.Alpha = false;
                        ChannelsToPaint.Red = true;
                        ChannelsToPaint.Green = true;
                        ChannelsToPaint.Blue = true;
                    }
                    else
                    {
                        ChannelsToPaint.Alpha = true;
                        ChannelsToPaint.Red = false;
                        ChannelsToPaint.Green = false;
                        ChannelsToPaint.Blue = false;
                    }
                    VertexPainter.window.Repaint();
                }
                break;
            case EventType.mouseUp:
                switch (currentMode)
                {
                    case Mode.Painting: break;
                }
                break;
            case EventType.mouseDown:
                switch (currentMode)
                {
                    case Mode.Painting:
                        if (current.button == 0)
                            current.Use();
                        break;
                }
                break;
            case EventType.mouseDrag:
                switch (currentMode)
                {
                    case Mode.None:
                        break;
                    case Mode.Painting:
                        if (current.button == 0)
                        {
                            EditorUtility.SetDirty(currentSelectionMesh);
                            PaintVertexColors(current.control);
                        }
                        break;
                }
                break;
            case EventType.layout:
                switch (currentMode)
                {
                    case Mode.None:
                        break;
                    case Mode.Painting:
                        HandleUtility.AddDefaultControl(ctrlID);
                        break;
                }
                break;
            default:
                break;
        }
        HandleUtility.Repaint();
        if (currentMode == Mode.Painting)
            DrawHandle();
    }

    void GenerateStyles()
    {
        if (boxBackground == null)
        {
            boxBackground = new GUIStyle();

            boxBackground.padding.top += 5;
            boxBackground.padding.bottom += 5;
        }
    }

    void Save()
    {
        if ((currentSelection == null) || (currentSelectionMesh == null) || (currentSelectionMeshFilter == null))
            return;

        AssetDatabase.Refresh();

        int id = currentSelection.GetInstanceID();
        string p = AssetDatabase.GetAssetPath(currentSelectionMesh);

        string toDelete = "";
        if ((p.Contains(".assets")) && (!p.Contains(id.ToString())))
        {
            toDelete = p;
        }

        Mesh newMesh = new Mesh();
        newMesh.vertices = currentSelectionMesh.vertices;
        newMesh.triangles = currentSelectionMesh.triangles;
        newMesh.colors = currentSelectionMesh.colors;
        newMesh.tangents = currentSelectionMesh.tangents;
        newMesh.normals = currentSelectionMesh.normals;
        newMesh.uv = currentSelectionMesh.uv;
        newMesh.uv2 = currentSelectionMesh.uv2;
        newMesh.uv2 = currentSelectionMesh.uv2;

        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();

        if (p != "") p = System.IO.Path.GetDirectoryName(p);
        else p = "Assets";
        string newPath = p + "/" + currentSelection.name + "_" + id + ".assets";

        AssetDatabase.CreateAsset(newMesh, newPath);

        currentSelectionMeshFilter.sharedMesh = newMesh;
        MeshCollider mC = currentSelection.GetComponent<MeshCollider>();
        if (mC != null)
            mC.sharedMesh = newMesh;

        currentSelectionMesh = newMesh;

        if (toDelete != "")
            AssetDatabase.DeleteAsset(toDelete);

        EditorUtility.SetSelectedWireframeHidden(currentSelection.GetComponent<Renderer>(), false);
        EditorUtility.SetDirty(currentSelection);
        AssetDatabase.Refresh();
    }

    void OnDestroy()
    {
        if (currentMode == Mode.Painting) EndPaint();

        if (oldMaterial != null)
        {
            if (currentSelection != null)
                if (currentSelection.GetComponent<Renderer>() != null)
                {
                    EditorUtility.SetSelectedWireframeHidden(currentSelection.GetComponent<Renderer>(), true);
                    currentSelection.GetComponent<Renderer>().sharedMaterial = oldMaterial;
                }
        }
        oldMaterial = null;
        currentMode = Mode.None;
        SceneView.onSceneGUIDelegate -= onSceneGUIFunc;
        window = null;
    }

    void OnGUI()
    {
        GenerateStyles();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("--Vertexpainter--", EditorStyles.boldLabel);
        GUI.color = Color.grey;
        GUILayout.Label("INFO: Press stop button to apply colors!!");
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUI.enabled = (currentSelection != null && currentSelectionMesh != null);
        if (currentSelection == null || currentSelectionMesh == null)
        {
            GUI.color = Color.red;
            EditorGUILayout.LabelField("WARNING: Please Select a Mesh");
            GUI.color = Color.white;
        }


        EditorGUILayout.ObjectField("Current Selection ", currentSelection, typeof(GameObject), true);
        EditorGUILayout.BeginVertical(boxBackground);
        currentColor = EditorGUILayout.ColorField("Color to Paint", currentColor);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Channels to Paint to:", "Check those Boxes on which you want to apply Colors"));
        ColorPaintMode = (ColorPaintMode)EditorGUILayout.EnumPopup("", ColorPaintMode);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        ChannelsToPaint.Red = GUILayout.Toggle(ChannelsToPaint.Red, "Red");
        ChannelsToPaint.Green = GUILayout.Toggle(ChannelsToPaint.Green, "Green");
        ChannelsToPaint.Blue = GUILayout.Toggle(ChannelsToPaint.Blue, "Blue");
        ChannelsToPaint.Alpha = GUILayout.Toggle(ChannelsToPaint.Alpha, "Alpha");

        GUI.color = Color.magenta;
        if (GUILayout.Button(new GUIContent("[RGB]", "You can switch between RGB and Alpha with 'Tab'"), GUILayout.MaxWidth(50)))
        {
            ChannelsToPaint.Red = true;
            ChannelsToPaint.Blue = true;
            ChannelsToPaint.Green = true;
            ChannelsToPaint.Alpha = false;
        }
        GUI.color = Color.grey;
        if (GUILayout.Button(new GUIContent("[A]", "You can switch between RGB and Alpha with 'Tab'"), GUILayout.MaxWidth(40)))
        {
         
            ChannelsToPaint.Red = false;
            ChannelsToPaint.Blue = false;
            ChannelsToPaint.Green = false;
            ChannelsToPaint.Alpha = true;
        }
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();
        Color old = GUI.color;
        EditorGUILayout.BeginHorizontal();
        GUI.color = Color.red;
        if (GUILayout.Button("Red")) currentColor = new Color(1, 0, 0, 0);
        GUI.color = Color.green;
        if (GUILayout.Button("Green")) currentColor = new Color(0, 1, 0, 0);
        GUI.color = Color.blue;
        if (GUILayout.Button("Blue")) currentColor = new Color(0, 0, 1, 0);
        GUI.color = old;
        if (GUILayout.Button("Alpha")) currentColor = new Color(0, 0, 0, 1);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        radius = EditorGUILayout.Slider(new GUIContent("Radius", "You can change the radius with the '1-5 Keys'"), radius, 0.1f, 20);
        blendFactor = EditorGUILayout.Slider(new GUIContent("Blend", "You can in/decrease the Blendfactors with '+/-'"), blendFactor, 0, 1);

        GUIContent showVertexLbl = new GUIContent("Show Vertex Colors", "Shortcut STRG+Q");
        RenderVertexColors = EditorGUILayout.Toggle(showVertexLbl, RenderVertexColors);

        if (currentSelectionMesh != null)
        {
            if (currentSelectionMesh.colors.Length != currentSelectionMesh.vertices.Length)
            {
                if (GUILayout.Button("Generate Vertex Array"))
                {
                    currentSelectionMesh.colors = new Color[currentSelectionMesh.vertices.Length];
                    EditorUtility.SetDirty(currentSelection);
                    EditorUtility.SetDirty(currentSelectionMesh);
                    AssetDatabase.SaveAssets();
                    EditorApplication.SaveAssets();
                }
            }
            else
            {

                if (GUILayout.Button("Set all to choosen color"))
                {
                    if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to recolor the whole Mesh?", "Yes", "No!"))
                    {
                        Undo.RegisterUndo(currentSelectionMesh, "Vertex Paint");
                        ResetVertexColors();
                        EditorUtility.SetDirty(currentSelectionMesh);
                    }
                }
                switch (currentMode)
                {
                    case Mode.None:
                        if (GUILayout.Button("Paint"))
                        {
                            Save();
                            GotCollider = (currentSelection.GetComponentInChildren<MeshCollider>() != null);
                            if (!GotCollider)
                                currentSelection.AddComponent<MeshCollider>();
                            EditorUtility.SetSelectedWireframeHidden(currentSelection.GetComponent<Renderer>(), true);
                            currentMode = Mode.Painting;
                        }
                        break;
                    case Mode.Painting:
                        if (GUILayout.Button("Stop"))
                        {
                            EndPaint();
                        }
                        break;
                }
            }
        }

    }

    void EndPaint()
    {
        Save();
        if (RenderVertexColors) RenderVertexColors = false;
        currentMode = Mode.None;
        if (!GotCollider && currentSelection.GetComponentInChildren<MeshCollider>() != null)
            DestroyImmediate(currentSelection.GetComponent<MeshCollider>());
    }

    void OnSelectionChange()
    {
        currentMode = Mode.None;

        currentSelection = Selection.activeGameObject;
        if (RenderVertexColors) RenderVertexColors = false;

        if (currentSelection != null)
        {
            currentSelectionMeshFilter = currentSelection.GetComponent<MeshFilter>();
            if (currentSelectionMeshFilter != null)
                currentSelectionMesh = currentSelectionMeshFilter.sharedMesh;

        }
        Repaint();
    }

    void ResetVertexColors()
    {
        Color[] colrs = currentSelectionMesh.colors;
        for (int i = 0; i < colrs.Length; i++)
        {
            colrs[i] = currentColor;
        }
        currentSelectionMesh.colors = colrs;
    }

}

