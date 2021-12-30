using UnityEngine;

public class Center : MonoBehaviour
{
    public Transform target;

    [ContextMenu("Center Transform")]
    void CenterTransform()
    {
        var chs = this.transform.childCount;
        Transform[] children = new Transform[chs];
        for (int i = 0; i < chs; i++)
        {
            children[i] = this.transform.GetChild(i);
        }
        foreach (var e in children)
        {
            e.parent = null;
        }
        this.transform.position = target.position;
        foreach (var e in children)
        {
            e.parent = this.transform;
        }
        this.transform.position = Vector3.zero;
    }
}