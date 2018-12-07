using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGraphics1
{
    public class Validation
    {

        public bool CheckDrawingParams(string R1, string R2, string R3, string A)
        {
            if (R1 == "" || R2 == "" || R3 == "" || A == "")
            {
                return false;
            }
            if (!Int32.TryParse(R1, out int r1) ||
            !Int32.TryParse(R2, out int r2) ||
            !Int32.TryParse(R3, out int r3) ||
            !Int32.TryParse(A, out int a) ||
            r1 >= r2 || a <= r3 + 20 || r1 <= 5 || r2 <= 5 || r3 <= 5 || a <= 0) { return false; }
            return true;
        }

        public bool CheckCurveParams(string A,ref int a)
        {
            if (A == "")
            {
                return false;
            }
            if (!Int32.TryParse(A, out a))
            {
                return false;
            }
            return true;
        }
    }
}
