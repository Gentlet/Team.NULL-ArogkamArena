/*
 * ChildUnitInterface.cs
 * 설명 :
 * Unit을 멤버 변수로 가지고, 연결하는 클래스일 경우 상속하면 된다.
 */
using UnityEngine;

public class ChildUnitInterface : MonoBehaviour
{
    private Unit unit = null;

    public void ConnectUnit(Unit unit)
    {
        if (this.unit == null)
        {
            this.unit = unit;
            Init();
        }
        else if (unit == null)
        {
            Debug.LogWarning(name + "Object argument is null");
        }
        else
        {
            Debug.LogWarning(name + " Object Already Connected");
        }
    }

    protected virtual void Init()
    {

    }

    #region Properties
    public Unit Unit
    {
        get
        {
            return unit;
        }
    }

    public T GetUnit<T>() where T : Unit
    {
        return unit as T;
    }
    #endregion
}
