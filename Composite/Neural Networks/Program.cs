namespace Neural_Networks;

public class Neuron
{
    public float Value;
    public List<Neuron> In, Out;

    public void ConnectTo(Neuron other)
    {
        Out.Add(other);
        other.In.Add(this);
    }

    
}

internal class Program
{
    static void Main(string[] args)
    {
        var neuron1 = new Neuron();
        var neuron2 = new Neuron();

        neuron1.ConnectTo(neuron2);
    }
}
