using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroNetRGR
{
    class NeuralNetwork
    {
        public Layer[] Layers;

        public NeuralNetwork(int inputs, params int[] structure)
        {
            Layers = new Layer[structure.Length];
            Layers[0] = new Layer(structure[0], inputs);

            for (int i = 1; i < structure.Length; i++)
            {
                var neuronsCount = structure[i];
                var inputsCount = structure[i - 1];

                Layers[i] = new Layer(neuronsCount, inputsCount);
            }

            for (int i = 0; i < Layers.Length - 1; i++) Layers[i].nextLayer = Layers[i + 1];
        }

        public double[] Compute(double[] vector)
        {
            ComputeForward(vector);
            return Layers.Last().Outputs;
        }

        public void ComputeForward(double[] input)
        {
            for (int i = 0; i < Layers.Length; i++) input = Layers[i].ComputeForward(input);
        }

        public void ComputeBackward(double[] output)
        {
            var lastLayer = Layers.Last();
            for (int i = 0; i < output.Length; i++)
                lastLayer.neurons[i].Error = (output[i] - lastLayer.neurons[i].Out) * lastLayer.neurons[i].Drv;

            for (int i = Layers.Length - 2; i >= 0; i--) Layers[i].ComputeBackward();
        }

        public void TuneWeights()
        {
            for (int i = 0; i < Layers.Length; i++)
                for (int j = 0; j < Layers[i].neurons.Length; j++)
                    Layers[i].neurons[j].TuneWeight(learnRate);
        }

        public void OneTune(double[][] inputs, double[][] outputs, int index = -1)
        {
            ComputeForward(inputs[index]);
            ComputeBackward(outputs[index]);
            TuneWeights();
        }

        double learnRate;
        public void Learn(double[][] inputs, double[][] outputs, int epoh = 50, double learnRate = 0.01)
        {
            this.learnRate = learnRate;

            for (int j = 0; j < epoh; j++)
                for (int i = 0; i < inputs.Count(); i++) OneTune(inputs, outputs, i);
        }
    }
}