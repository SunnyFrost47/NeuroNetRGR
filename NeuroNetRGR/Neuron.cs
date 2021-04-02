using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroNetRGR
{
    class Neuron
    {
        public double[] Weights, Input;
        public double Out, Error, Drv, Bias, BiasWeight;

        public Neuron(int inputsCount)
        {
            Weights = new double[inputsCount];
            InitWeights();
        }

        int seed = (int)DateTime.Now.Ticks;
        private double Rnd() => ((new Random(seed++)).NextDouble() * 2 - 1);

        public void InitWeights()
        {
            var l = Weights.Length;
            for (int i = 0; i < l; i++) Weights[i] = 2 * Rnd() / l;
            BiasWeight = 2 * Rnd() / l;
        }

        public double Compute(double[] input)
        {
            Input = input;
            Out = 0;
            for (int i = 0; i < input.Length; i++) Out += input[i] * Weights[i];
            Out += BiasWeight;
            Out = Math.Tanh(Out);
            Drv = (1 - Out) * (1 + Out);

            return Out;
        }

        public void TuneWeight(double learnRate)
        {
            for (int i = 0; i < Input.Length; i++) Weights[i] += Input[i] * Error * learnRate;
            BiasWeight += Error * learnRate;
        }
    }
}
