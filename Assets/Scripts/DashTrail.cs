using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;

public class Trailstruct
{
    public List<GameObject> Obj = new List<GameObject>();
    public List<MeshFilter> meshFilters = new List<MeshFilter>();
    public List<Mesh> meshs = new List<Mesh>();
}

public class DashTrail : MonoBehaviour
{
    public List<SkinnedMeshRenderer> skinMeshs = new List<SkinnedMeshRenderer>();

    public GameObject meshRoot;

    public List<Trailstruct> trailStructs = new List<Trailstruct>();

    public List<GameObject> bodyParts = new List<GameObject>();
    public List<Vector3> posMemory = new List<Vector3>();
    public List<Quaternion> rotMemory = new List<Quaternion>();
    
    [SerializeField] private int trailCount = 5;
    private bool isCreate;

    void Start()
    {
        meshRoot = GameManager._Instance._Player.transform.GetChild(1).gameObject;
        
        for (int i = 0; i < 19; i++)
        {
            skinMeshs.Add(meshRoot.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>());
        }

        for(int i = 0; i < trailCount; i++)
        {
            trailStructs.Add(new Trailstruct());
            GameObject obj = new GameObject("DashTrail" + i);
            obj.transform.SetParent(this.transform);
            bodyParts.Add(obj);
            obj.SetActive(false);
            BakeMeshs(obj.transform, i);
        }
    }

    void Update()
    {
        if (GameManager._Instance._Pctrl.isDash)
        {
            DrawTrail();
            
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].transform.position = posMemory[Mathf.Min(i, posMemory.Count - 1)];
                bodyParts[i].transform.rotation = rotMemory[Mathf.Min(i, rotMemory.Count - 1)];
            }
            for (int i = bodyParts.Count - 1; i >= 0; i--)
            {
                bodyParts[i].SetActive(true);
            }
        }
        else isCreate = false;
    }

    private void FixedUpdate()
    {
        if (!isCreate)
        {
            DrawPositionCreate();
        }
        
    }

    void BakeMeshs(Transform obj, int count)
    {
        for(int i = 0; i < skinMeshs.Count; i++)
        {
            trailStructs[count].Obj.Add(new GameObject(meshRoot.transform.GetChild(i).gameObject.name));
            trailStructs[count].Obj[i].transform.SetParent(obj);
            trailStructs[count].Obj[i].AddComponent<MeshFilter>();
            trailStructs[count].Obj[i].AddComponent<MeshRenderer>();
            trailStructs[count].Obj[i].AddComponent<TrailColor>();
            trailStructs[count].meshs.Add(new Mesh());
            skinMeshs[i].BakeMesh(trailStructs[count].meshs[i]);
            trailStructs[count].meshFilters.Add(trailStructs[count].Obj[i].GetComponent<MeshFilter>());
            trailStructs[count].meshFilters[i].mesh = trailStructs[count].meshs[i];            
            trailStructs[count].Obj[i].GetComponent<MeshRenderer>().material = Resources.Load<Material>("Model/Test");

        }        
    }    

    public void DrawPositionCreate()
    {
        if (posMemory.Count > trailCount) posMemory.RemoveAt(trailCount);
        if (rotMemory.Count > trailCount) rotMemory.RemoveAt(trailCount);
        posMemory.Insert(0, transform.TransformDirection(GameManager._Instance._Player.transform.position));
        rotMemory.Insert(0, GameManager._Instance._Player.transform.rotation);

        if (posMemory.Count == trailCount && rotMemory.Count == trailCount) isCreate = true;        
    }

    public void DrawTrail()
    {
        for (int j = trailStructs.Count - 2; j >= 0; j--)
        {
            for (int k = 0; k < skinMeshs.Count; k++)
            {
                trailStructs[j + 1].meshs[k].vertices = trailStructs[j].meshs[k].vertices;
                trailStructs[j + 1].meshs[k].triangles = trailStructs[j].meshs[k].triangles;
            }
        }

        for (int j = 0; j < skinMeshs.Count; j++)
        {
            skinMeshs[j].BakeMesh(trailStructs[0].meshs[j]);
        }             
    }
}
