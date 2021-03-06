﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ITreeService
    {
        Node GetTree(string dwp, string filename = null);
        void SaveTree(string dwp);
    }
}
