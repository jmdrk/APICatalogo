using System;

namespace ApiCatalogo.Controllers
{
    internal class ApiVersionAttribute : Attribute
    {
        private string v;
        private bool deprecated;

        public ApiVersionAttribute(string v, bool Deprecated)
        {
            this.v = v;
            deprecated = Deprecated;
        }

        public bool Deprecated { get; set; }
    }
}