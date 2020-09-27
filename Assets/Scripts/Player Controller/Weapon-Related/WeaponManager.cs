using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]private GameObject[] weaponsPrefabs;
    public GameObject weaponHandlerObject, rayCastPoint;
    [SerializeField] public Weapon currentWeapon;
    public int[] currentWeapons = {0, 1};
    [HideInInspector] public int selectedWeapon = 0;
    int i;
    public int secondsPerAddingGrenade = 5; 

    private int numberOfGrenades = 5; 
    public int NumberOfGrenades
    {
        get
        {
            return this.numberOfGrenades;
        }
        set
        {
            if (value > this.MaxNumberOfGrenades)
            {
                this.numberOfGrenades = this.MaxNumberOfGrenades;

            }
            else if (value < 0)
            {
                this.numberOfGrenades = 0;

            }
            else
            {
                this.numberOfGrenades = value;
            }
        }
    }

    private int maxNumberOfGrenades = 5;
    public int MaxNumberOfGrenades
    {
        get
        {
            return this.maxNumberOfGrenades;
        }
    }

    public void UpdateGrenades()
    {
        this.NumberOfGrenades++;
    }

    public void DecreaseGrenades()
    {
        this.NumberOfGrenades--;
    }


    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("UpdateGrenades", 0, secondsPerAddingGrenade);

        //instantiate weapon prefabs
        foreach (GameObject weapon in weaponsPrefabs){
            if (weapon.transform.GetComponent<Weapon>() != null){
                GameObject obj = Instantiate(weapon, weaponHandlerObject.transform);
                obj.GetComponent<Weapon>().rayCastPoint = rayCastPoint.transform;
            }
        }

        //checking if current weapons have a valid index
        i = 0;
        foreach (int weaponIndex in currentWeapons){
            if (weaponIndex < 0){
                currentWeapons[i] = 0;
            } else if (weaponIndex > weaponHandlerObject.transform.childCount - 1){
                currentWeapons[i] = weaponHandlerObject.transform.childCount - 1;
            }
            i++;
        }

        SelectWeapon(selectedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            NextWeapon();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0){
            PreviousWeapon();
        }
    }

    public void SelectWeapon(int WeaponIndex){
        //checking if index out of range and loop through current weapons
        if (WeaponIndex < 0){
            selectedWeapon = currentWeapons.Length - 1;
        } else if (WeaponIndex > currentWeapons.Length - 1){
            selectedWeapon = 0;
        } else {
            selectedWeapon = WeaponIndex;
        }

        //enabling only selected weapon
        i = 0;
        foreach (Transform weapon in weaponHandlerObject.transform){
            if (i != currentWeapons[selectedWeapon]){
                weapon.gameObject.SetActive(false);
            } else {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.GetComponent<Weapon>();
            }
            i++;
        }

    }

    public void NextWeapon(){
        SelectWeapon(selectedWeapon + 1);
    }

    public void PreviousWeapon(){
        SelectWeapon(selectedWeapon - 1);
    }

    public int GetWeaponIndex(){
        return currentWeapons[selectedWeapon]; 
    }
}
