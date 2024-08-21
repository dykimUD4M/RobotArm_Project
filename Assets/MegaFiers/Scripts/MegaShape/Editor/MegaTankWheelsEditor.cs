
using UnityEngine;
using UnityEditor;

namespace MegaFiers
{
	[CustomEditor(typeof(MegaTankWheels))]
	public class MegaTankWheelsEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
		}

	#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5 || UNITY_5_6 || UNITY_2017_1_OR_NEWER	// || UNITY_2018 || UNITY_2019 || UNITY_2020 || UNITY_2021 || UNITY_2022 || UNITY_2023
		[DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable | GizmoType.InSelectionHierarchy)]
	#else
		[DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Pickable | GizmoType.InSelectionHierarchy)]
	#endif
		static void RenderGizmo(MegaTankWheels track, GizmoType gizmoType)
		{
			if ( (gizmoType & GizmoType.Active) != 0 && Selection.activeObject == track.gameObject )
			{
				Gizmos.matrix = track.transform.localToWorldMatrix;
				Gizmos.DrawWireSphere(Vector3.zero, track.radius);
			}
		}
	}
}