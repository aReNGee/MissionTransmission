using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour {

	public string primaryTarget;

	public string secondaryTarget;

	GameObject target;

	GameObject[] targetList;

	public bool hasSecondaryTarget;

	public enum targetType {
		primary,
		secondary,
		none
	}

	public targetType TT;

	// Use this for initialization
	public void checkForTargets(){
		targetList = GameObject.FindGameObjectsWithTag(primaryTarget);
		if (targetList.Length != 0){
			TT = targetType.primary;
		}
		else if (secondaryTarget != "" && hasSecondaryTarget){
			targetList = GameObject.FindGameObjectsWithTag(secondaryTarget);
			if (targetList.Length != 0){
				TT = targetType.secondary;
			}
			else {
				TT = targetType.secondary;
			}
		}
		else {
			TT = targetType.none;
		}
	}

	public bool targetAvailable(){
		if (TT == targetType.none){
			return false;
		}
		else {
			return true;
		}
	}

	public GameObject getNearestTarget(Vector3 pos){
		float shortDist = Mathf.Infinity;
		GameObject targetToReturn = null;
		foreach (GameObject g in targetList){
			float tempSD = Vector3.Magnitude(g.transform.position - pos);
			if (tempSD < shortDist){
				shortDist = tempSD;
				targetToReturn = g;
			}
		}
		return targetToReturn;
	}

	public bool checkForPrimaryTargets(){
		GameObject[] g = GameObject.FindGameObjectsWithTag(primaryTarget);
		if (g.Length != 0){
			return true;
		}
		else {
			return false;
		}
	}
}
