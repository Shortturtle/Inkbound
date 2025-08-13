using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractiveWater : MonoBehaviour
{
    [Header("Mesh Generation")]
    [Range(2, 500)] public int numOfXVertices = 70;
    public float width = 10;
    public float height = 4;
    public Material waterMaterial;
    private const int NUM_OF_Y_VERTICES = 2;

    [Header("Gizmos")]
    public Color gizmoColor = Color.white;

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private int[] topVerticesIndex;

    private EdgeCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        coll = GetComponent<EdgeCollider2D>();
        coll.isTrigger = true;
    }

    public void ResetEdgeCollider()
    {
        coll = GetComponent<EdgeCollider2D>();

        Vector2[] newPoints = new Vector2[2];

        Vector2 firstPoint = new Vector2(vertices[topVerticesIndex[0]].x, vertices[topVerticesIndex[0]].y);
        newPoints[0] = firstPoint;

        Vector2 secondPoint = new Vector2(vertices[topVerticesIndex[topVerticesIndex.Length - 1]].x, vertices[topVerticesIndex[topVerticesIndex.Length - 1]].y);
        newPoints[1] = secondPoint;

        coll.offset = Vector2.zero;
        coll.points = newPoints;
    }

    public void GenerateMesh()
    {
        mesh = new Mesh();

        //add vertices
        vertices = new Vector3[numOfXVertices *  NUM_OF_Y_VERTICES];
        topVerticesIndex = new int[numOfXVertices];
        for (int y = 0; y < NUM_OF_Y_VERTICES; y++)
        {
            for (int x = 0; x < numOfXVertices; x++)
            {
                float xPos = (x / (float)(numOfXVertices - 1)) * width - width / 2;
                float yPos = (y/ (float)(NUM_OF_Y_VERTICES - 1)) * height - height / 2;
                vertices[y * numOfXVertices + x] = new Vector3(xPos, yPos, 0f);

                if (y == numOfXVertices - 1)
                {
                    topVerticesIndex[x] = y * numOfXVertices + x;
                }
            }
        }

        //construct triangles
        int[] triangles = new int[(numOfXVertices - 1) * (NUM_OF_Y_VERTICES - 1) * 6];
        int index = 0;

        for(int y = 0; y < NUM_OF_Y_VERTICES - 1; y++)
        {
            for (int x = 0;x < numOfXVertices - 1; x++)
            {
                int bottomLeft = y * numOfXVertices + x;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + numOfXVertices;
                int topRight = topLeft + 1;

                triangles[index++] = bottomLeft;
                triangles[index++] = topLeft;
                triangles[index++] = bottomRight;

                triangles[index++] = bottomRight;
                triangles[index++] = topLeft;
                triangles[index++] = topRight;
            }
        }

        //UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2((vertices[i].x + width/2) / width, (vertices[i].y + height/2) / height);
        }

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        meshRenderer.material = waterMaterial;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}

[CustomEditor(typeof(InteractiveWater))]
public class InteraciveWaterEditor : Editor
{
    private InteractiveWater water;

    private void OnEnable()
    {
        water = (InteractiveWater)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        root.Add(new VisualElement { style = { height = 10 } });

        Button generateMeshButton = new Button(() => water.GenerateMesh())
        {
            text = "Generate Mesh"
        };

        root.Add(generateMeshButton);

        Button placeEdgeColliderButton = new Button(() => water.ResetEdgeCollider())
        {
            text = "Place Edge Collider"
        };
        root.Add(placeEdgeColliderButton);

        return root;
    }

    private void ChangeDimensions(ref float width, ref float height, float calculatedWidthMax, float calculatedHeightMax)
    {
        width = Mathf.Max(0.1f, calculatedWidthMax);
        height = Mathf.Max(0.1f, calculatedHeightMax);
    }


    private void OnSceneGUI()
    {
        //Draw wireframe box
        Handles.color = water.gizmoColor;
        Vector3 center = water.transform.position;
        Vector3 size = new Vector3(water.width, water.height, 0.1f);
        Handles.DrawWireCube(center, size);

        //Handles for width and height
        float handleSize = HandleUtility.GetHandleSize(center) * 0.1f;
        Vector3 snap = Vector3.one * 0.1f;

        //Corner handles
        Vector3[] corners = new Vector3[4];
        corners[0] = center + new Vector3(-water.width / 2, -water.height / 2, 0); // Bottom-left
        corners[1] = center + new Vector3(water.width / 2, -water.height / 2, 0); // Bottom-right
        corners[2] = center + new Vector3(-water.width / 2, water.height / 2, 0); // Top-left
        corners[3] = center + new Vector3(water.width / 2, water.height / 2, 0); // Top-right

        //Handles for each corner
        EditorGUI.BeginChangeCheck();
        Vector3 newBottomLeft = Handles.FreeMoveHandle(corners[0], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref water.width, ref water.height, corners[1].x - newBottomLeft.x, corners[3].y - newBottomLeft.y);
            water.transform.position += new Vector3((newBottomLeft.x - corners[0].x)/2, (newBottomLeft.y - corners[0].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomRight = Handles.FreeMoveHandle(corners[1], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref water.width, ref water.height, newBottomRight.x - corners[0].x, corners[3].y - newBottomRight.y);
            water.transform.position += new Vector3((newBottomRight.x - corners[1].x) / 2, (newBottomRight.y - corners[1].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopLeft = Handles.FreeMoveHandle(corners[2], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref water.width, ref water.height, corners[3].x - newTopLeft.x, newTopLeft.y - corners[0].y);
            water.transform.position += new Vector3((newTopLeft.x - corners[2].x) / 2, (newTopLeft.y - corners[2].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopRight = Handles.FreeMoveHandle(corners[3], handleSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref water.width, ref water.height, newTopRight.x - corners[2].x, newTopRight.y - corners[1].y);
            water.transform.position += new Vector3((newTopRight.x - corners[3].x) / 2, (newTopRight.y - corners[3].y) / 2, 0);
        }

        if (GUI.changed)
        {
            water.GenerateMesh();
        }
    }
}
