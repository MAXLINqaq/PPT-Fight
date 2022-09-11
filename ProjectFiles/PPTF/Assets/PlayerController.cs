using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float swingSpeedSTD;
    public float AngleA;
    public float AngleB;

    public Transform weaponA;
    public Transform weaponB;
    public HingeJoint2D ArmA;
    public HingeJoint2D ArmB;

    private float totalWeightWeaponA;
    private float totalWeightWeaponB;
    public float weaponAngleA;
    public float weaponAngleB;
    JointMotor2D motor2D = new JointMotor2D();

    // Start is called before the first frame update
    void Start()
    {
        totalWeightWeaponA = weaponA.gameObject.GetComponent<weaponAStatus>().totalWeight;
        totalWeightWeaponB = weaponB.gameObject.GetComponent<weaponBStatus>().totalWeight;
        motor2D.maxMotorTorque = 10000;
    }

    // Update is called once per frame
    private void Update()
    {
        weaponAngleA = transform.localEulerAngles.z + weaponA.transform.localEulerAngles.z;
        weaponAngleB = transform.localEulerAngles.z + weaponB.transform.localEulerAngles.z;
        if (weaponAngleA > 360)
        {
            weaponAngleA -= 360;
        }
        if (weaponAngleB > 360)
        {
            weaponAngleB -= 360;
        }

        if (AngleA == 360)
        {
            AngleA = 0;
        }
        if (AngleB == 360)
        {
            AngleB = 0;
        }


        if (AngleA != 0)
        {
            SwingWeapon(totalWeightWeaponA, weaponAngleA, AngleA, ArmA);
        }
        else
        {
            SetMotorSpeedZero(ArmA);
        }
        if (AngleB != 0)
        {
            SwingWeapon(totalWeightWeaponB, weaponAngleB, AngleB, ArmB);
        }
        else
        {
            SetMotorSpeedZero(ArmB);
        }
    }
    private void FixedUpdate()
    {




    }
    public void WeaponOne(InputAction.CallbackContext value)
    {
        Vector2 valueVector2 = value.ReadValue<Vector2>();
        if (valueVector2.x > 0)
        {
            AngleA = Vector2.Angle(valueVector2, new Vector2(0, -1));
        }
        else
        {
            AngleA = 360 - Vector2.Angle(valueVector2, new Vector2(0, -1));
        }
    }
    public void WeaponTwo(InputAction.CallbackContext value)
    {
        Vector2 valueVector2 = value.ReadValue<Vector2>();
        if (valueVector2.x > 0)
        {
            AngleB = Vector2.Angle(valueVector2, new Vector2(0, -1));
        }
        else
        {
            AngleB = 360 - Vector2.Angle(valueVector2, new Vector2(0, -1));
        }
    }
    private void SwingWeapon(float totalWeight, float weaponAngle, float Angle, HingeJoint2D Arm)
    {

        if (weaponAngle > Angle)
        {
            if (weaponAngle - Angle > 180)
            {
                motor2D.motorSpeed = GetSpeed(totalWeight);
            }
            else
            {
                motor2D.motorSpeed = -GetSpeed(totalWeight);
            }
        }
        else
        {

            if (Angle - weaponAngle < 180)
            {
                motor2D.motorSpeed = GetSpeed(totalWeight);
            }
            else
            {
                motor2D.motorSpeed = -GetSpeed(totalWeight);
            }
        }

        if (Mathf.Abs(weaponAngle - Angle) < 10)
        {
            motor2D.motorSpeed = 0;
        }
        Arm.motor = motor2D;

    }
    private float GetSpeed(float totalWeight)
    {
        float swingSpeed;
        float totalWeightSTD = 16;
        swingSpeed = swingSpeedSTD * totalWeightSTD / totalWeight;
        return swingSpeed;
    }
    private void SetMotorSpeedZero(HingeJoint2D Arm)
    {
        Arm.useMotor = false;
    }
}
