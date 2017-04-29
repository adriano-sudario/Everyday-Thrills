using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Inputs.Interface
{
    public interface IInput
    {
        void GetInputs();

        float MoveX();
        float MoveY();
        //float MoveAngleInDegrees();
        bool Select();
        bool Start();
        bool Back();
    }
}
