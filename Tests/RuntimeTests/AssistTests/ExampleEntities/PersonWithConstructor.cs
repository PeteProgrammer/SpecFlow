using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechTalk.SpecFlow.RuntimeTests.AssistTests.ExampleEntities
{
    class PersonWithConstructor : Person
    {
        public PersonWithConstructor(int id)
        {
            ID = id;
        }

        public int ID { get; private set; }
    }
}
