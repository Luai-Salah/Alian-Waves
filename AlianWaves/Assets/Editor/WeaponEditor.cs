using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Weapon weapon = (Weapon)target;

		if (weapon.weaponType == WeaponType.Raycasts)
		{
			weapon.effectRate = EditorGUILayout.FloatField("Effect Rate", weapon.effectRate);
			weapon.lineRenderer = (LineRenderer)EditorGUILayout.ObjectField("Trail Prefab", weapon.lineRenderer, typeof(LineRenderer), true);
			//weapon.whatToHit = EditorGUILayout.MaskField("What To Hit", weapon.whatToHit);
		}
		else if (weapon.weaponType == WeaponType.Prefabs)
		{
			weapon.bulletPrefab = (Transform)EditorGUILayout.ObjectField("Bullet Prefab", weapon.bulletPrefab, typeof(Transform), true);
		}
	}
}
