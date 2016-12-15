using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMe.Infrastructure;

namespace TeachMe.Domain
{
    public class Field
    {
        public Field(int rows, int columns)
        {
            IsNegative(rows);
            IsNegative(columns);
            
            var sells = new List<List<Sell>>();

            for (var x = 0; x < columns; x++)
            {
                sells.Add(new List<Sell>());

                for (var y = 0; y < rows; y++)
                {
                    sells[x].Add(new Sell(new Location(x, y)));
                }
            }

            Sells = sells.AsReadOnly();
        }
        public Field(IReadOnlyList<IReadOnlyList<Sell>> sells)
        {
            if (sells == null)
                throw new ArgumentNullException("sells");

            Sells = sells;
        }

        public IReadOnlyList<IReadOnlyList<Sell>> Sells { get; }
        public int Rows => Sells.Count;
        public int Colums => Sells[0].Count;

        private void IsNegative(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Value = " + value);
        }
    }
}
