using UnityEngine;

[System.Serializable]
public class Joint
{
    public float x;
    public float y;
    public float z;

    public Joint(float _x, float _y, float _z) {
    	x = _x;
    	y = _y;
    	z = _z;
    }

    public Vector3 GetVector() {
    	return new Vector3(x, y, z);
    }
}
