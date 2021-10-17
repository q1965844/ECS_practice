using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class JobExample : MonoBehaviour
{
    void Start()
	{
		DoExample();
	}

	private static void DoExample()
	{
		var reslutArray = new NativeArray<int>(1,Allocator.TempJob);

		// instantiate and initialize
		firstJob first = new firstJob()
		{
			A = 5,
			result = reslutArray
		};

		secondJob secondJob = new secondJob()
		{
			result = reslutArray
		};

		// schedule
		JobHandle handle = first.Schedule();
		JobHandle secondHandle = secondJob.Schedule(handle);

		// other tasks to run in Main thread in parallel.
		secondHandle.Complete();

		Debug.Log($"result = {reslutArray[0]}");

		reslutArray.Dispose();
	}

	// JOB只能使用Private嗎? 都可以
	private struct firstJob : IJob
	{
		public int A;
		public NativeArray<int> result;

		public void Execute()
		{
			result[0] = A;
		}
	}

	public struct secondJob : IJob
	{
		public NativeArray<int> result;

		public void Execute()
		{
			result[0] += 1;
		}
	}
}
