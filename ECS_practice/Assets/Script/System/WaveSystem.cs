using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

/// <summary>
/// �u�ϥ�Main thread����B��
/// </summary>
//public class WaveSystem : ComponentSystem
//{
//	protected override void OnUpdate()
//	{
//		////Jobified logic(sub thread)
//		Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData) =>
//		{
//			float zPosition = waveData.Amplitude * math.sin((float)Time.ElapsedTime * moveSpeed.value + trans.Value.x * waveData.XOffset + trans.Value.y * waveData.YOffset);
//			trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
//		});
//	}
//}


/// <summary>
/// �ϥ�Job System������Chunks�åB�Ѧh�Ӱ��������B��
/// </summary>
public class WaveSystem : SystemBase
{
	protected override void OnUpdate()
	{
		// main thread
		var elapsedTime = (float)Time.ElapsedTime;

		//Jobified logic(sub thread)
		Entities.ForEach((ref Translation trans, ref MoveSpeed moveSpeed, ref WaveData waveData) =>
		{
			float zPosition = waveData.Amplitude * math.sin(elapsedTime * moveSpeed.value + trans.Value.x * waveData.XOffset + trans.Value.y * waveData.YOffset);
			trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
		}).ScheduleParallel();
	}
}