using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    private List<Vector3> jointPosData = new List<Vector3>();
    private string jointDataString = "";
    // public bool isSync = false;
    private bool isThereHuman;

    private void InitPosData()
    {
        for(int i  = 0; i < 16; i++){
            jointPosData.Add(new Vector3(0,0,0));
        }
    }

    private void PrintPosData()
    {
        jointDataString = "";
        for(int i = 0; i < 16; i++)
        {
            jointDataString += i.ToString() + " : " + jointPosData[i].ToString() + "\n";
            
        }
        //Debug.Log(jointDataString);
    }

    public void SetPosData(Kinect.Body body)
    {
        jointPosData[0] = GetVector3FromJoint(body.Joints[Kinect.JointType.HandLeft]);
        jointPosData[1] = GetVector3FromJoint(body.Joints[Kinect.JointType.ElbowLeft]);
        jointPosData[2] = GetVector3FromJoint(body.Joints[Kinect.JointType.ShoulderLeft]);
        jointPosData[3] = GetVector3FromJoint(body.Joints[Kinect.JointType.HandRight]);
        jointPosData[4] = GetVector3FromJoint(body.Joints[Kinect.JointType.ElbowRight]);
        jointPosData[5] = GetVector3FromJoint(body.Joints[Kinect.JointType.ShoulderRight]);
        jointPosData[6] = GetVector3FromJoint(body.Joints[Kinect.JointType.FootLeft]);
        jointPosData[7] = GetVector3FromJoint(body.Joints[Kinect.JointType.KneeLeft]);
        jointPosData[8] = GetVector3FromJoint(body.Joints[Kinect.JointType.HipLeft]);
        jointPosData[9] = GetVector3FromJoint(body.Joints[Kinect.JointType.FootRight]);
        jointPosData[10] = GetVector3FromJoint(body.Joints[Kinect.JointType.KneeRight]);
        jointPosData[11] = GetVector3FromJoint(body.Joints[Kinect.JointType.HipRight]);
        jointPosData[12] = GetVector3FromJoint(body.Joints[Kinect.JointType.SpineBase]);
        jointPosData[13] = GetVector3FromJoint(body.Joints[Kinect.JointType.SpineMid]);
        jointPosData[14] = GetVector3FromJoint(body.Joints[Kinect.JointType.SpineShoulder]);
        jointPosData[15] = GetVector3FromJoint(body.Joints[Kinect.JointType.Head]);
        PrintPosData();
    }

    public List<Vector3> GetPosData()
    {
        return jointPosData;
    }

    public bool IsThereHuman()
    {
        return isThereHuman;
    }

    void Start()
    {
        InitPosData();
        isThereHuman = false;
    }
    
    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                    isThereHuman = true;
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
                SetPosData(body);
            }
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.00005f, 0.00005f);
            
            jointObj.transform.localScale = new Vector3(0.0003f, 0.0003f, 0.0003f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
