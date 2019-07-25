using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectChecker.DeviceModule
{
    public interface DeviceInterface
    {
        void GetCodeList(out Dictionary<int, string> codeList);
        //void Read
    }
}
