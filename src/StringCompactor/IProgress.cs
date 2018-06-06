using System;
using System.Collections.Generic;
using System.Text;

namespace StringCompactor
{
    public interface IProgress
    {
        void ProgressChanged(int i, int total);
    }
}
