using System;
using System.Collections.Generic;
using TeachMe.Infrastructure;

namespace TeachMe.Domain.Field
{
    public class Field
    {
        public Field(int size)
        {
            IsNegative(size);
            
            var sells = new List<List<Sell>>();

            for (var x = 0; x < size; x++)
            {
                sells.Add(new List<Sell>());

                for (var y = 0; y < size; y++)
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
