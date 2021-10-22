using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

/// <summary>
/// 只使用Main thread執行運算
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
/// 使用Job System切分成Chunks並且由多個執行緒執行運算
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