using System;
using UnityEngine.Internal;
using UnityEngine;

public struct MyMatriz : IEquatable<MyMatriz>
{
    #region Variable
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

    public MyMatriz transpose
    {
        get
        {
            return Transpose(this);
        }
    }
    #endregion
    #region construction
    public MyMatriz(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
    {
        m00 = column0.x; m01 = column1.x; m02 = column2.x; m03 = column3.x;
        m10 = column0.y; m11 = column1.y; m12 = column2.y; m13 = column3.y;
        m20 = column0.z; m21 = column1.z; m22 = column2.z; m23 = column3.z;
        m30 = column0.w; m31 = column1.w; m32 = column2.w; m33 = column3.w;
        return;
    }
    #endregion
    #region operators
    public static Vector4 operator *(MyMatriz lhs, Vector4 vector)
    {
        Vector4 res;
        res.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
        res.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
        res.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
        res.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
        return res;
    }
    // Multiplies two matrices. Para no olvidarme como se multiplican matrices. se hace una linea recta y una linea horizontal.
    public static MyMatriz operator *(MyMatriz lhs, MyMatriz rhs)
    {
        MyMatriz res;
        res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
        res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
        res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
        res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;

        res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
        res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
        res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
        res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;

        res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
        res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
        res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
        res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;

        res.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
        res.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
        res.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
        res.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;

        return res;
    }
    public static bool operator ==(MyMatriz lhs, MyMatriz rhs)
    {
        for (int i = 0; i < 16; i++)
        {
            if (lhs[i] != rhs[i])
            {
                return false;
            }
        }
        return true;

    }
    public static bool operator !=(MyMatriz lhs, MyMatriz rhs)
    {
        for (int i = 0; i < 16; i++)
        {
            if (lhs[i] != rhs[i])
            {
                return true;
            }
        }
        return false;
    }

    public override int GetHashCode()
    {
        return GetColumn(0).GetHashCode() ^ GetColumn(1).GetHashCode() << 2 ^ GetColumn(2).GetHashCode() >> 2 ^ GetColumn(3).GetHashCode() >> 1;
    }
    #endregion
    #region getters
    public Vector4 GetColumn(int i)
    {
        return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
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
    public float this[int row, int column]
    {
        get
        {
            switch (row)
            {
                case 0:
                    switch (column)
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

    /// <summary>
    /// Resumen:Devuelve una matriz con todos los elementos en cero (Solo de lectura).
    /// </summary>    
    public static MyMatriz zero { get { return new MyMatriz(Vector4.zero, Vector4.zero, Vector4.zero, Vector4.zero); } }
    /// <summary>
    ///  Resumen: Devuelve la identidad de la matriz osea con la franja de 1 al medio (Solo de lectura).
    /// </summary>
    public static MyMatriz identity
    {
        get
        {
            return new MyMatriz(
          new Vector4(1, 0, 0, 0),
          new Vector4(0, 1, 0, 0),
          new Vector4(0, 0, 1, 0),
          new Vector4(0, 0, 0, 1));
        }
    }

    // Resumen:
    //     Setea una columna.
    public void SetColumn(int index, Vector4 column)
    {
        for (int i = 0; i < 4; i++)
        {
            this[i, index] = column[i];
        }
    }

    // Resumen:
    //     Setea auna fila.
    public void SetRow(int index, Vector4 row)
    {
        for (int i = 0; i < 4; i++)
        {
            this[index, i] = row[i];
        }
    }
    #endregion
    #region funciones
    public static MyMatriz Transpose(MyMatriz m)
    {
        return new MyMatriz()
        {
            m00 = m.m00,
            m01 = m.m10,
            m02 = m.m20,
            m03 = m.m30,
            m10 = m.m01,
            m11 = m.m11,
            m12 = m.m21,
            m13 = m.m31,
            m20 = m.m02,
            m21 = m.m12,
            m22 = m.m22,
            m23 = m.m32,
            m30 = m.m03,
            m31 = m.m13,
            m32 = m.m23,
            m33 = m.m33
        };
    }
    public static MyMatriz Translate(Vector3 v)
    {
        return new MyMatriz()
        {
            m00 = 0.0f,
            m01 = 0.0f,
            m02 = 0.0f,
            m03 = v.x,
            m10 = 0.0f,
            m11 = 0.0f,
            m12 = 0.0f,
            m13 = v.y,
            m20 = 0.0f,
            m21 = 0.0f,
            m22 = 0.0f,
            m23 = v.z,
            m30 = 0.0f,
            m31 = 0.0f,
            m32 = 0.0f,
            m33 = 0.0f
        };
    }
    public static MyMatriz Rotate(Quaternion q)
    {
        float num1 = q.x * 2f;
        float num2 = q.y * 2f;
        float num3 = q.z * 2f;
        float num4 = q.x * num1;
        float num5 = q.y * num2;
        float num6 = q.z * num3;
        float num7 = q.x * num2;
        float num8 = q.x * num3;
        float num9 = q.y * num3;
        float num10 = q.w * num1;
        float num11 = q.w * num2;
        float num12 = q.w * num3;
        MyMatriz matrix4x4;
        matrix4x4.m00 = (float)(1.0 - ((double)num5 + (double)num6));
        matrix4x4.m10 = num7 + num12;
        matrix4x4.m20 = num8 - num11;
        matrix4x4.m30 = 0.0f;
        matrix4x4.m01 = num7 - num12;
        matrix4x4.m11 = (float)(1.0 - ((double)num4 + (double)num6));
        matrix4x4.m21 = num9 + num10;
        matrix4x4.m31 = 0.0f;
        matrix4x4.m02 = num8 + num11;
        matrix4x4.m12 = num9 - num10;
        matrix4x4.m22 = (float)(1.0 - ((double)num4 + (double)num5));
        matrix4x4.m32 = 0.0f;
        matrix4x4.m03 = 0.0f;
        matrix4x4.m13 = 0.0f;
        matrix4x4.m23 = 0.0f;
        matrix4x4.m33 = 1f;
        return matrix4x4;
    }
    public static MyMatriz Scale(Vector3 v)
    {
        return new MyMatriz()
        {
            m00 = v.x,
            m01 = 0.0f,
            m02 = 0.0f,
            m03 = 0.0f,
            m10 = 0.0f,
            m11 = v.y,
            m12 = 0.0f,
            m13 = 0.0f,
            m20 = 0.0f,
            m21 = 0.0f,
            m22 = v.z,
            m23 = 0.0f,
            m30 = 0.0f,
            m31 = 0.0f,
            m32 = 0.0f,
            m33 = 1f
        };
    }
    public static MyMatriz TRS(Vector3 pos, Quaternion q, Vector3 s)
    {
        return ((Translate(pos)) * (Rotate(q)) * Scale(s));
    }
    public static MyMatriz Inverse(MyMatriz matrix)
    {
        float detA = Determinant(matrix);
        if (detA == 0)
            return MyMatriz.identity;

        MyMatriz tempMatrix = new MyMatriz()
        {
            //------0---------
            m00 = matrix.m11 * matrix.m22 * matrix.m33 + matrix.m12 * matrix.m23 * matrix.m31 + matrix.m13 * matrix.m21 * matrix.m32 - matrix.m11 * matrix.m23 * matrix.m32 - matrix.m12 * matrix.m21 * matrix.m33 - matrix.m13 * matrix.m22 * matrix.m31,
            m01 = matrix.m01 * matrix.m23 * matrix.m32 + matrix.m02 * matrix.m21 * matrix.m33 + matrix.m03 * matrix.m22 * matrix.m31 - matrix.m01 * matrix.m22 * matrix.m33 - matrix.m02 * matrix.m23 * matrix.m31 - matrix.m03 * matrix.m21 * matrix.m32,
            m02 = matrix.m01 * matrix.m12 * matrix.m33 + matrix.m02 * matrix.m13 * matrix.m32 + matrix.m03 * matrix.m11 * matrix.m32 - matrix.m01 * matrix.m13 * matrix.m32 - matrix.m02 * matrix.m11 * matrix.m33 - matrix.m03 * matrix.m12 * matrix.m31,
            m03 = matrix.m01 * matrix.m13 * matrix.m22 + matrix.m02 * matrix.m11 * matrix.m23 + matrix.m03 * matrix.m12 * matrix.m21 - matrix.m01 * matrix.m12 * matrix.m23 - matrix.m02 * matrix.m13 * matrix.m21 - matrix.m03 * matrix.m11 * matrix.m22,
            //-------1--------					     								    
            m10 = matrix.m10 * matrix.m23 * matrix.m32 + matrix.m12 * matrix.m20 * matrix.m33 + matrix.m13 * matrix.m22 * matrix.m30 - matrix.m10 * matrix.m22 * matrix.m33 - matrix.m12 * matrix.m23 * matrix.m30 - matrix.m13 * matrix.m20 * matrix.m32,
            m11 = matrix.m00 * matrix.m22 * matrix.m33 + matrix.m02 * matrix.m23 * matrix.m30 + matrix.m03 * matrix.m20 * matrix.m32 - matrix.m00 * matrix.m23 * matrix.m32 - matrix.m02 * matrix.m20 * matrix.m33 - matrix.m03 * matrix.m22 * matrix.m30,
            m12 = matrix.m00 * matrix.m13 * matrix.m32 + matrix.m02 * matrix.m10 * matrix.m33 + matrix.m03 * matrix.m12 * matrix.m30 - matrix.m00 * matrix.m12 * matrix.m33 - matrix.m02 * matrix.m13 * matrix.m30 - matrix.m03 * matrix.m10 * matrix.m32,
            m13 = matrix.m00 * matrix.m12 * matrix.m23 + matrix.m02 * matrix.m13 * matrix.m20 + matrix.m03 * matrix.m10 * matrix.m22 - matrix.m00 * matrix.m13 * matrix.m22 - matrix.m02 * matrix.m10 * matrix.m23 - matrix.m03 * matrix.m12 * matrix.m20,
            //-------2--------					     								    
            m20 = matrix.m10 * matrix.m21 * matrix.m33 + matrix.m11 * matrix.m23 * matrix.m30 + matrix.m13 * matrix.m20 * matrix.m31 - matrix.m10 * matrix.m23 * matrix.m31 - matrix.m11 * matrix.m20 * matrix.m33 - matrix.m13 * matrix.m31 * matrix.m30,
            m21 = matrix.m00 * matrix.m23 * matrix.m31 + matrix.m01 * matrix.m20 * matrix.m33 + matrix.m03 * matrix.m21 * matrix.m30 - matrix.m00 * matrix.m21 * matrix.m33 - matrix.m01 * matrix.m23 * matrix.m30 - matrix.m03 * matrix.m20 * matrix.m31,
            m22 = matrix.m00 * matrix.m11 * matrix.m33 + matrix.m01 * matrix.m13 * matrix.m31 + matrix.m03 * matrix.m10 * matrix.m31 - matrix.m00 * matrix.m13 * matrix.m31 - matrix.m01 * matrix.m10 * matrix.m33 - matrix.m03 * matrix.m11 * matrix.m30,
            m23 = matrix.m00 * matrix.m13 * matrix.m21 + matrix.m01 * matrix.m10 * matrix.m23 + matrix.m03 * matrix.m11 * matrix.m31 - matrix.m00 * matrix.m11 * matrix.m23 - matrix.m01 * matrix.m13 * matrix.m20 - matrix.m03 * matrix.m10 * matrix.m21,
            //------3---------					     								    
            m30 = matrix.m10 * matrix.m22 * matrix.m31 + matrix.m11 * matrix.m20 * matrix.m32 + matrix.m12 * matrix.m21 * matrix.m30 - matrix.m00 * matrix.m21 * matrix.m32 - matrix.m11 * matrix.m22 * matrix.m30 - matrix.m12 * matrix.m20 * matrix.m31,
            m31 = matrix.m00 * matrix.m21 * matrix.m32 + matrix.m01 * matrix.m22 * matrix.m30 + matrix.m02 * matrix.m20 * matrix.m31 - matrix.m00 * matrix.m22 * matrix.m31 - matrix.m01 * matrix.m20 * matrix.m32 - matrix.m02 * matrix.m21 * matrix.m30,
            m32 = matrix.m00 * matrix.m12 * matrix.m31 + matrix.m01 * matrix.m10 * matrix.m32 + matrix.m02 * matrix.m11 * matrix.m30 - matrix.m00 * matrix.m11 * matrix.m32 - matrix.m01 * matrix.m12 * matrix.m30 - matrix.m02 * matrix.m10 * matrix.m31,
            m33 = matrix.m00 * matrix.m11 * matrix.m22 + matrix.m01 * matrix.m12 * matrix.m20 + matrix.m02 * matrix.m10 * matrix.m21 - matrix.m00 * matrix.m12 * matrix.m21 - matrix.m01 * matrix.m10 * matrix.m22 - matrix.m02 * matrix.m11 * matrix.m20
        };

        MyMatriz result = new MyMatriz()
        {
            m00 = tempMatrix.m00 / detA,
            m01 = tempMatrix.m01 / detA,
            m02 = tempMatrix.m02 / detA,
            m03 = tempMatrix.m03 / detA,
            m10 = tempMatrix.m10 / detA,
            m11 = tempMatrix.m11 / detA,
            m12 = tempMatrix.m12 / detA,
            m13 = tempMatrix.m13 / detA,
            m20 = tempMatrix.m20 / detA,
            m21 = tempMatrix.m21 / detA,
            m22 = tempMatrix.m22 / detA,
            m23 = tempMatrix.m23 / detA,
            m30 = tempMatrix.m30 / detA,
            m31 = tempMatrix.m31 / detA,
            m32 = tempMatrix.m32 / detA,
            m33 = tempMatrix.m33 / detA

        };
        return result;
    }
    public static float Determinant(MyMatriz m)
    {
        return
            m[0, 3] * m[1, 2] * m[2, 1] * m[3, 0] - m[0, 2] * m[1, 3] * m[2, 1] * m[3, 0] -
            m[0, 3] * m[1, 1] * m[2, 2] * m[3, 0] + m[0, 1] * m[1, 3] * m[2, 2] * m[3, 0] +
            m[0, 2] * m[1, 1] * m[2, 3] * m[3, 0] - m[0, 1] * m[1, 2] * m[2, 3] * m[3, 0] -
            m[0, 3] * m[1, 2] * m[2, 0] * m[3, 1] + m[0, 2] * m[1, 3] * m[2, 0] * m[3, 1] +
            m[0, 3] * m[1, 0] * m[2, 2] * m[3, 1] - m[0, 0] * m[1, 3] * m[2, 2] * m[3, 1] -
            m[0, 2] * m[1, 0] * m[2, 3] * m[3, 1] + m[0, 0] * m[1, 2] * m[2, 3] * m[3, 1] +
            m[0, 3] * m[1, 1] * m[2, 0] * m[3, 2] - m[0, 1] * m[1, 3] * m[2, 0] * m[3, 2] -
            m[0, 3] * m[1, 0] * m[2, 1] * m[3, 2] + m[0, 0] * m[1, 3] * m[2, 1] * m[3, 2] +
            m[0, 1] * m[1, 0] * m[2, 3] * m[3, 2] - m[0, 0] * m[1, 1] * m[2, 3] * m[3, 2] -
            m[0, 2] * m[1, 1] * m[2, 0] * m[3, 3] + m[0, 1] * m[1, 2] * m[2, 0] * m[3, 3] +
            m[0, 2] * m[1, 0] * m[2, 1] * m[3, 3] - m[0, 0] * m[1, 2] * m[2, 1] * m[3, 3] -
            m[0, 1] * m[1, 0] * m[2, 2] * m[3, 3] + m[0, 0] * m[1, 1] * m[2, 2] * m[3, 3];
    }
    #endregion

    /// <summary>
	///pregunta si es igual.
	/// </summary>
	public override bool Equals(object other)
    {
        if (!(other is MyMatriz))
        {
            return false;
        }
        MyMatriz quaternion = (MyMatriz)other;
        return GetColumn(0).Equals(quaternion.GetColumn(0)) && GetColumn(1).Equals(quaternion.GetColumn(1)) && GetColumn(2).Equals(quaternion.GetColumn(2)) && GetColumn(3).Equals(quaternion.GetColumn(3));
    }
    /// <summary>
    ///pregunta si es igual.
    /// </summary>
    public bool Equals(MyMatriz other)
    {
        return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
    }
    #region Implicit conversions to and from Unity's Quaternion
    public static implicit operator Matrix4x4(MyMatriz me)
    {
        return new Matrix4x4(me.GetColumn(0), me.GetColumn(1), me.GetColumn(2), me.GetColumn(3));
    }
    public static implicit operator MyMatriz(Matrix4x4 other)
    {
        return new MyMatriz(other.GetColumn(0), other.GetColumn(1), other.GetColumn(2), other.GetColumn(3));
    }
    #endregion
}
//Constructor                           Listo
//La mentira    (El get usando [])      Listo
//Mat identidad                         Listo
//Mat zero                              Listo
//Matriz transpose    (Propierty)       Listo
//Matriz rotation                       Listo
//Matriz lossyScanel                    No
//Mat inverse                           Listo
//Rot transf y scale                    Listo?
//TRS                                   Listo
//Transpose                             Listo
//Equals                                Listo
//Get Row                               Listo
//Set colum Set row Set trs             Listo
//Y operadores                          Listo

