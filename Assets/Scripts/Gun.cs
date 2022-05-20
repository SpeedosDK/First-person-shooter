using UnityEngine;
using System.Collections;



    public class Gun : MonoBehaviour
{

        public float damage = 10f;
        public float range = 100f;
        public float fireRate = 15f;
        public Camera fpsCam;
        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;
        private float nextTimeToFire = 0f;
        public float impactForce = 30f;
        private bool isReloading = false;
        public int maxAmmo = 30;
        private int currentAmmo;
        public float reloadTime = 1f;

    public Recoil Recoil_Script; 

        public Animator animator;

        private UIManager uiManager;

    



        private void Start()
        {

            {
                currentAmmo = maxAmmo;
            }

            uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            Recoil_Script =GameObject.FindObjectOfType<Recoil>();

        }

        private void OnEnable()
        {
            isReloading = false;
            animator.SetBool("Reloading", false);
        }
        // Update is called once per frame
        void Update()
        {
            if (isReloading)
                return;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        IEnumerator Reload()
        {

            isReloading = true;
            Debug.Log("Reloading...");

            animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(reloadTime - .25f);
            animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(reloadTime - .25f);
            currentAmmo = maxAmmo;
            uiManager.UpdateAmmo(currentAmmo);
            isReloading = false;
        }
        void Shoot()
        {
            muzzleFlash.Play();

            currentAmmo--;
            uiManager.UpdateAmmo(currentAmmo);


            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {


                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                Recoil_Script.RecoilFire();
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);




            }

        }



}

