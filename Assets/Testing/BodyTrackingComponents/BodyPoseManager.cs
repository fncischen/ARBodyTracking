using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using System;

[Serializable]
public class BodyPoseEvent {
    public BodyPoseData bodyPoseData;
    public VisualEffect visualEffect;
    public bool isVisualEffectPlaying = false; 
}


public class BodyPoseManager : MonoBehaviour
{
    public BoneController boneController;

    [SerializeField]
    public BodyPoseEvent[] bodyPoseEvents;

    public void OnEnable()
    {
    }

    public void OnDisable()
    {
    }

    // the goal is to get the transform at a certain event 

    public void Update()
    {
        checkBodyTrackers();
    }

    public void checkBodyTrackers()
    {
        // loop through each body tracker
        foreach (BodyPoseEvent bpe in bodyPoseEvents)
        {
            BodyPoseData bpd = bpe.bodyPoseData;
            BodyPartGroup[] bpgs = bpd.bodyPartGroups;

            // loop through each bodyPartGroup and retrieve all relevant ranges
            bool meetsCriteria = true;
            // check if the bodytrackers are within this rotation
            foreach (BodyPartGroup bpg in bpgs)
            {
                String bodyPartName = bpg.bodyPart.ToString();// get the enum type

                // search for the Bone from the BoneController
                Transform bodyPart;
                BoneFromBodyPart(bodyPartName, out bodyPart);
                //Debug.Log(bodyPart);
                Quaternion currentRotation = bodyPart.localRotation;
                Debug.Log("Angle at " + bodyPartName + " " + currentRotation.eulerAngles);

                if (bpg.isLocalRotationX)
                {
                    if (currentRotation.eulerAngles.x < bpg.minLocalRotationX || currentRotation.eulerAngles.x > bpg.maxLocalRotationX)
                    {
                        meetsCriteria = false;
                        break;
                    }
                    // convert degrees to quanterion values
                }

                if (bpg.isLocalRotationY)
                {
                    // convert degrees to quanterion values

                    if (currentRotation.eulerAngles.y < bpg.minLocalRotationY || currentRotation.eulerAngles.y > bpg.maxLocalRotationY)
                    {
                        meetsCriteria = false;
                        break;
                    }

                }

                if (bpg.isLocalRotationZ)
                {
                    // convert degrees to quanterion values

                    if (currentRotation.eulerAngles.z < bpg.minLocalRotationZ || currentRotation.eulerAngles.z > bpg.maxLocalRotationZ)
                    {
                        meetsCriteria = false;
                        break;
                    }


                }


            }


            //
            Debug.Log(bpe.bodyPoseData.name + " this meets criteria"  + meetsCriteria);

            if (meetsCriteria & !bpe.isVisualEffectPlaying)
            {
                Debug.Log(bpe.bodyPoseData.name +  " Meets criteria!");
                bpe.isVisualEffectPlaying = true; 
                bpe.visualEffect.Play();
                // send the event to the appropriate character event attached to the bodyposeData 
            }

            else if (!meetsCriteria & bpe.isVisualEffectPlaying)
            {
                Debug.Log(bpe.bodyPoseData.name + "will stop playing!");
                bpe.isVisualEffectPlaying = false; 
                bpe.visualEffect.Stop();
            }
            else
            {
                Debug.Log(bpe.bodyPoseData.name + "VFX will not play");
            }

        }

    }

    private void BoneFromBodyPart(String boneName, out Transform bone)
    {
        bone = null; 
        // Debug.Log("Bone Name: " + boneName);

        Queue<Transform> nodes = new Queue<Transform>();
        nodes.Enqueue(boneController.skeletonRoot);
        while (nodes.Count > 0)
        {
            Transform next = nodes.Dequeue();
            for (int i = 0; i < next.childCount; ++i)
            {
                if (next.name == boneName)
                {
                    bone = next;
                    break;
                }
                nodes.Enqueue(next.GetChild(i));
            }
        }

        // Debug.Log("Bone found! " + bone.name);
    }
}
