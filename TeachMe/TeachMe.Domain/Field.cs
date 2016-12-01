using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Domain
{
    public class Field
    {
        public Field(IReadOnlyList<IReadOnlyList<Sell>> sells)
        {
            if (sells == null)
                throw new ArgumentNullException("sells");

            this.Sells = sells;
        }

        public IReadOnlyList<IReadOnlyList<Sell>> Sells { get; private set; }
    }
}
