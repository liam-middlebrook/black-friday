using UnityEngine;
using System.Collections;

public class FlowFieldManager : MonoBehaviour
{
    public ComputeShader fieldShader;

    int kernel_InitFlowfield;
    int kernel_ForceAtPosition;

    ComputeBuffer outputForce;

    public RenderTexture fieldTexture;

    // Use this for initialization
    void Start()
    {
        fieldTexture = new RenderTexture(512, 512, 1);
        fieldTexture.enableRandomWrite = true;

        kernel_InitFlowfield = fieldShader.FindKernel("InitFlowfield");
        kernel_ForceAtPosition = fieldShader.FindKernel("ForceAtPosition");

        fieldShader.SetTexture(kernel_InitFlowfield, "NewField", fieldTexture);

        fieldShader.SetTexture(kernel_ForceAtPosition, "FlowField", fieldTexture);

        fieldShader.SetFloats("FieldSize", VectorToFloatArray(new Vector2(fieldTexture.width, fieldTexture.height)));
        fieldShader.SetFloat("positionRatio", 1.0f);

        outputForce = new ComputeBuffer(1, 8);

        fieldShader.SetBuffer(kernel_ForceAtPosition, "force", outputForce);


        fieldShader.Dispatch(kernel_InitFlowfield, fieldTexture.width/8, fieldTexture.height/8, 1);
    }

    public void OnDisable()
    {
        outputForce.Release();
    }
    public Vector2 GetForce2D(Vector2 position)
    {
        fieldShader.SetFloats("position", VectorToFloatArray(position));
        fieldShader.SetFloat("step", 0);
        fieldShader.Dispatch(kernel_ForceAtPosition, 512, 1, 1);
        fieldShader.SetFloat("step", 4);
        fieldShader.Dispatch(kernel_ForceAtPosition, 1, 1, 1);

        float[] forceVector = new float[2];
        outputForce.GetData(forceVector);

        return new Vector2(forceVector[0], forceVector[1]);
    }
    float[] VectorToFloatArray(Vector2 vector)
    {
        return new float[] { vector.x, vector.y };
    }
}
