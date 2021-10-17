using Unity.Entities;
[GenerateAuthoringComponent]
public struct WaveData : IComponentData
{
	public float Amplitude;
	public float XOffset;
	public float YOffset;
}
