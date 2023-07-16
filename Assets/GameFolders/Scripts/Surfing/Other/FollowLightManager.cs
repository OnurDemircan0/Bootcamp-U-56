using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class FollowLightManager : MonoBehaviour
{
    public PathCreator pathCreator;

    [SerializeField] private GameObject followLight;
    [SerializeField] private GameObject followLightParent;

    [SerializeField] private float sumLongPathCount;
    [SerializeField] private float distanteEveryFollowLight;

    //private float sumDstTravelled = 0;

    private int followLightCount;

    private FollowPath newFollowLightFollowPath;


    void Awake()
    {
        for(int i = 0; i < (sumLongPathCount/ distanteEveryFollowLight); i++)
        {
            newFollowLightFollowPath = Instantiate(followLight, transform.position, Quaternion.identity, followLightParent.transform).GetComponent<FollowPath>();

            newFollowLightFollowPath.pathCreator = pathCreator;
            newFollowLightFollowPath.dstTravelled = distanteEveryFollowLight * i;
        }
    }


    void Update()
    {
        
    }
}
