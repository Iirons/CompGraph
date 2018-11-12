using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGraphics1.Types
{
    public class Matrix
    {
        public double[,] matrix;

        public int Row { get; protected set; }
        public int Column { get; protected set; }

        public Matrix(int row, int column)
        {
            Row = row;
            Column = column;
            matrix = new double[row, column];
        }

        public Matrix(double _11, double _12, double _13, 
            double _21, double _22, double _23,
            double _31, double _32, double _33)
        {
            Row = 3;
            Column = 3;
            matrix = new double[3, 3]
            {
                {
                    _11,_12,_13
                },
                {
                    _21,_22,_23
                },
                {
                    _31,_32,_33
                }

            };
        }

        public Matrix(double x, double y, double w)
        {
            Row = 1;
            Column = 3;
            matrix = new double[1,3]
            {
                {
                    x,y,w
                }

            };
        }

        public Matrix Multiple(Matrix value)
        {
            Matrix result = new Matrix(Row, value.Column);
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < value.Column; j++)
                    for (int k = 0; k < value.Row; k++)
                        result.matrix[i, j] += matrix[i, k] * value.matrix[k, j];
            return result;
        }

        public void Read()
        {
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Column; j++)
                {
                    Console.Write("Введите элемент [{0},{1}]: ", i + 1, j + 1);
                    matrix[i, j] = System.Convert.ToDouble(Console.ReadLine());
                }
        }

        public void Print()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                    Console.Write("{0:f2} ", matrix[i, j]);
                Console.WriteLine();
            }
        }
    }
}
