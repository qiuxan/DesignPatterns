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

public class Vector<TSelf, T, D>
    where D : IInteger, new()
    where TSelf : Vector<TSelf, T, D>, new()
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

    public static TSelf  Create(params T[] values)
    {
        var result = new TSelf();
        var requiredSize = new D().Value;
        result.data = new T[requiredSize];
        var providedSize = values.Length;

        for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
        {
            result.data[i] = values[i];
        }
        return result;
    }
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

public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
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

public class VectorOfFloat<TSelf,D> : Vector<TSelf, float, D>
    where D : IInteger, new()
    where TSelf : Vector<TSelf, float, D>, new()
{


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

public class Vector3f : VectorOfFloat<Vector3f,Demensions.Three>
{ 

}




class Demo
{
    static void Main(string[] args)
    {
        var v = new Vector2i(1,2);
        v[0] = 0;

        var vv = new Vector2i(3,2);

        var result = v + vv;

        Vector3f u = Vector3f.Create(3.5f, 2.2f, 1.1f);
    }
}
