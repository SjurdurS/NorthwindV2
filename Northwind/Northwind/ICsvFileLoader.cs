using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindNS
{
    public interface ICsvFileLoader
    {
        string[] categoryLines { get; }
        string[] productLines { get; }
        string[] orderLines { get; }
        string[] orderDetailLines { get; }
    }
}
