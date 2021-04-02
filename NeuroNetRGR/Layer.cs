using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroNetRGR
{
    class Layer
    {
        public Neuron[] neurons;
        public Layer nextLayer;
        public double[] Outputs;

        public Layer(int neuronsCount, int inputsCount)
        {
            neurons = new Neuron[neuronsCount];
            for (int i = 0; i < neurons.Length; i++) neurons[i] = new Neuron(inputsCount);
        }

        public double[] ComputeForward(double[] input)
        {
            Outputs = new double[neurons.Length];
            for (int i = 0; i < neurons.Length; i++) Outputs[i] = neurons[i].Compute(input);

            return Outputs;
        }

        public void ComputeBackward()
        {
            if (nextLayer != null)
                for (int i = 0; i < neurons.Length; i++) OneBackItter(i);
        }

        private void OneBackItter(int i)
        {
            double backPropError = 0;
            for (int j = 0; j < nextLayer.neurons.Length; j++)
                backPropError += nextLayer.neurons[j].Weights[i] * nextLayer.neurons[j].Error;

            neurons[i].Error = backPropError * neurons[i].Drv;
        }
    }
}
