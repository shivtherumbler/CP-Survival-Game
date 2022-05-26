using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IKPlacement : MonoBehaviour
{
    public Animator anim;
    public bool gun;
    public Transform[] aiming;
    public GameObject[] weapon;
    public GameObject[] lefthand;
    public GameObject[] righthand;
    [Range(0, 1f)]
    public float rightreach;
    [Range(0, 1f)]
    public float leftreach;
    public CinemachineVirtualCamera[] cam;
    float turn;
    public GameObject reticle;
    public PlayerShooting shooting;
    public bool mobile;
    public bool aim;
    public int gunno;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    [Tooltip("How far in degrees can you move the camera up")]
    public float LeftClamp = 70.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float RightClamp = -30.0f;
    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;
    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    private StarterAssets.StarterAssetsInputs _input;
    private const float _threshold = 0.01f;
    private UnityEngine.InputSystem.PlayerInput _playerInput;
    private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        _playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        shooting = GetComponent<PlayerShooting>();
    }


    private void LateUpdate()
    {
        CameraRotation();
    }


    // Update is called once per frame
    void Update()
    {
        weapon = shooting.gun;

        if(mobile == false)
        Shoot();
        else
        {
            AimShoot();
        }

        if (gun == true)
        {
            weapon[gunno].SetActive(true);
            for(int i = 0; i < weapon.Length; i++)
            {
                if(i != gunno)
                weapon[i].SetActive(false);
            }
            
        }
        else
        {
            weapon[gunno].SetActive(false);

        }

        //AimShoot();
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            gun = !gun;
        }
        
        if(weapon[gunno].activeInHierarchy)
        {
            if(Input.GetMouseButton(1))
            {
                
                weapon[gunno].transform.position = Vector3.Lerp(weapon[gunno].transform.position, aiming[1].position, Time.deltaTime * 10);
                weapon[gunno].transform.rotation = Quaternion.Lerp(weapon[gunno].transform.rotation, aiming[1].rotation, Time.deltaTime * 10);
                reticle.SetActive(true);

                cam[0].Priority = 9;
                //cam[1].gameObject.SetActive(true);
                //cam[0].transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
                cam[1].Priority = 11;
                //cam[0].gameObject.SetActive(false);
                //turn += Input.GetAxis("Mouse X");
                transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);//(0, turn, 0);
            }
            else
            {
                
                weapon[gunno].transform.position = Vector3.Lerp(weapon[gunno].transform.position, aiming[0].position, Time.deltaTime * 10);
                weapon[gunno].transform.rotation = Quaternion.Lerp(weapon[gunno].transform.rotation, aiming[0].rotation, Time.deltaTime * 10);
                reticle.SetActive(false);
                
                cam[1].Priority = 9;
                //cam[0].gameObject.SetActive(true);
                
                cam[0].Priority = 11;
                //cam[1].gameObject.SetActive(false);

                //transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
            }
            if(Input.GetMouseButtonUp(1))
            {
                transform.localRotation = Quaternion.Euler(0, _cinemachineTargetYaw, 0.0f);
                
                //
            }
        }
       
    }

    public void Aim()
    {
        gun = !gun;
        
    }

    public void AimShoot()
    {
        if(aim)
        {
            weapon[gunno].transform.position = Vector3.Lerp(weapon[gunno].transform.position, aiming[1].position, Time.deltaTime * 10);
            weapon[gunno].transform.rotation = Quaternion.Lerp(weapon[gunno].transform.rotation, aiming[1].rotation, Time.deltaTime * 10);
            reticle.SetActive(true);

            cam[0].Priority = 9;
            //cam[1].gameObject.SetActive(true);
            //cam[0].transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
            cam[1].Priority = 11;
            //cam[0].gameObject.SetActive(false);
            //turn += Input.GetAxis("Mouse X");
            transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);//(0, turn, 0);
        }
        else
        {
            weapon[gunno].transform.position = Vector3.Lerp(weapon[gunno].transform.position, aiming[0].position, Time.deltaTime * 10);
            weapon[gunno].transform.rotation = Quaternion.Lerp(weapon[gunno].transform.rotation, aiming[0].rotation, Time.deltaTime * 10);
            reticle.SetActive(false);

            cam[1].Priority = 9;
            //cam[0].gameObject.SetActive(true);

            cam[0].Priority = 11;
            //cam[1].gameObject.SetActive(false);

            //transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
        }


    }

    public void AimReset()
    {
        aim = !aim;
        
    }

    public void ShootMobile()
    {
        if(gun)
        {
            shooting.ShootMobile();
        }  
    }

    public void ResetRot()
    {
        transform.localRotation = Quaternion.Euler(0, _cinemachineTargetYaw, 0.0f);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        rightreach = anim.GetFloat("RightHandReach");
        leftreach = anim.GetFloat("LeftHandReach");
        if (weapon[gunno].activeInHierarchy)
        {
            if (anim.GetFloat("RightHandReach") <= 1f)
                anim.SetFloat("RightHandReach", rightreach += Time.deltaTime * 5);
            if (anim.GetFloat("LeftHandReach") <= 1f)
                anim.SetFloat("LeftHandReach", leftreach += Time.deltaTime * 5);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthand[gunno].transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftreach);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lefthand[gunno].transform.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftreach);

            anim.SetIKPosition(AvatarIKGoal.RightHand, righthand[gunno].transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightreach);
            anim.SetIKRotation(AvatarIKGoal.RightHand, righthand[gunno].transform.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightreach);
        }
        else
        {
            if (anim.GetFloat("RightHandReach") >= 0f)
                anim.SetFloat("RightHandReach", rightreach -= Time.deltaTime * 5);
            if (anim.GetFloat("LeftHandReach") >= 0f)
                anim.SetFloat("LeftHandReach", leftreach -= Time.deltaTime * 5);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthand[gunno].transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftreach);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lefthand[gunno].transform.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftreach);

            anim.SetIKPosition(AvatarIKGoal.RightHand, righthand[gunno].transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightreach);
            anim.SetIKRotation(AvatarIKGoal.RightHand, righthand[gunno].transform.rotation);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightreach);
        }
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
       // if(Input.GetMouseButtonDown(1))
        //{
            //_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, RightClamp, LeftClamp);//float.MinValue, float.MaxValue);
        //}
       //else
        //{
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        //}
        
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
