using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ClsXMLResultado
    {
        public string mensaje {get;set;}
        public string errormensaje { get; set; }
        public bool error { get; set; }
    }
}