using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
	//線
	[SerializeField] private GameObject l;
	[SerializeField] private GameObject r;
	private float sp = 2.0f;


	[SerializeField] private GameObject target_;

	private float angle = 20;//角度
     public float view { get => angle * Mathf.Deg2Rad; }
	/// <summary>  
	/// プレイヤー  
	/// </summary>  
	[SerializeField] private Player player_ = null;

    /// <summary>  
    /// ワールド行列   
    /// </summary>  
    private Matrix4x4 worldMatrix_ = Matrix4x4.identity;

    /// <summary>  
    /// ターゲットとして設定する  
    /// </summary>  
    /// <param name="enable">true:設定する / false:解除する</param>  
    public void SetTarget(bool enable)
    {
        // マテリアルの色を変更する  
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials[0].color = enable ? Color.red : Color.white;
    }

	/// <summary>
	/// 開始処理
	/// </summary>
	public void Start()
    {
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Update()
    {
		if(l)
		{
			var localMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -angle, 0));
			l.transform.rotation = (worldMatrix_ * localMatrix).rotation;
			l.transform.position=transform.position;
		}

		if(r)
		{
			var localMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -angle, 0));
			r.transform.rotation=(worldMatrix_ * localMatrix).rotation;
			r.transform.position=transform.position;
		}

		if(angle>=view)
		{
			var toTarget = (target_.transform.position - transform.position).normalized;
			var fowerd = transform.forward;
			var dot = Vector3.Dot(fowerd, toTarget);
			Vector3 direction = toTarget.normalized;
			var radian = Mathf.Acos(dot);
			var cross = Vector3.Cross(fowerd, toTarget);
			radian *= Mathf.Sign(cross.y);
			Quaternion rotation = Quaternion.AngleAxis(radian * Mathf.Rad2Deg, Vector3.up);
			transform.rotation = rotation * transform.rotation;
			transform.position += direction * sp * Time.deltaTime;
		}
    }
}
