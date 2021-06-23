using System;
using UnityEngine.Internal;
using UnityEngine;

public struct MyMatriz
{

    public float m00; //0.
    public float m10; //1.
    public float m20; //2.
    public float m30; //3.

    public float m01; //4.
    public float m11; //5.
    public float m21; //6.
    public float m31; //7.

    public float m02; //8.
    public float m12; //9.
    public float m22; //10.
    public float m32; //11.

    public float m03; //12.
    public float m13; //13.
    public float m23; //14.
    public float m33; //15.




    public MyMatriz(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
    {
        m00 = column0.x; m01 = column1.x; m02 = column2.x; m03 = column3.x;
        m10 = column0.y; m11 = column1.y; m12 = column2.y; m13 = column3.y;
        m20 = column0.z; m21 = column1.z; m22 = column2.z; m23 = column3.z;
        m30 = column0.w; m31 = column1.w; m32 = column2.w; m33 = column3.w;
        return;
    }
	public float this[int index]
	{
		get
		{
            switch (index)
			{
				case 0:
					return this.m00;
				case 1:
					return this.m10;
				case 2:
					return this.m20;
				case 3:
					return this.m30;
				case 4:
					return this.m01;
				case 5:
					return this.m11;
				case 6:
					return this.m21;
				case 7:
					return this.m31;
                case 8:
                    return this.m02;
                case 9:
                    return this.m12;
                case 10:
                    return this.m22;
                case 11:
                    return this.m32;
                case 12:
                    return this.m03;
                case 13:
                    return this.m13;
                case 14:
                    return this.m23;
                case 15:
                    return this.m33;
                default:
					throw new IndexOutOfRangeException("Invalid index: " + index + ", can use only 0-15");
			}
		}
		set
		{
			switch (index)
			{
                case 0:
                    this.m00 = value; break;
                case 1:
                    this.m10 = value; break;
                case 2:
                    this.m20 = value; break;
                case 3:
                    this.m30 = value; break;
                case 4:
                    this.m01 = value; break;
                case 5:
                    this.m11 = value; break;
                case 6:
                    this.m21 = value; break;
                case 7:
                    this.m31 = value; break;
                case 8:
                    this.m02 = value; break;
                case 9:
                    this.m12 = value; break;
                case 10:
                    this.m22 = value; break;
                case 11:
                    this.m32 = value; break;
                case 12:
                    this.m03 = value; break;
                case 13:
                    this.m13 = value; break;
                case 14:
                    this.m23 = value; break;
                case 15:
                    this.m33 = value; break;
                default:
                    throw new IndexOutOfRangeException("Invalid index: " + index + ", can use only 0-15");
            }
		}
	}
    public float this[int row, int column] {
        get
        {
            switch (row)
            {
                case 0:
                    switch(column)
                    {
                        case 0:
                            return this.m00;
                        case 1:
                            return this.m10;
                        case 2:
                            return this.m20;
                        case 3:
                            return this.m30;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                case 1:
                    switch (column)
                    {
                        case 0:
                            return this.m01;
                        case 1:
                            return this.m11;
                        case 2:
                            return this.m21;
                        case 3:
                            return this.m31;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                case 2:
                    switch (column)
                    {
                        case 0:
                            return this.m02;
                        case 1:
                            return this.m12;
                        case 2:
                            return this.m22;
                        case 3:
                            return this.m32;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                case 3:
                    switch (column)
                    {
                        case 0:
                            return this.m03;
                        case 1:
                            return this.m13;
                        case 2:
                            return this.m23;
                        case 3:
                            return this.m33;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                default:
                    throw new IndexOutOfRangeException("Invalid row index: " + row + ", can use only 0,1,2,3");
            }
        }
        set
        {
            switch (row)
            {
                case 0:
                    switch (column)
                    {
                        case 0:
                            this.m00 = value; break;
                        case 1:
                            this.m10 = value; break;
                        case 2:
                            this.m20 = value; break;
                        case 3:
                            this.m30 = value; break;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                    break;
                case 1:
                    switch (column)
                    {
                        case 0:
                            this.m01 = value; break;
                        case 1:
                            this.m11 = value; break;
                        case 2:
                            this.m21 = value; break;
                        case 3:
                            this.m31 = value; break;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                    break;
                case 2:
                    switch (column)
                    {
                        case 0:
                            this.m02 = value; break;
                        case 1:
                            this.m12 = value; break;
                        case 2:
                            this.m22 = value; break;
                        case 3:
                            this.m32 = value; break;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                    break;
                case 3:
                    switch (column)
                    {
                        case 0:
                            this.m03 = value; break;
                        case 1:
                            this.m13 = value; break;
                        case 2:
                            this.m23 = value; break;
                        case 3:
                            this.m33 = value; break;
                        default:
                            throw new IndexOutOfRangeException("Invalid column index: " + column + ", can use only 0,1,2,3");
                    }
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid row index: " + row + ", can use only 0,1,2,3");
            }
        }
    }

    //
    // Resumen:
    //     Devuelve una matriz con todos los elementos seteados en cero (Solo de lectura).
    public static MyMatriz zero { get { return new MyMatriz(Vector4.zero, Vector4.zero, Vector4.zero, Vector4.zero); } }
    //
    // Resumen:
    //     Devuelve la identidad de la matriz osea con la franja de 1 al medio (Solo de lectura).
    public static MyMatriz identity 
    { get
        {
            return new MyMatriz(
          new Vector4(1, 0, 0, 0),
          new Vector4(0, 1, 0, 0),
          new Vector4(0, 0, 1, 0),
          new Vector4(0, 0, 0, 1));
        }
    }
    // Resumen:
    //     Devuelve el transpose de la matriz (Solo lectura). cambia las posiciones en la matrix.
    public MyMatriz transpose { get {
            MyMatriz res;
    res.m00 = m00;
    res.m10 = m01;
    res.m20 = m02;
    res.m30 = m03;
    res.m01 = m10;
    res.m11 = m11;
    res.m21 = m12;
    res.m31 = m13;
    res.m02 = m20;
    res.m12 = m21;
    res.m22 = m22;
    res.m32 = m23;
    res.m03 = m30;
    res.m13 = m31;
    res.m23 = m32;
    res.m33 = m33;
            return res;
        }
    }
}


// Resumen:
//     A standard 4x4 transformation matrix.
public struct MyMatrix4x4
{

    public float m00; //0.
    public float m10; //1.
    public float m20; //2.
    public float m30; //3.

    public float m01; //4.
    public float m11; //5.
    public float m21; //6.
    public float m31; //7.

    public float m02; //8.
    public float m12; //9.
    public float m22; //10.
    public float m32; //11.

    public float m03; //12.
    public float m13; //13.
    public float m23; //14.
    public float m33; //15.




    //
    // Resumen:
    //     Returns a matrix with all elements set to zero (Read Only).
    public static Matrix4x4 zero { get; }
    //
    // Resumen:
    //     Returns the identity matrix (Read Only).
    public static Matrix4x4 identity { get; }
    //
    // Resumen:
    //     Returns the transpose of this matrix (Read Only).
    public Matrix4x4 transpose { get; }
    //
    // Resumen:
    //     Attempts to get a rotation quaternion from this matrix.
    public Quaternion rotation { get; }
    //
    // Resumen:
    //     Attempts to get a scale value from the matrix. (Read Only)
    public Vector3 lossyScale { get; }
    //
    // Resumen:
    //     Checks whether this is an identity matrix. (Read Only)
    public bool isIdentity { get; }
    //
    // Resumen:
    //     The determinant of the matrix. (Read Only)
    public float determinant { get; }
    //
    // Resumen:
    //     This property takes a projection matrix and returns the six plane coordinates
    //     that define a projection frustum.
    public FrustumPlanes decomposeProjection { get; }
    //
    // Resumen:
    //     The inverse of this matrix. (Read Only)
    public Matrix4x4 inverse { get; }

    public static float Determinant(Matrix4x4 m);
    //
    // Resumen:
    //     This function returns a projection matrix with viewing frustum that has a near
    //     plane defined by the coordinates that were passed in.
    //
    // Parámetros:
    //   left:
    //     The X coordinate of the left side of the near projection plane in view space.
    //
    //   right:
    //     The X coordinate of the right side of the near projection plane in view space.
    //
    //   bottom:
    //     The Y coordinate of the bottom side of the near projection plane in view space.
    //
    //   top:
    //     The Y coordinate of the top side of the near projection plane in view space.
    //
    //   zNear:
    //     Z distance to the near plane from the origin in view space.
    //
    //   zFar:
    //     Z distance to the far plane from the origin in view space.
    //
    //   frustumPlanes:
    //     Frustum planes struct that contains the view space coordinates of that define
    //     a viewing frustum.
    //
    //   fp:
    //
    // Devuelve:
    //     A projection matrix with a viewing frustum defined by the plane coordinates passed
    //     in.
    
    public static Matrix4x4 Inverse(Matrix4x4 m);
    public static bool Inverse3DAffine(Matrix4x4 input, ref Matrix4x4 result);
    public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up);
    //
    // Resumen:
    //     Create an orthogonal projection matrix.
    //
    // Parámetros:
    //   left:
    //     Left-side x-coordinate.
    //
    //   right:
    //     Right-side x-coordinate.
    //
    //   bottom:
    //     Bottom y-coordinate.
    //
    //   top:
    //     Top y-coordinate.
    //
    //   zNear:
    //     Near depth clipping plane value.
    //
    //   zFar:
    //     Far depth clipping plane value.
    //
    // Devuelve:
    //     The projection matrix.
    public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar);
    //
    // Resumen:
    //     Create a perspective projection matrix.
    //
    // Parámetros:
    //   fov:
    //     Vertical field-of-view in degrees.
    //
    //   aspect:
    //     Aspect ratio (width divided by height).
    //
    //   zNear:
    //     Near depth clipping plane value.
    //
    //   zFar:
    //     Far depth clipping plane value.
    //
    // Devuelve:
    //     The projection matrix.
    public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar);
    //
    // Resumen:
    //     Creates a rotation matrix.
    //
    // Parámetros:
    //   q:
    public static Matrix4x4 Rotate(Quaternion q);
    //
    // Resumen:
    //     Creates a scaling matrix.
    //
    // Parámetros:
    //   vector:
    public static Matrix4x4 Scale(Vector3 vector);
    //
    // Resumen:
    //     Creates a translation matrix.
    //
    // Parámetros:
    //   vector:
    public static Matrix4x4 Translate(Vector3 vector);
    public static Matrix4x4 Transpose(Matrix4x4 m);
    //
    // Resumen:
    //     Creates a translation, rotation and scaling matrix.
    //
    // Parámetros:
    //   pos:
    //
    //   q:
    //
    //   s:
    public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s);
    public override bool Equals(object other);
    public bool Equals(Matrix4x4 other);
    //
    // Resumen:
    //     Get a column of the matrix.
    //
    // Parámetros:
    //   index:
    public Vector4 GetColumn(int index);
    public override int GetHashCode();
    //
    // Resumen:
    //     Returns a row of the matrix.
    //
    // Parámetros:
    //   index:
    public Vector4 GetRow(int index);
    //
    // Resumen:
    //     Transforms a position by this matrix (generic).
    //
    // Parámetros:
    //   point:
    public Vector3 MultiplyPoint(Vector3 point);
    //
    // Resumen:
    //     Transforms a position by this matrix (fast).
    //
    // Parámetros:
    //   point:
    public Vector3 MultiplyPoint3x4(Vector3 point);
    //
    // Resumen:
    //     Transforms a direction by this matrix.
    //
    // Parámetros:
    //   vector:
    public Vector3 MultiplyVector(Vector3 vector);
    //
    // Resumen:
    //     Sets a column of the matrix.
    //
    // Parámetros:
    //   index:
    //
    //   column:
    public void SetColumn(int index, Vector4 column);
    //
    // Resumen:
    //     Sets a row of the matrix.
    //
    // Parámetros:
    //   index:
    //
    //   row:
    public void SetRow(int index, Vector4 row);
    //
    // Resumen:
    //     Sets this matrix to a translation, rotation and scaling matrix.
    //
    // Parámetros:
    //   pos:
    //
    //   q:
    //
    //   s:
    public void SetTRS(Vector3 pos, Quaternion q, Vector3 s);
    //
    // Resumen:
    //     Returns a nicely formatted string for this matrix.
    //
    // Parámetros:
    //   format:
    public override string ToString();
    //
    // Resumen:
    //     Returns a nicely formatted string for this matrix.
    //
    // Parámetros:
    //   format:
    public string ToString(string format);
    //
    // Resumen:
    //     Returns a plane that is transformed in space.
    //
    // Parámetros:
    //   plane:
    public Plane TransformPlane(Plane plane);
    //
    // Resumen:
    //     Checks if this matrix is a valid transform matrix.
    [ThreadSafe]
    public bool ValidTRS();

    public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector);
    public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs);
    public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs);
    public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs);
}