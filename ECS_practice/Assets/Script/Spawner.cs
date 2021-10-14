using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class Spawner : MonoBehaviour
{
    [SerializeField] Mesh unitMesh_;
    [SerializeField] Material unitMaterial_;
    [SerializeField] GameObject unitPrefab_;

    Entity entityPrefab_;
    World defaultWord_;
    EntityManager entityManager_;

    [SerializeField] int sizeX_;
    [SerializeField] int sizeY_;
    [Range(0,2)]
    [SerializeField] float space_;

    void Start()
	{
        defaultWord_ = World.DefaultGameObjectInjectionWorld;
        entityManager_ = defaultWord_.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWord_, null);
        entityPrefab_ = GameObjectConversionUtility.ConvertGameObjectHierarchy(unitPrefab_, settings);

        InstatiateEntityGrids(sizeX_, sizeY_, space_);
	}

    void InstantiateEntity(float3 position)
    {
        Entity entity_ = entityManager_.Instantiate(entityPrefab_);
        entityManager_.SetComponentData(entity_, new Translation
        {
            Value = position
        }); 
    }

    void InstatiateEntityGrids(int sizeX,int sizeY,float space)
    {
		for (int i = 0; i < sizeX; i++)
		{
			for (int j = 0; j < sizeY; j++)
			{
                InstantiateEntity(new float3(i * space, j * space, 0f));
			}
		}
    }
}
