﻿using System.Collections.Generic;

namespace RestApiTester
{
    public class Environment
    {
        public string Name { get; set; }
        public IDictionary<string,string> Variables { get; set; }
    }
}