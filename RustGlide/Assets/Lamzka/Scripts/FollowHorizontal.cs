using UnityEngine;

public class FollowHorizontal : MonoBehaviour
{

    //Change to not be hard refrenced later
    public GameObject HeadRotation;

    //dont touch
    [Header("offset Settings")]
    public float XModify;
    public float YModify;
    public float zModify;



    void Update()
    {
        GetHeadRotation();
        GetHeadHorizontalAxis();
    }

    private void GetHeadRotation()
    {
        Vector3 currentEuler = transform.eulerAngles;
        float targetY = HeadRotation.transform.eulerAngles.y;
        this.transform.rotation = Quaternion.Euler(currentEuler.x, targetY, currentEuler.z);
    }

    private void GetHeadHorizontalAxis()
    {
        float x = HeadRotation.transform.position.x + XModify;
        float y = HeadRotation.transform.position.y + YModify;
        float z = HeadRotation.transform.position.z + zModify;

        this.transform.position = new Vector3(x, y, z);
    }
}
