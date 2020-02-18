using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using System;

public enum BodyPart
{
    Hips,
    LeftUpLeg,
    LeftLeg,
    LeftFoot,
    LeftToes,
    RightUpLeg,
    RightLeg,
    RightFoot,
    Spine1,
    Spine2,
    Spine3,
    Spine4,
    Spine5,
    Spine6,
    Spine7,
    Neck1,
    Neck2,
    Neck3,
    Neck4,
    Head,
    RightShoulder1,
    RightArm,
    RightForearm,
    RightHand,
    LeftShoulder1,
    LeftArm,
    LeftForearm,
    LeftHand
}

[Serializable]
public class BodyPartGroup
{

    public BodyPart bodyPart;

    [Header ("rotationRanges")]

    public bool isLocalRotationX;

    [ConditionalHide("isLocalRotationX", true)]
    public float minLocalRotationX;
    [ConditionalHide("isLocalRotationX", true)]
    public float maxLocalRotationX;

    public bool isLocalRotationY;
    [ConditionalHide("isLocalRotationY", true)]
    public float minLocalRotationY;
    [ConditionalHide("isLocalRotationY", true)]
    public float maxLocalRotationY;

    public bool isLocalRotationZ;
    [ConditionalHide("isLocalRotationZ", true)]
    public float minLocalRotationZ;
    [ConditionalHide("isLocalRotationZ", true)]
    public float maxLocalRotationZ;

}

// 

[CreateAssetMenu(fileName = "BodyPoseEvent", menuName = "ScriptableObjects/BodyPoseEvent")]
public class BodyPoseData : ScriptableObject
{

    public delegate void OnRotationCriteriaMet();
    public OnRotationCriteriaMet onRotationCriteriaMet;

    [SerializeField]
    public CharacterEvent characterEvent; 

    [SerializeField]
    public BodyPartGroup[] bodyPartGroups;

    public void subscribeToCharacterEvent()
    {
        onRotationCriteriaMet += characterEvent.TriggerCharacterEvent;
    }

    public void unsubscribeToCharacterEvent()
    {
        onRotationCriteriaMet -= characterEvent.TriggerCharacterEvent;

    }

}


//[CustomEditor(typeof(BodyPoseData))]
//public class BodyPoseEditor : Editor
//{
//    // include checkboxes in our user interface
//    // these checkboxes will indicate which functions to subscribe to!

//    SerializedProperty bodyPartGroups;
    

//    // 
//    private void OnEnable()
//    {
//        bodyPartGroups = serializedObject.FindProperty("bodyPartGroups");

//    }

//    public override void OnInspectorGUI()
//    {
//        // Debug.Log(bodyPartGroups.ToString());
//        EditorGUILayout.PropertyField(bodyPartGroups, true);
//        Debug.Log(bodyPartGroups.arraySize);

//        //
//        for(int i = 0; i < bodyPartGroups.arraySize; i++)
//        {
//            SerializedProperty b = bodyPartGroups.GetArrayElementAtIndex(i);
//            // int index = b.FindPropertyRelative("bodyPart").enumValueIndex;
//            // Debug.Log(b.FindPropertyRelative("bodyPart").enumNames[index]);

//            EditorGUILayout.BeginVertical();
//            bool isLocalRotationX = b.FindPropertyRelative("isLocalRotationX").boolValue;
//            bool isLocalRotationY = b.FindPropertyRelative("isLocalRotationY").boolValue;
//            bool isLocalRotationZ = b.FindPropertyRelative("isLocalRotationZ").boolValue;

//            // isLocalRotationX = EditorGUILayout.Toggle(isLocalRotationX);
//            // isLocalRotationY = EditorGUILayout.Toggle(isLocalRotationY);
//            // isLocalRotationZ = EditorGUILayout.Toggle(isLocalRotationZ);
//            //}

//            float minLocalRotationX = b.FindPropertyRelative("minLocalRotationX").floatValue;
//            float maxLocalRotationX = b.FindPropertyRelative("maxLocalRotationX").floatValue;
//            float minLocalRotationY = b.FindPropertyRelative("minLocalRotationY").floatValue;
//            float maxLocalRotationY = b.FindPropertyRelative("maxLocalRotationY").floatValue;
//            float minLocalRotationZ = b.FindPropertyRelative("minLocalRotationZ").floatValue;
//            float maxLocalRotationZ = b.FindPropertyRelative("maxLocalRotationZ").floatValue;

//            //bool issLocalRotationX = EditorGUILayout.Toggle("hasLocalRotationX", isLocalRotationX);
//            //bool issLocalRotationY = EditorGUILayout.Toggle("hasLocalRotationY", body.isLocalRotationY);
//            //bool issLocalRotationZ = EditorGUILayout.Toggle("hasLocalRotationZ", bpg.isLocalRotationZ);

//            if (isLocalRotationX)
//            {
//                Debug.Log("Test");
//                minLocalRotationX = EditorGUILayout.FloatField("minLocalRotationX", minLocalRotationX);
//                maxLocalRotationX = EditorGUILayout.FloatField("maxLocalRotationX", maxLocalRotationX);
//            }
//            // if localRotationX is checked, then return this section 

//            // situation B)
//            if (isLocalRotationY)
//            {
//                Debug.Log("Test");
//                minLocalRotationY = EditorGUILayout.FloatField("minLocalRotationY", minLocalRotationY);
//                maxLocalRotationY = EditorGUILayout.FloatField("maxLocalRotationY", maxLocalRotationY);
//            }
//            // if localRotationY is checked, then return this section

//            // situation C)
//            // if localRotationZ is checked, then return this section
//            if (isLocalRotationZ)
//            {
//                Debug.Log("Test");

//                minLocalRotationZ = EditorGUILayout.FloatField("minLocalRotationZ", minLocalRotationZ);
//                maxLocalRotationZ = EditorGUILayout.FloatField("maxLocalRotationZ", maxLocalRotationZ);

//            }

//            EditorGUILayout.EndVertical();


//        }

//        //int i = 1;
//        //while (bodyPartGroups.Next(true))
//        //{
//        //    Debug.Log("Test");
//        //    Debug.Log(bodyPartGroups.name);
//        //    i += 1; 
//        //}

//        //    while(bodyPartGroups.Next(true))
//        //    {

//        //    SerializedProperty minLocalRotationX;
//        //    SerializedProperty maxLocalRotationX;
//        //    SerializedProperty minLocalRotationY;
//        //    SerializedProperty maxLocalRotationY;
//        //    SerializedProperty minLocalRotationZ;
//        //    SerializedProperty maxLocalRotationZ;

//        //    minLocalRotationX = serializedObject.FindProperty("MinLocalRotationX");
//        //    maxLocalRotationX = serializedObject.FindProperty("MaxLocalRotationX");

//        //    minLocalRotationY = serializedObject.FindProperty("MinLocalRotationY");
//        //    maxLocalRotationY = serializedObject.FindProperty("MaxLocalRotationY");

//        //    minLocalRotationZ = serializedObject.FindProperty("MinLocalRotationZ");
//        //    maxLocalRotationZ = serializedObject.FindProperty("MaxLocalRotationZ");

//        //    bool issLocalRotationX = EditorGUILayout.Toggle("hasLocalRotationX", bodyPartGroups.isLocalRotationX);
//        //    bool issLocalRotationY = EditorGUILayout.Toggle("hasLocalRotationY", bodyPartGroups.isLocalRotationY);
//        //    bool issLocalRotationZ = EditorGUILayout.Toggle("hasLocalRotationZ", bpg.isLocalRotationZ);
//        //}


//        serializedObject.ApplyModifiedProperties();


//    }
//}