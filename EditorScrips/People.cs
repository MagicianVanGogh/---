using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("http://www.baidu.com")]//自定义帮助文档的链接，一定要有https://
[AddComponentMenu("Learning/People")]//在视图中显示
[RequireComponent(typeof(Rigidbody))]//添加时可同时添加其他组件
public class People : MonoBehaviour
{
    [Header("BaseInfo")]//标题
    [Multiline(5)]//一个name的输入框，数字代表高
    public string name;
    [Range(-2,2)]//一个范围性的slider
    public int num;

    [Space(20)]//空行
    [Tooltip("设置你的性别")]//鼠标停留时的提示信息
    public string sex;

    [ContextMenu("OutputInfos")]//在非执行情况下可在component里调用函数
    void OutputInfo()
    {
        print("output:" + num);
    }
}
