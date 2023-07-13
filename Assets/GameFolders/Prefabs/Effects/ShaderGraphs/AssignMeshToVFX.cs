using UnityEngine;
using UnityEngine.VFX;

public class AssignMeshToVFX : MonoBehaviour
{
    public VisualEffect vfxGraph;
    public MeshRenderer meshRenderer;

    private void Start()
    {
        // Get the mesh from the Mesh Renderer component
        Mesh mesh = meshRenderer.GetComponent<MeshFilter>().sharedMesh;

        // Assign the mesh to the "Mesh" input in the VFX Graph
        vfxGraph.SetMesh("Mesh", mesh);
    }
}
