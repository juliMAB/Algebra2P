using System;
using UnityEngine.Internal;
using UnityEngine;


public struct MyQuaternion : IEquatable<MyQuaternion>
{
	#region valores
	public float x;

	public float y;

	public float z;

	public float w;


	public const float kEpsilon = 1E-06f;


	public Vector3 xyz
	{
		set
		{
			x = value.x;
			y = value.y;
			z = value.z;
		}
		get
		{
			return new Vector3(x, y, z);
		}
	}


	

	public float this[int index]
	{
		get
		{
			switch (index)
			{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				case 3:
					return this.w;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
			}
		}
		set
		{
			switch (index)
			{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Quaternion index: " + index + ", can use only 0,1,2,3");
			}
		}
	}
	public static MyQuaternion identity
	{
		get
		{
			return new MyQuaternion(0f, 0f, 0f, 1f);
		}
	}
	#endregion
	#region Constructores
	public MyQuaternion(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}

	private MyQuaternion(Vector3 v, float w)
	{
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
		this.w = w;
	}
	#endregion


	public Vector3 eulerAngles
	{
		get
		{
			//lo pasa a grados de radianes.
			return ToEulerRad(this) * Mathf.Rad2Deg;
		}
		set
		{
			//lo pasa a radeanes para convertirlo en quaternion.
			this = FromEulerRad(value * Mathf.Deg2Rad);
		}
	}

	public MyQuaternion normalized
    {
        get
        {
            MyQuaternion q = this;
            float scale = 1.0f / q.Length;
            q.xyz *= scale;
            q.w *= scale;
            return q;
			// Te devuelve el quaternion con la magnitud en 1.
		}
	}
	
    public float Length
	{
		get
		{
			return Mathf.Sqrt(x * x + y * y + z * z + w * w);
			// te devuelve la magnitud del quaternion.
		}
	}

	
    public float LengthSquared
	{
		get
		{
			return x * x + y * y + z * z + w * w;
		}
	}
	
	
	public void Set(float newX, float newY, float newZ, float newW)
    {
        this.x = newX;
        this.y = newY;
		this.z = newZ;
		this.w = newW;
	}

	public void Normalize()
	{
		float scale = 1.0f / this.Length;
		xyz *= scale;
		w *= scale;
		//quaternion normalizado, con magnitud en = 1.
	}

	public static MyQuaternion Normalize(MyQuaternion q)
	{
		MyQuaternion result;
		float scale = 1.0f / q.Length;
		result = new MyQuaternion(q.xyz * scale, q.w * scale);
		return result;
		//normaliza un q pasado por parametro y lo devuelve.
	}

	public static float Dot(MyQuaternion a, MyQuaternion b)
	{
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		//producto punto.
	}

	private static Vector3 ToEulerRad(MyQuaternion rotation)
	{
		//singularidad: es cuando dados ciertos valores, las reglas matematicas fallan. como 40/0
		//sieras reglas matemacias no se aplican para ciertas reglas matematicas,
		//primero normalizo el quaternion-----
		float sqw = rotation.w * rotation.w;
		float sqx = rotation.x * rotation.x;
		float sqy = rotation.y * rotation.y;
		float sqz = rotation.z * rotation.z;
		//---------------------------------
		//unit no va a dar 1 siempre que el quaternion este normalizado, en caso de que no, se utilizara
		//como escalar para normalizar
		//aplicarlo a este quaternion.
		float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
		float test = rotation.x * rotation.w - rotation.y * rotation.z;
		//test nos va a devolver el valor de x
		Vector3 v;
		//si x esta muy cerca de 90 o de - 90 se corrigen. porque x va a estar muy cerca de z.
		//lo que hace principalmene es calcularlo de la siguiente manera.
		//singularidad llamada gimbal lock
		if (test > 0.4999f * unit)
		{ // singularity at north pole
			v.y = 2f * Mathf.Atan2(rotation.y, rotation.x);
			v.x = Mathf.PI / 2; //lo que hace aca es darle directamente el valor a x y z lo manda a 0.
			v.z = 0;
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}
		if (test < -0.4999f * unit)
		{ // singularity at south pole
			v.y = -2f * Mathf.Atan2(rotation.y, rotation.x);
			v.x = -Mathf.PI / 2;
			v.z = 0;
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}
		//si el quaternion no esta cerca de sus polos, se transforma de la manera contraria a fromEulerRad.
		//https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles#Euler_angles_to_quaternion_conversion.
		//la inversa de fromEulerRad.
		//la respuesta correcta, porque asi es la formula.
		MyQuaternion q = new MyQuaternion(rotation.w, rotation.z, rotation.x, rotation.y);
		v.y = Mathf.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));       // Yaw
		v.x = Mathf.Asin(2f * (q.x * q.z - q.w * q.y));                                             // Pitch
		v.z = Mathf.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));       // Roll
		return NormalizeAngles(v * Mathf.Rad2Deg);
	}

	public static MyQuaternion AngleAxis(float angle, Vector3 axis)
	{
		axis.Normalize();
		axis *= Mathf.Sin(angle * Mathf.Deg2Rad * 0.5f);
		return new MyQuaternion(axis.x, axis.y, axis.z, Mathf.Cos(angle * Mathf.Deg2Rad * 0.5f)); 
	}

	public static MyQuaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
	{
		//va a girar sobre axis la cantidad de angle.
		//axis va a ser el vector perpendicular a ambos vectores.
		Vector3 axis = Vector3.Cross(fromDirection, toDirection);
		float angle = Vector3.Angle(fromDirection, toDirection);
		return AngleAxis(angle,axis.normalized);
	}

	public static MyQuaternion Inverse(MyQuaternion rotation)
	{
		return new Quaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
	}

	private static MyQuaternion LookRotation(Vector3 forward, Vector3 up)
	{
		//transforma un vector director en una rotacion que tenga su eje z alineado con el foward
		//primero creamos la matriz de rotacion.
		forward = Vector3.Normalize(forward);
		Vector3 right = Vector3.Normalize(Vector3.Cross(up, forward));
		up = Vector3.Cross(forward, right);
		var m00 = right.x;
		var m01 = right.y;
		var m02 = right.z;
		var m10 = up.x;
		var m11 = up.y;
		var m12 = up.z;
		var m20 = forward.x;
		var m21 = forward.y;
		var m22 = forward.z;


		float num8 = (m00 + m11) + m22;
		var quaternion = new MyQuaternion();
		if (num8 > 0f)
		{
			var num = Mathf.Sqrt(num8 + 1f);
			quaternion.w = num * 0.5f;
			num = 0.5f / num;
			quaternion.x = (m12 - m21) * num;
			quaternion.y = (m20 - m02) * num;
			quaternion.z = (m01 - m10) * num;
			return quaternion;
		}
		if ((m00 >= m11) && (m00 >= m22))
		{
			var num7 = Mathf.Sqrt(((1f + m00) - m11) - m22);
			var num4 = 0.5f / num7;
			quaternion.x = 0.5f * num7;
			quaternion.y = (m01 + m10) * num4;
			quaternion.z = (m02 + m20) * num4;
			quaternion.w = (m12 - m21) * num4;
			return quaternion;
		}
		if (m11 > m22)
		{
			var num6 = Mathf.Sqrt(((1f + m11) - m00) - m22);
			var num3 = 0.5f / num6;
			quaternion.x = (m10 + m01) * num3;
			quaternion.y = 0.5f * num6;
			quaternion.z = (m21 + m12) * num3;
			quaternion.w = (m20 - m02) * num3;
			return quaternion;
		}
		//ningun componente es igual a 0.
		var num5 = Mathf.Sqrt(((1f + m22) - m00) - m11);
		var num2 = 0.5f / num5;
		quaternion.x = (m20 + m02) * num2;
		quaternion.y = (m21 + m12) * num2;
		quaternion.z = 0.5f * num5;
		quaternion.w = (m01 - m10) * num2;
		return quaternion;
	}
	/// <summary>
	///   Crea la rotacion de donde mira y con que cara, que por defecto es up.
	/// view es la direccion a donde va a mirar.
	/// </summary>
    public static MyQuaternion SlerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
    {
        return SlerpUnclamped(ref a, ref b, t);
    }
    private static MyQuaternion SlerpUnclamped(ref MyQuaternion a, ref MyQuaternion b, float t)
    {
        // if either input is zero, return the other.
        if (a.LengthSquared == 0.0f)
        {
            if (b.LengthSquared == 0.0f)
            {
                return identity;
            }
            return b;
        }
        else if (b.LengthSquared == 0.0f)
        {
            return a;
        }


        float cosHalfAngle = a.w * b.w + Vector3.Dot(a.xyz, b.xyz);

        if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
        {
            // angle = 0.0f, so just return one input.
            return a;
        }
        else if (cosHalfAngle < 0.0f)
        {
            b.xyz = -b.xyz;
            b.w = -b.w;
            cosHalfAngle = -cosHalfAngle;
        }

        float blendA;
        float blendB;
        if (cosHalfAngle < 0.99f)
        {
            // do proper slerp for big angles
            float halfAngle = Mathf.Acos(cosHalfAngle);
            float sinHalfAngle = Mathf.Sin(halfAngle);
            float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
            blendA = Mathf.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
            blendB = Mathf.Sin(halfAngle * t) * oneOverSinHalfAngle;
        }
        else
        {
            // do lerp if angle is really small.
            blendA = 1.0f - t;
            blendB = t;
        }

        MyQuaternion result = new MyQuaternion(blendA * a.xyz + blendB * b.xyz, blendA * a.w + blendB * b.w);
        if (result.LengthSquared > 0.0f)
            return Normalize(result);
        else
            return identity;
    }

    public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
    {
		MyQuaternion res = identity;
		float timeLeft = 1.0f - t;
        if (Dot(a,b)>=0) //para ver el camino mas corto.
        {
			res.x = (timeLeft * a.x) + (t * b.x);
			res.y = (timeLeft * a.y) + (t * b.y);
			res.z = (timeLeft * a.z) + (t * b.z);
			res.w = (timeLeft * a.w) + (t * b.w);
		}
        else
        {
			res.x = (timeLeft * a.x) - (t * b.x);
			res.y = (timeLeft * a.y) - (t * b.y);
			res.z = (timeLeft * a.z) - (t * b.z);
			res.w = (timeLeft * a.w) - (t * b.w);
		}
		res.Normalize();
		return res;
    }
    
    /// <summary>
    ///   te va a devolver la rotacion que se requiere para pasar de de from a to
    /// </summary>
    public static MyQuaternion RotateTowards(MyQuaternion from, MyQuaternion to, float maxDegreesDelta)
	{
		float num = Angle(from, to);
		if (num == 0f)
		{
			return to;
		}
		float t = Math.Min(1f, maxDegreesDelta / num);
		return SlerpUnclamped(from, to, t);
	}

	/// <summary>
	///   <para>Returns a nicely formatted string of the MyQuaternion.</para>
	/// </summary>
	/// <param name="format"></param>
	public override string ToString()
	{
		return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", this.x, this.y, this.z, this.w);
	}
	/// <summary>
	///   <para>Returns a nicely formatted string of the MyQuaternion.</para>
	/// </summary>
	/// <param name="format"></param>
	public string ToString(string format)
	{
		return string.Format("({0}, {1}, {2}, {3})", this.x.ToString(format), this.y.ToString(format), this.z.ToString(format), this.w.ToString(format));
	}

	public static float Angle(MyQuaternion a, MyQuaternion b)
	{
		float f = Dot(a, b);
		return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * Mathf.Rad2Deg;
		//el arcocoseno del producto punto entre los q da el angulo.
	}
	/// <summary>
	///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	public static MyQuaternion Euler(float x, float y, float z)
	{
		return MyQuaternion.FromEulerRad(new Vector3((float)x, (float)y, (float)z) * Mathf.Deg2Rad);
	}
	/// <summary>
	///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
	/// </summary>
	/// <param name="euler"></param>
	public static MyQuaternion Euler(Vector3 euler)
	{
		return MyQuaternion.FromEulerRad(euler * Mathf.Deg2Rad);
	}
	/// <summary>
	///pasa de quaternion a euler.
	/// </summary>
	
	private static MyQuaternion FromEulerRad(Vector3 euler)
	{
		MyQuaternion qx = identity;
		MyQuaternion qy = identity;
		MyQuaternion qz = identity;
		MyQuaternion res= identity;
		//el 0.5f es la mitad de grado a radian.
		//para el euler a radianes.
		euler.x = euler.x * 0.5f;
		euler.y = euler.y * 0.5f;
		euler.z = euler.z * 0.5f;
		//se calcula el seno del angulo en la componente imaginalia.
		//y el coseno del angulo en la componente real.
		float sinX = Mathf.Sin(euler.x);
		float cosX = Mathf.Cos(euler.x);
		qx.Set(sinX, 0, 0, cosX);
		float sinY = Mathf.Sin(euler.y);
		float cosY = Mathf.Cos(euler.y);
		qy.Set(0, sinY, 0, cosY);
		float sinZ = Mathf.Sin(euler.z);
		float cosZ = Mathf.Cos(euler.z);
		qz.Set(0, 0, cosZ, cosZ);
		//y se aplican las 3 rotaciones en este orden especifico.
		res = qy * qx * qz;

		return res;
	}
	/// <summary>
	///devuelve todos los angulos de 360 a 0.
	/// </summary>
	private static Vector3 NormalizeAngles(Vector3 angles)
	{
		angles.x = NormalizeAngle(angles.x);
		angles.y = NormalizeAngle(angles.y);
		angles.z = NormalizeAngle(angles.z);
		return angles;
	}
	/// <summary>
	///normalizar el angulo es hacer que siempre esten de 360 a 0, si se pasa de 360 se resta, si se pasa de 0 se suma 360.
	/// </summary>
	private static float NormalizeAngle(float angle)
	{
		while (angle > 360)
			angle -= 360;
		while (angle < 0)
			angle += 360;
		return angle;
	}

	private void ToAxisAngle(MyQuaternion q, out Vector3 axis, out float angle)
	{
		if (Math.Abs(q.w) > 1.0f)
			q.Normalize();
		angle = 2.0f * Mathf.Acos(q.w); // el arcoseno del componente real da como resultado el angulo es una propiedad.
		float mag = Mathf.Sqrt(1.0f - q.w * q.w); //la magnitud
		if (mag > 0.0001f)
		{
			//el eje va a equivaler a las componentes imaginarias divididas por la magnitud.
			axis = q.xyz / mag;
		}
		else
		{
			//si el angulo es 0 se pasa un eje arbitrario.
			axis = new Vector3(1, 0, 0);
		}
		//devuelve un eje axis que rotado la cantidad de angle.
	}

	#region operators

	/// <summary>
	///Es como la ide del objeto.
	/// </summary>
	public override int GetHashCode()
	{
		return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode() >> 1;
	}
	/// <summary>
	///pregunta si es igual.
	/// </summary>
	public override bool Equals(object other)
	{
		if (!(other is MyQuaternion))
		{
			return false;
		}
		MyQuaternion quaternion = (MyQuaternion)other;
		return x.Equals(quaternion.x) && y.Equals(quaternion.y) && z.Equals(quaternion.z) && w.Equals(quaternion.w);
	}
	/// <summary>
	///pregunta si es igual.
	/// </summary>
	public bool Equals(MyQuaternion other)
	{
		return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
	}
	/// <summary>
	///multiplica los quaterniones. como el exel que vimos en la clase.
	///ijk=-1
	///x*x=-1
	///y*y=-1
	///es una distribuida.
	/// los menos son por la propiedad de arriba.
	/// 
	/// </summary>
	public static MyQuaternion operator *(MyQuaternion lhs, MyQuaternion rhs)
	{
		return new MyQuaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
								lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
								lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
								lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
	}
	/// <summary>
	///devuelve un punto rotado, rota un punto con un quaternion, rota un vector con un quaternion.
	///formula de matriz de rotacion x, y, z.
	/// </summary>
	public static Vector3 operator *(MyQuaternion rotation, Vector3 point)
	{
		float num = rotation.x * 2f;
		float num2 = rotation.y * 2f;
		float num3 = rotation.z * 2f;
		float num4 = rotation.x * num;
		float num5 = rotation.y * num2;
		float num6 = rotation.z * num3;
		float num7 = rotation.x * num2;
		float num8 = rotation.x * num3;
		float num9 = rotation.y * num3;
		float num10 = rotation.w * num;
		float num11 = rotation.w * num2;
		float num12 = rotation.w * num3;
		Vector3 result;
		result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
		result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
		result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
		return result;
	}
	/// <summary>
	///el producto escalar es que tantas veces tengo que sumar uno para que me de el otro, por lo tanto cuando es 1 son iguales y el 0.999 es por el margen de error.
	/// </summary>
	public static bool operator ==(MyQuaternion lhs, MyQuaternion rhs)
	{
		return Dot(lhs, rhs) > 0.999999f;
	}
	public static bool operator !=(MyQuaternion lhs, MyQuaternion rhs)
    {
        return Dot(lhs, rhs) <= 0.999999f;
	}
	#region Implicit conversions to and from Unity's Quaternion
	public static implicit operator Quaternion(MyQuaternion me)
	{
		return new Quaternion((float)me.x, (float)me.y, (float)me.z, (float)me.w);
	}
	public static implicit operator MyQuaternion(UnityEngine.Quaternion other)
	{
		return new MyQuaternion((float)other.x, (float)other.y, (float)other.z, (float)other.w);
	}
    #endregion

    #endregion
}

