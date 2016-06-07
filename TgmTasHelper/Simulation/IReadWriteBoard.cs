﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface IReadWriteBoard : IReadOnlyBoard
    {
        int ClearCompletedLines();
        void Set(int x, int y, TetrominoType tetrominoType);
    }
}