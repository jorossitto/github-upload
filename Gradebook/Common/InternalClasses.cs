using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    internal abstract class Class0
    {
        public virtual string GetString0()
        {
            return "abc";
        }

        protected virtual string GetString00()
        {
            return "abcd";
        }
    }

    internal class Class1 : Class0
    {
        public string GetString1()
        {
            return "a";
        }

        internal class Class2 : Class0
        {
            public override string GetString0()
            {
                return "xyz";
            }

            internal string GetString2()
            {
                return "b";
            }

            private class Class3
            {
                private string GetString3()
                {
                    return "c";
                }
            }
        }
    }
}
