namespace Generic_Value_Adapter;


// vector2f vector3i

public interface IInteger
{
    int Value { get; }
}

public static class Demensions 
{
    public class  Two : IInteger
    {
        public int Value => 2;
    }

    public class Three : IInteger
    {
        public int Value => 3;
    }
}

public class Vector<T, D>
    where D : IInteger, new()
{
    protected T[] data;

    public Vector()
    {
        data = new T[new D().Value];
    }

    public T this[int index]
    {
        get => data[index];
        set => data[index] = value;
    }

    //public T X // NOT A GOOD IDEA
    //{
    //    get => data[0];
    //    set => data[0] = value;

    //}
    //public T Y
    //{
    //    get => data[1];
    //    set => data[1] = value;
    //}

    //public Vector(T x, T y,T z ...)// not a good idea either

    public Vector(params T[] values)
    {
        var requiredSize = new D().Value;
        data = new T[requiredSize];
        var providedSize = values.Length;

        for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
        {
            data[i] = values[i];
        }
    }

}

public class VectorOfInt<D> : Vector<int, D>
    where D : IInteger, new()
{
    public VectorOfInt()
    {

    }
    public VectorOfInt(params int[] values) : base(values)
    {
    }

    public static VectorOfInt<D> operator +(VectorOfInt<D> lhs, VectorOfInt<D> rhs)// making v+vv possible
    {
        var result = new VectorOfInt<D>();
        var dim = new D().Value;
        for (int i = 0; i < dim; i++)
        {
            result[i] = lhs[i] + rhs[i];
        }
        return result;
    }

}

public class Vector2i : VectorOfInt< Demensions.Two>
{
    public Vector2i()
    {
        
    }
    public Vector2i(params int[] values) : base(values)
    {
    }

}


class Demo
{
    static void Main(string[] args)
    {
        var v = new Vector2i(1,2);
        v[0] = 0;

        var vv = new Vector2i(3,2);

        var result = v + vv;
    }
}
