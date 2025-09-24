using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Services
{
       public class TestService : ITestService
    {
        private bool _useAlt = false;

        public string GetMessage()
        {
            return _useAlt ? "Alternate message" : "Default message";
        }

        public void Toggle()
        {
            _useAlt = !_useAlt;
        }
    }

}