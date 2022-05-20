using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public int selcetedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelctedWeapon = selcetedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selcetedWeapon >= transform.childCount - 1)
                selcetedWeapon = 0;
            else
                selcetedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selcetedWeapon <= 0)
                selcetedWeapon = transform.childCount - 1;
            else
                selcetedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selcetedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selcetedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selcetedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selcetedWeapon = 3;
        }

        if (previousSelctedWeapon != selcetedWeapon)
        {
            SelectWeapon();
        }

    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selcetedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
