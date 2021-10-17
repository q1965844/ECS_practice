using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class WaveSystem : ComponentSystem
{
	protected override void OnUpdate()
	{
		Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed , ref WaveData waveData) =>
		{
			float zPosition = waveData.Amplitude * math.tan((float)Time.ElapsedTime * moveSpeed.value + trans.Value.x * waveData.XOffset + trans.Value.y * waveData.YOffset);
			trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
		});
	}
}
