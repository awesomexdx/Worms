﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakes.models;

namespace Snakes.moves
{
    public interface IMove
    {
        Cell Move(Cell cell);
    }
}
