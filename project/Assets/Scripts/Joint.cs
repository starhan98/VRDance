using UnityEngine;

[System.Serializable]
public class Joint
{
    public int x;
    public int y;
    public int z;

    public Vector3 GetVector() {
    	return new Vector3(x, y, z);
    }
}
