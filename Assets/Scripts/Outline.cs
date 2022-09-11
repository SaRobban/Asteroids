using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    float off = 0.05f;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.TryGetComponent(out MeshFilter meshF)) {
            Mesh m = meshF.mesh;
            VertsNormals vns = GetVertsFromObject(m);
            vns = SmoothNormals(vns);
            vns = OffsetAndFlipNormals(vns);
            m.SetNormals(vns.normals);
            m.SetVertices(vns.verts);
            for(int i = 0;i < vns.normals.Length; i++)
            {
                Debug.DrawRay(vns.verts[i], vns.normals[i], Color.magenta, 5);
            }
            //            m.RecalculateNormals();

            m.triangles=(FlipSurface(m.triangles));
            m.RecalculateBounds();
            m.UploadMeshData(true);
        }
    }

    VertsNormals GetVertsFromObject(Mesh m)
    {
        Vector3[] verts = m.vertices;
        Vector3[] normals = m.normals;

        return new VertsNormals(verts, normals);
    }

    VertsNormals SmoothNormals(VertsNormals vns)
    {
        Vector3[] smoothNormals = new Vector3[vns.normals.Length];
        for (int x = 0; x < vns.normals.Length; x++)
        {
            smoothNormals[x] = Vector3.zero;

            for (int y = 0; y < vns.normals.Length; y++)
            {
                if (vns.verts[x] == vns.verts[y])
                {
                    smoothNormals[x] += vns.normals[y];
                }
            }
            smoothNormals[x] = smoothNormals[x].normalized;
        }

        vns.normals = smoothNormals;
        return vns;

    }

    VertsNormals OffsetAndFlipNormals(VertsNormals vns)
    {
        for(int i = 0; i < vns.normals.Length; i++)
        {
            vns.verts[i] += vns.normals[i] * off;
            vns.normals[i] *= -1;
        }
        return vns;
    }

    int[] FlipSurface(int[] triangles)
    {
        for (int i = 0; i < triangles.Length; i = i + 3)
        {
            int tmp = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = tmp;
        }
        return triangles;
    }
}

public struct VertsNormals
{
    public Vector3[] verts;
    public Vector3[] normals;
    public VertsNormals(Vector3[] verts, Vector3[] normals)
    {
        this.verts = verts;
        this.normals = normals;
    }
}
