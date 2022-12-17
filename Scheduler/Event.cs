using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    [Serializable]
    public class Event
    {
        public string name { get; set; }    
        public DateTime dateTime { get; set; }  
    }
}
