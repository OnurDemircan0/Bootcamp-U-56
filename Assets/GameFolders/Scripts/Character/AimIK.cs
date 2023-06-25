using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float weight;
}
public class AimIK : MonoBehaviour
{
    public Transform TargetTransform;
    public Transform AimTransform;

    public int iterations = 10;
    [Range(0f,1f)]
    public float Weight = 1.0f;

    public float angleLimit = 90.0f;
    public float distanceLimit = 1.5f;

    public HumanBone[] humanBones;
    Transform[] boneTransforms;

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for (int i = 0; 1 < boneTransforms.Length; i++)
        {
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
    }

    Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = TargetTransform.position - AimTransform.position;
        Vector3 aimDirection = AimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if(targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return AimTransform.position + direction;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if( AimTransform == null)
        {
            return;
        }

        if (TargetTransform == null)
        {
            return;
        }


        Vector3 targetPosition = GetTargetPosition();
        for(int i =0; i < iterations; i++)
        {
            for(int b = 0; b < boneTransforms.Length; b++)
            {
                Transform Bone = boneTransforms[b];
                float boneWeight = humanBones[b].weight * Weight;
                AimAtTarget(Bone, targetPosition, boneWeight);
            }
            
        }      
    }

    void AimAtTarget(Transform bone, Vector3 targetPosition, float Weight)
    {
        Vector3 aimDirection = AimTransform.forward;
        Vector3 targetDirection = targetPosition - AimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, Weight);

        bone.rotation = blendedRotation * bone.rotation;
    }

    public void SetTargetTransform(Transform Target)
    {
        TargetTransform = Target;
    }

    public void SetAimTransform(Transform Aim)
    {
        AimTransform = Aim;
    }
}
