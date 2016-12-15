using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMe.Appl.Exception
{
    public class SearchCommandViewerInListBoxException : System.Exception
    {
        public SearchCommandViewerInListBoxException(System.Exception innerException) : base("", innerException)
        { }

        public SearchCommandViewerInListBoxException() : this(null)
        { }
    }
}
