﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioLib
{
    public class DniInvalidoException:ApplicationException
    {
        public DniInvalidoException() { }
        public DniInvalidoException(string message) : base(message) { }

    }
}