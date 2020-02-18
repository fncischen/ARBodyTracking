using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HumanBodyTrackerLogging : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Skeleton prefab to be controlled.")]
    GameObject m_SkeletonPrefab;

    [SerializeField]
    [Tooltip("The ARHumanBodyManager which will produce body tracking events.")]
    ARHumanBodyManager m_HumanBodyManager;

    /// <summary>
    /// Get/Set the <c>ARHumanBodyManager</c>.
    /// </summary>
    public ARHumanBodyManager humanBodyManager
    {
        get { return m_HumanBodyManager; }
        set { m_HumanBodyManager = value; }
    }

    /// <summary>
    /// Get/Set the skeleton prefab.
    /// </summary>
    public GameObject skeletonPrefab
    {
        get { return m_SkeletonPrefab; }
        set { m_SkeletonPrefab = value; }
    }

    Dictionary<TrackableId, BoneController> m_SkeletonTracker = new Dictionary<TrackableId, BoneController>();

    
    private BoneTracker[] boneTrackers;

    [SerializeField]
    public GameObject WelcomePanel;

    public Button dismissButton;

    [SerializeField]
    public Text loggingText;

    private void Awake()
    {
        dismissButton.onClick.AddListener(DismissWelcomePanel);
    }

    void DismissWelcomePanel()
    {
        WelcomePanel.SetActive(false);
        dismissButton.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        Debug.Assert(m_HumanBodyManager != null, "Human body manager is required.");
        m_HumanBodyManager.humanBodiesChanged += OnHumanBodiesChanged;
    }

    void OnDisable()
    {
        if (m_HumanBodyManager != null)
            m_HumanBodyManager.humanBodiesChanged -= OnHumanBodiesChanged;
    }

    void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs)
    {
        BoneController boneController;
        loggingText.text = "Body Tracking Positions: ";

        foreach (var humanBody in eventArgs.added)
        {
            if (!m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                Debug.Log($"Adding a new skeleton [{humanBody.trackableId}].");
                var newSkeletonGO = Instantiate(m_SkeletonPrefab, humanBody.transform);
                boneController = newSkeletonGO.GetComponent<BoneController>();
                m_SkeletonTracker.Add(humanBody.trackableId, boneController);

            }

            boneController.InitializeSkeletonJoints();
            boneController.ApplyBodyPose(humanBody);

            if (boneTrackers == null)
                {
                    boneTrackers = boneController.skeletonRoot.GetComponentsInChildren<BoneTracker>();
                    foreach(BoneTracker boneTracker in boneTrackers)
                    {
                        loggingText.text += $" Bone: {boneTracker.gameObject.transform.name} LocalRotation : {boneTracker.transform.rotation} \n";
                }

            }


        }

        foreach (var humanBody in eventArgs.updated)
        {
            if (m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                boneController.ApplyBodyPose(humanBody);
            }

            if (boneTrackers != null)
            {
                foreach (BoneTracker boneTracker in boneTrackers)
                {
                    loggingText.text += $" Bone: {boneTracker.gameObject.transform.name} LocalRotation : {boneTracker.transform.rotation} \n";
                }
            }
        }

        foreach (var humanBody in eventArgs.removed)
        {
            Debug.Log($"Removing a skeleton [{humanBody.trackableId}].");
            if (m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                Destroy(boneController.gameObject);
                m_SkeletonTracker.Remove(humanBody.trackableId);
            }
        }
    }
}
