using UnityEngine;

public class GunHolster : MonoBehaviour, IGunGetState
{

    [Header("offset Settings")]
    public float XModify;
    public float YModify;
    public float zModify;


    private GameObject CurrentGun;



    public void NotifyGrab(bool IsGunGrabbed)
    {
        if (!IsGunGrabbed)
        {
            SetGunToHolster();
        }
    }

    void Start()
    {
        GetGun();
        SetGunToHolster();
    }

    void Update()
    {

    }

    private void GetGun()
    {
        CurrentGun = GameObject.FindWithTag("Gun");
        CurrentGun.GetComponent<GunSubject>().AddObserver(this);

    }

    private void SetGunToHolster()
    {
        CurrentGun.transform.position = this.transform.position;
        CurrentGun.transform.rotation = Quaternion.Euler(0, 0, -90);
        CurrentGun.transform.parent = this.transform;
    }

    /* private void SetOffset()
     {
         float x = this.transform.position.x + XModify;
         float y = this.transform.position.y + YModify;
         float z = this.transform.position.z + zModify;  

         this.transform.position = new Vector3(x, y, z);
     }*/
}
