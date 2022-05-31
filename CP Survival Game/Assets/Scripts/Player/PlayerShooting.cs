using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ImpactStone;
    [SerializeField] public GameObject ImpactMetal;  
    [SerializeField] public Transform MuzzleSpawn;
    [SerializeField] Transform PistolMuzzleSpawn;
    [SerializeField] public GameObject[] MuzzleFlash;
    [SerializeField] public GameObject BloodEffect;
    public GameObject aim;
    RaycastHit hit;
    public GameObject[] gun;
    public IKPlacement iK;

    private bool RapidPlay = true;
    private bool FireFuel = false;
    public GameObject GrenadeExplosion;
    public GameObject GrenadeSmoke;
    public GameObject Flames;

    public GameObject Bullet;
    public AudioSource shootsound;
    public AudioClip[] clip;
    public bool CanFire;
    public float FireRate = 0f;
    public int gunno;

    public int xp;

    // Start is called before the first frame update
    void Start()
    {
        iK = GetComponent<IKPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();

    }

    void Shooting()
    {
        for(gunno = 0; gunno < gun.Length; gunno++)
        {
            if (gun[gunno].activeInHierarchy)
            {
                if(iK.gunno == 0)
                {
                    if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
                    {

                        if (SaveScript.AmmoAmt > 0)
                        {
                        //Instantiate(MuzzleFlash, PistolMuzzleSpawn.position, MuzzleSpawn.rotation);
                        MuzzleFlash[iK.gunno].SetActive(true);
                        StartCoroutine(Fire());
                        SaveScript.AmmoAmt -= 1;
                        shootsound.clip = clip[0];
                        shootsound.loop = false;
                        shootsound.pitch = 1;
                        shootsound.Play();
                        //PlayerAudio.clip = SingleShotSound;
                        //PlayerAudio.loop = false;
                        //PlayerAudio.pitch = 1;
                        //PlayerAudio.Play();

                        Hits();
                        }
                    }
                }
                else if(iK.gunno == 1)
                {
                    if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
                    {

                        //if (SaveScript.AmmoAmt > 0)
                        //{
                        //Instantiate(MuzzleFlash, PistolMuzzleSpawn.position, MuzzleSpawn.rotation);
                        if (SaveScript.SMGAmmo > 0)
                        {
                            MuzzleFlash[iK.gunno].SetActive(true);
                            StartCoroutine(Fire());
                            SaveScript.AmmoAmt -= 1;
                            if (RapidPlay == true)
                            {
                                RapidPlay = false;
                                shootsound.clip = clip[1];
                                shootsound.loop = true;
                                shootsound.pitch = 3;
                                shootsound.Play();
                            }

                            Hits();
                        }
                           
                            //PlayerAudio.clip = SingleShotSound;
                            //PlayerAudio.loop = false;
                            //PlayerAudio.pitch = 1;
                            //PlayerAudio.Play();

                            
                        //}
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        shootsound.Stop();
                        RapidPlay = true;
                    }
                }
                else if (iK.gunno == 2)
                {
                    if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
                    {
                        if (SaveScript.GrenadeAmmo > 0)
                        {
                            Instantiate(GrenadeSmoke, MuzzleSpawn.position, MuzzleSpawn.rotation);

                            SaveScript.GrenadeAmmo -= 1;

                            shootsound.clip = clip[2];
                            shootsound.loop = false;
                            shootsound.pitch = 1;
                            shootsound.PlayDelayed(0.3f);
                            if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, 1000))
                            {
                                StartCoroutine(Grenade());
                            }
                        }      
                    }                       
                }
                else if (iK.gunno == 3)
                {
                    if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
                    {
                        if (SaveScript.FlameAmmo > 0)
                        {
                            Flames.gameObject.SetActive(true);

                            if (RapidPlay == true)
                            {
                                RapidPlay = false;
                                FireFuel = true;
                                shootsound.clip = clip[3];
                                shootsound.loop = true;
                                shootsound.pitch = 0.1f;
                                shootsound.Play();
                            }
                        }
                           
                    }
                        
                }
                if (SaveScript.FlameAmmo <= 0)
                {
                    FireFuel = false;
                    Flames.gameObject.SetActive(false);
                    shootsound.Stop();

                }

                if (FireFuel == true)
                {
                    SaveScript.FlameAmmo -= 3 * Time.deltaTime;
                }
            }
        }   
    }

    public void ShootMobile()
    {
        if (iK.gun)
        {
            if (iK.gunno == 0)
            {
                if (SaveScript.AmmoAmt > 0)
                {
                    MuzzleFlash[iK.gunno].SetActive(true);
                    StartCoroutine(Fire());
                    SaveScript.AmmoAmt -= 1;
                    shootsound.clip = clip[0];
                    shootsound.loop = false;
                    shootsound.pitch = 1;
                    shootsound.Play();
                    //PlayerAudio.clip = SingleShotSound;
                    //PlayerAudio.loop = false;
                    //PlayerAudio.pitch = 1;
                    //PlayerAudio.Play();

                    Hits();
                }
                    
            }
            else if(iK.gunno == 1)
            {
                //if (SaveScript.AmmoAmt > 0)
                //{
                //Instantiate(MuzzleFlash, PistolMuzzleSpawn.position, MuzzleSpawn.rotation);
                if (SaveScript.SMGAmmo > 0)
                {
                    MuzzleFlash[iK.gunno].SetActive(true);
                    StartCoroutine(Fire());
                    SaveScript.AmmoAmt -= 1;
                    if (RapidPlay == true)
                    {
                        RapidPlay = false;
                        shootsound.clip = clip[1];
                        shootsound.loop = true;
                        shootsound.pitch = 3;
                        shootsound.Play();


                    }
                }

                if (SaveScript.SMGAmmo <= 0)
                {
                    shootsound.Stop();
                }

                //PlayerAudio.clip = SingleShotSound;
                //PlayerAudio.loop = false;
                //PlayerAudio.pitch = 1;
                //PlayerAudio.Play();
                InvokeRepeating("Hits", Time.deltaTime, 0.1f);
                //Hits();
                //}

            }
            else if(iK.gunno == 2)
            {
                if (SaveScript.GrenadeAmmo > 0)
                {
                    Instantiate(GrenadeSmoke, MuzzleSpawn.position, MuzzleSpawn.rotation);

                    SaveScript.GrenadeAmmo -= 1;

                    shootsound.clip = clip[2];
                    shootsound.loop = false;
                    shootsound.pitch = 1;
                    shootsound.Play();
                    if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, 1000))
                    {
                        StartCoroutine(Grenade());
                    }

                    Hits();
                }
                   
            }
            else if(iK.gunno == 3)
            {
                if (SaveScript.FlameAmmo > 0)
                {
                    Flames.gameObject.SetActive(true);

                    if (RapidPlay == true)
                    {
                        RapidPlay = false;
                        FireFuel = true;
                        shootsound.clip = clip[3];
                        shootsound.loop = true;
                        shootsound.pitch = 0.1f;
                        shootsound.Play();
                    }

                    Hits();
                }

                   
            }

            if (SaveScript.FlameAmmo <= 0)
            {
                FireFuel = false;
                Flames.gameObject.SetActive(false);
                shootsound.Stop();

            }

            if (FireFuel == true)
            {
                SaveScript.FlameAmmo -= 3 * Time.deltaTime;
            }
        }
    } 
    
    public void StopShooting()
    {
        Flames.gameObject.SetActive(false);
        shootsound.Stop();
        if (RapidPlay == false)
        {
            FireFuel = false;

        }

        RapidPlay = true;
        CancelInvoke("Hits");
    }

    public void PickupSound()
    {
        shootsound.clip = clip[4];
        shootsound.loop = false;
        shootsound.pitch = 0.7f;
        shootsound.Play();
    }
    public void Hits()
    {

        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, 1000))
        {
            if (hit.transform.tag == "Stone")
            {
                Instantiate(ImpactStone, hit.point, Quaternion.LookRotation(hit.normal));
            }
            if (hit.transform.tag == "Metal")
            {
                Instantiate(ImpactMetal, hit.point, Quaternion.LookRotation(hit.normal));
            }
            if (hit.transform.tag == "ExplodingBarrel")
            {
                hit.transform.gameObject.SendMessage("Explode");
            }
            if(hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Animator>().SetTrigger("Kick");
                hit.transform.GetComponent<AIHealthManager>().health--;
                if (hit.transform.GetComponent<AIHealthManager>().health == 0)
                {
                    hit.transform.GetComponent<Animator>().SetBool("Death", true);
                }
                
                Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    IEnumerator Fire()
    {
        GameObject newBullet = BulletPool.Reference.GetBulletFromPool();//(GameObject)Instantiate(Bullet, MuzzleSpawn.transform.position, transform.rotation);
        //Instantiate(MuzzleFlash, MuzzleSpawn.position, MuzzleSpawn.rotation);
        MuzzleFlash[iK.gunno].SetActive(true);
        newBullet.GetComponent<Rigidbody>().AddForce(10000 * transform.forward);
        /*if (iK.gunno == 0)
            shootsound.PlayOneShot(clip[0]);
        else
        {
            shootsound.PlayOneShot(clip[1]);
        }*/
        yield return new WaitForSeconds(FireRate);
        CanFire = true;
    }

    public void RootMotionOn()
    {
        GetComponent<Animator>().applyRootMotion = true;
    }

    public void RootMotionOff()
    {
        GetComponent<Animator>().applyRootMotion = false;
    }

    IEnumerator Grenade()
    {
        yield return new WaitForSeconds(0.3f);

        Instantiate(GrenadeExplosion, hit.point, Quaternion.LookRotation(hit.normal));

        if (hit.transform.tag == "ExplodingBarrel")
        {
            hit.transform.gameObject.SendMessage("Explode");
        }
    }

    public void PistolAmmo()
    {
        SaveScript.AmmoAmt += 10f;
        PickupSound();
    }

    public void SMGAmmo()
    {
        SaveScript.AmmoAmt += 10f;
        SaveScript.SMGAmmo += 30f;
        PickupSound();
    }    

    public void GrenadeAmmo()
    {
        SaveScript.AmmoAmt += 10f;
        SaveScript.SMGAmmo += 30f;
        SaveScript.GrenadeAmmo += 3f;
        PickupSound();
    }

    public void FlameAmmo()
    {
        SaveScript.AmmoAmt += 10f;
        SaveScript.SMGAmmo += 30f;
        SaveScript.GrenadeAmmo += 3f;
        SaveScript.FlameAmmo += 50f;
        PickupSound();
    }

}
