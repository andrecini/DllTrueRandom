using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Presys.Random
{
    /// <summary>
    /// Essa classe Gera números realmentes aleatórios, que 
    /// seguem a curva normal.
    /// </summary>
    public class TrueRandom
    {
        private Stopwatch sw = new Stopwatch();
        private System.Random rdm = new System.Random();

        #region Set Binary
        /// <summary>
        /// Retorna 1 ou 0 aleatóriamente.
        /// </summary>
        /// <returns>1 ou 0</returns>
        public int SetBinary()
        {
            return int.Parse(SetInterval().ToString()) % 2;
        }

        /// <summary>
        /// Retorna um vetor com 1 ou 0 preenchidos aleatóriamente.
        /// </summary>
        /// <param name="quantify"></param>
        /// <returns>Vetor de inteiros com 1 ou 0</returns>
        public int[] SetBinary(int quantify)
        {
            int[] output = new int[quantify];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = SetBinary() % 2;
            }

            return output;
        }

        private double SetInterval()
        {
            sw.Start();
            Delay();
            sw.Stop();

            double interval = sw.ElapsedTicks;

            VerifyInterval(ref interval);

            return interval;
        }

        private void Delay()
        {
            sw.Start();
            sw.Stop();

            double interval = sw.ElapsedTicks;

            VerifyInterval(ref interval);

            for (int i = 0; i < interval; i++)
            {
                continue;
            }
        }

        private void VerifyInterval(ref double interval)
        {
            if (interval > 5)
            {
                interval = rdm.Next(5);
            }
        }
        #endregion

        #region Set Integer
        /// <summary>
        /// Retorna um long aleatório.
        /// </summary>
        /// <param name="max">Valor Limite</param>
        /// <returns>Long aleatório entre 0 e o número Max</returns>
        public long SetInteger(int max)
        {
            long maxValue = DecimalToBinary(max.ToString());
            string value;

            do
            {
                value = SetValue(maxValue.ToString().Length);
            } while (double.Parse(value) > maxValue);

            return BinaryToDecimal(value);
        }

        /// <summary>
        /// Retorna um long aleatório.
        /// </summary>
        /// <param name="min">Valor mínimo Limite</param>
        /// <param name="max">Valor máximo Limite</param>
        /// <returns>Long aleatório entre o número Min e o número Max</returns>
        public long SetInteger(int min, int max)
        {
            VerifyMaxAndMin(ref min, ref max);

            long maxValue = DecimalToBinary(max.ToString());
            long minValue = DecimalToBinary(min.ToString());
            string value;

            do
            {
                value = SetValue(maxValue.ToString().Length);
            } while (double.Parse(value) > maxValue || double.Parse(value) < minValue);

            return BinaryToDecimal(value);
        }

        /// <summary>
        /// Vetor de long preenchido aleatóriamente com números entre o intervalo definido.
        /// </summary>
        /// <param name="quantify">Tamanho do vetor</param>
        /// <param name="min">Limite mínimo</param>
        /// <param name="max">Limite máximo</param>
        /// <returns>Vetor de long preenchido com números entre o intervalo definido.</returns>
        public long[] SetInteger(uint quantify, int min, int max)
        {
            VerifyMaxAndMin(ref min, ref max);

            long[] output = new long[quantify];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = SetInteger(min, max);
            }

            return output;
        }

        private long DecimalToBinary(string numero)
        {

            string valor = "";

            int dividendo = Convert.ToInt32(numero);

            if (dividendo == 0 || dividendo == 1)
            {

                return dividendo;

            }

            else
            {

                while (dividendo > 0)
                {

                    valor += Convert.ToString(dividendo % 2);

                    dividendo = dividendo / 2;

                }

                return long.Parse(InvertString(valor));

            }

        }

        private int BinaryToDecimal(string valorBinario)
        {

            int expoente = 0;

            int numero;

            int soma = 0;

            string numeroInvertido = InvertString(valorBinario);

            for (int i = 0; i < numeroInvertido.Length; i++)
            {

                //pega dígito por dígito do número digitado

                numero = Convert.ToInt32(numeroInvertido.Substring(i, 1));

                //multiplica o dígito por 2 elevado ao expoente, e armazena o resultado em soma

                soma += numero * (int)Math.Pow(2, expoente);

                // incrementa o expoente

                expoente++;

            }

            return soma;

        }

        private string InvertString(string str)
        {
            int tamanho = str.Length;

            char[] caracteres = new char[tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                caracteres[i] = str[tamanho - 1 - i];
            }

            return new string(caracteres);
        }

        private string SetValue(int length)
        {
            string value = string.Empty;

            for (int i = 0; i < length; i++)
            {
                value += SetBinary();
            }

            return value;
        }
        #endregion

        private void VerifyMaxAndMin(ref int min, ref int max)
        {
            if(min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }
        }
    }
}
