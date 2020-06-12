using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Initialize Transforms / Slots for Equiping and Carrying Weapons
/// </summary>
public class InitContainersAction : Action
{
    Animator animator;

    PlayerInventory inventory;

    public override void OnInitialize()
    {
        animator = actor.GetComponent<Animator>();

        inventory = (actor.profile as CombatantProfile).inventory as PlayerInventory;

        //Generate and Set Containers
        
        #region Left Hand

        GameObject leftHand = new GameObject(nameof(inventory.leftHand));
        leftHand.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.LeftHand));

        leftHand.transform.localPosition = Vector3.zero;
        leftHand.transform.localRotation = Quaternion.identity;

        inventory.leftHand = leftHand.transform;

        #endregion

        #region Right Hand

        GameObject rightHand = new GameObject(nameof(inventory.rightHand));
        rightHand.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.RightHand));

        rightHand.transform.localPosition = Vector3.zero;
        rightHand.transform.localRotation = Quaternion.identity;

        inventory.rightHand = rightHand.transform;

        #endregion

        #region Ranged Weapon

        GameObject rangedWeapon = new GameObject(nameof(inventory.rangedWeapon));
        rangedWeapon.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.Spine));

        rangedWeapon.transform.localPosition = Vector3.zero;
        rangedWeapon.transform.localRotation = Quaternion.identity;

        inventory.rangedWeapon = rangedWeapon.transform;

        #endregion
    }

    public override void OnAction()
    {
    }
}
