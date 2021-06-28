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



	//   Te devuelve el angulo en grados, ya que para conseguir los angulos con ToEulerRag te los da en radianes.</para>
	public Vector3 eulerAngles
	{
		get
		{
			//lo pasa a grados.
			return ToEulerRad(this) * Mathf.Rad2Deg;
		}
		set
		{
			//lo pasa a radeanes para convertirlo en quaternion.
			this = FromEulerRad(value * Mathf.Deg2Rad);
		}
	}

	// Te devuelve el quaternion con la magnitud en 1.
	public MyQuaternion normalized
    {
        get
        {
            MyQuaternion q = this;
            float scale = 1.0f / q.Length;
            q.xyz *= scale;
            q.w *= scale;
            return q;
        }
    }
	// magnitud del quaternion.
    public float Length
	{
		get
		{
			return Mathf.Sqrt(x * x + y * y + z * z + w * w);
		}
	}

	// te devuelve la raiz de la magnitud del quaternion.
    public float LengthSquared
	{
		get
		{
			return x * x + y * y + z * z + w * w;
		}
	}
	/// <summary>
	///   <para>Constructs new MyQuaternion with given x,y,z,w components.</para>
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	/// <param name="w"></param>
	public MyQuaternion(float x, float y, float z, float w)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = w;
	}
	/// <summary>
	/// Construct a new MyQuaternion from vector and w components
	/// </summary>
	/// <param name="v">The vector part</param>
	/// <param name="w">The w part</param>
	private MyQuaternion(Vector3 v, float w)
	{
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
		this.w = w;
	}
	/// <summary>
	///   <para>Set x, y, z and w components of an existing MyQuaternion.</para>
	/// </summary>
	/// <param name="newX">x</param>
	/// <param name="newY">y</param>
	/// <param name="newZ">z</param>
	/// <param name="newW">w</param>
	public void Set(float newX, float newY, float newZ, float newW)
    {
        this.x = newX;
        this.y = newY;
		this.z = newZ;
		this.w = newW;
	}

	// la escala del quaternion sobre su largo.

	public void Normalize()
	{
		float scale = 1.0f / this.Length;
		xyz *= scale;
		w *= scale;
	}
	/// <summary>
	/// Converts this quiaternion to one whit the same orientation but with a magnitude of 1.
	/// </summary>

	public static MyQuaternion Normalize(MyQuaternion q)
	{
		MyQuaternion result;
		Normalize(ref q, out result);
		return result;
	}
	/// <summary>
	/// Scale the given quaternion to unit length
	/// </summary>
	/// <param name="q">The quaternion to normalize</param>
	/// <param name="result">The normalized quaternion</param>
	private static void Normalize(ref MyQuaternion q, out MyQuaternion result)
	{
		float scale = 1.0f / q.Length;
		result = new MyQuaternion(q.xyz * scale, q.w * scale);
	}
	/// <summary>
	///   <para>The dot product between two rotations.</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	public static float Dot(MyQuaternion a, MyQuaternion b)
	{
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
	}
	/// <summary>
	///   <para>Creates a rotation which rotates /angle/ degrees around /axis/.</para>
	/// </summary>
	/// <param name="angle"></param>
	/// <param name="axis"></param>
	public static MyQuaternion AngleAxis(float angle, Vector3 axis)
	{
		return MyQuaternion.AngleAxis(angle, ref axis);
	}
	private static MyQuaternion AngleAxis(float degress, ref Vector3 axis)
	{
		if (axis.sqrMagnitude == 0.0f)
			return identity;

		MyQuaternion result = identity;
		var radians = degress * Mathf.Deg2Rad;
		radians *= 0.5f;
		axis.Normalize();
		axis = axis * (float)System.Math.Sin(radians);
		result.x = axis.x;
		result.y = axis.y;
		result.z = axis.z;
		result.w = (float)System.Math.Cos(radians);

		return Normalize(result);
	}
	public void ToAngleAxis(out float angle, out Vector3 axis)
	{
		ToAxisAngleRad(this, out axis, out angle);
		angle *= Mathf.Rad2Deg;
	}
	/// <summary>
	///   <para>crea una rotacion desdelaDireccion a la direccion2.</para>
	///   Esta no entiendo para que esta... no que hace, lo mismo que la siguiente crea una rotacion entre 2 direcciones pero porque hay 2.
	/// </summary>
	public static MyQuaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
	{
		return RotateTowards(LookRotation(fromDirection), LookRotation(toDirection), float.MaxValue);
	}
	/// <summary>
	///   <para>crea una rotacion desdelaDireccion a la direccion2.</para>
	/// </summary>
	public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
	{
		this = FromToRotation(fromDirection, toDirection);
	}
	public static MyQuaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
	{
		return LookRotation(ref forward, ref upwards);
	}
	public static MyQuaternion LookRotation(Vector3 forward)
	{
		Vector3 up = Vector3.up;
		return LookRotation(ref forward, ref up);
	}
	/// <summary>
	///estoy es muy complejo amigo.
	///my resumido, se plantea una matriz.
	///si sabes que la magnitud del quaternion es 1 y sabes todas las componentes,
	///podes ir haciendo preguntar para ir descartando y saber cual no es el 0.
	///siempre entra en alguno de los ifs, estos son principalmente para no entrar en un nan.
	/// </summary>
	private static MyQuaternion LookRotation(ref Vector3 forward, ref Vector3 up)
	{
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
	public void SetLookRotation(Vector3 view)
	{
		Vector3 up = Vector3.up;
		SetLookRotation(view, up);
	}
	/// <summary>
	///   Crea la rotacion de donde mira y adonde.
	/// view es la direccion a donde va a mirar.
	/// up es la cara que va a mirar en esa direccion.
	/// </summary>
	public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
	{
		this = LookRotation(view, up);
	}
	/// <summary>
	///   <para>Spherically interpolates between /a/ and /b/ by t. The parameter /t/ is clamped to the range [0, 1].</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <param name="t"></param>
	public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t)
	{
		return Slerp(ref a, ref b, t);
	}
	private static MyQuaternion Slerp(ref MyQuaternion a, ref MyQuaternion b, float t)
	{
		if (t > 1) t = 1;
		if (t < 0) t = 0;
		return SlerpUnclamped(ref a, ref b, t);
	}
	/// <summary>
	///   <para>Spherically interpolates between /a/ and /b/ by t. The parameter /t/ is not clamped.</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <param name="t"></param>
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
	/// <summary>
	///   <para>Interpolates between /a/ and /b/ by /t/ and normalizes the result afterwards. The parameter /t/ is clamped to the range [0, 1].</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <param name="t"></param>
	public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t)
	{
		if (t > 1) t = 1;
		if (t < 0) t = 0;
		return Slerp(ref a, ref b, t); // TODO: use lerp not slerp, "Because quaternion works in 4D. Rotation in 4D are linear" ???
	}
	/// <summary>
	///   <para>Interpolates between /a/ and /b/ by /t/ and normalizes the result afterwards. The parameter /t/ is not clamped.</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <param name="t"></param>
	public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
	{
		return LerpUnclamped(ref a, ref b, t);
	}
	private static MyQuaternion LerpUnclamped(ref MyQuaternion a, ref MyQuaternion b, float t)
	{
		MyQuaternion q = new Quaternion(0, 0, 0, 0);
		if (Dot(a, b) < 0)
		{
			q.x = a.x + t * (-b.x - a.x);
			q.y = a.y + t * (-b.y - a.y);
			q.z = a.z + t * (-b.z - a.z);
			q.w = a.w + t * (-b.w - b.w);
		}
		else
		{
			q.x = a.x + t * (b.x - a.x);
			q.y = a.y + t * (b.y - a.y);
			q.z = a.z + t * (b.z - a.z);
			q.w = a.w + t * (b.w - b.w);
		}
		return q.normalized;
	}
	/// <summary>
	///   <para>Rotates a rotation /from/ towards /to/.</para>
	/// </summary>
	/// <param name="from"></param>
	/// <param name="to"></param>
	/// <param name="maxDegreesDelta"></param>
	public static MyQuaternion RotateTowards(MyQuaternion from, MyQuaternion to, float maxDegreesDelta)
	{
		float num = MyQuaternion.Angle(from, to);
		if (num == 0f)
		{
			return to;
		}
		float t = Math.Min(1f, maxDegreesDelta / num);
		return MyQuaternion.SlerpUnclamped(from, to, t);
	}
	/// <summary>
	///   <para>Returns the Inverse of /rotation/.</para>
	/// </summary>
	/// <param name="rotation"></param>
	public static MyQuaternion Inverse(MyQuaternion rotation)
	{
		float lengthSq = rotation.LengthSquared;
		if (lengthSq != 0.0)
		{
			float i = 1.0f / lengthSq;
			return new MyQuaternion(rotation.xyz * -i, rotation.w * i);
		}
		return rotation;
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
	/// <summary>
	///   <para>Returns the angle in degrees between two rotations /a/ and /b/.</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	public static float Angle(MyQuaternion a, MyQuaternion b)
	{
		float f = MyQuaternion.Dot(a, b);
		return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * Mathf.Rad2Deg;
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
	private static Vector3 ToEulerRad(MyQuaternion rotation)
	{
		//singularidad: dada sieras reglas matemacias no funciona.
		float sqw = rotation.w * rotation.w;
		float sqx = rotation.x * rotation.x;
		float sqy = rotation.y * rotation.y;
		float sqz = rotation.z * rotation.z;
		float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
		float test = rotation.x * rotation.w - rotation.y * rotation.z;
		Vector3 v;
		//no tengo ni la menor idea de que son las singularidades de los polos.
		//si x esta muy cerca de 90 o de - 90 se corrigen. porque x e y pasan a fucionarse.
		if (test > 0.4995f * unit)
		{ // singularity at north pole
			v.y = 2f * Mathf.Atan2(rotation.y, rotation.x);
			v.x = Mathf.PI / 2;
			v.z = 0;
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}
		if (test < -0.4995f * unit)
		{ // singularity at south pole
			v.y = -2f * Mathf.Atan2(rotation.y, rotation.x);
			v.x = -Mathf.PI / 2;
			v.z = 0;
			return NormalizeAngles(v * Mathf.Rad2Deg);
		}
		//si el quaternion no esta cerca de sus polos, se transforma de la manera contraria a fromEulerRad.
		//al principio no entendia el orden de este nuevo quaternion, pero es asi porque el orden afecta el resultado.
		MyQuaternion q = new MyQuaternion(rotation.w, rotation.z, rotation.x, rotation.y);
		v.y = Mathf.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));       // Yaw
		v.x = Mathf.Asin(2f * (q.x * q.z - q.w * q.y));												// Pitch
		v.z = Mathf.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));       // Roll
		return NormalizeAngles(v * Mathf.Rad2Deg);
	}
	/// <summary>
	///pasa de euler a quaternion.
	/// </summary>
	private static MyQuaternion FromEulerRad(Vector3 euler)
	{
		
		MyQuaternion value;
		//el 0.5f es la mitad de grado a radian.
		//para el euler a radianes.
		euler.x = euler.x * 0.5f;
		euler.y = euler.y * 0.5f;
		euler.z = euler.z * 0.5f;

		float sinX = Mathf.Sin(euler.x);
		float cosX = Mathf.Cos(euler.x);
		float sinY = Mathf.Sin(euler.y);
		float cosY = Mathf.Cos(euler.y);
		float sinZ = Mathf.Sin(euler.z);
		float cosZ = Mathf.Cos(euler.z);

		value.w = cosY * cosX * cosZ + sinY * sinX * sinZ;
		value.x = cosY * sinX * cosZ + sinY * cosX * sinZ;
		value.y = sinY * cosX * cosZ - cosY * sinX * sinZ;
		value.z = cosY * cosX * sinZ - sinY * sinX * cosZ;

		return value;
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

	//deprecado.
	private static void ToAxisAngleRad(MyQuaternion q, out Vector3 axis, out float angle)
	{
		if (System.Math.Abs(q.w) > 1.0f)
			q.Normalize();
		angle = 2.0f * Mathf.Acos(q.w); // angle
		float den = Mathf.Sqrt(1.0f - q.w * q.w);
		if (den > 0.0001f)
		{
			axis = q.xyz / den;
		}
		else
		{
			// This occurs when the angle is zero. 
			// Not a problem: just set an arbitrary normalized axis.
			axis = new Vector3(1, 0, 0);
		}
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

