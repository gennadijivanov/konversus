using System;
using System.Collections.Generic;

namespace Conversus.Scheduler
{
    //"time": "09:30", 
    //"FIO": "Р... Иван Михайлович", 
    //"LegalName": "ООО ...", 
    //"Comments": "", 
    //"TurnPIN": "43210" 
    [Serializable]
    public class PureClientField
    {
        public string time { get; set; }
        public string FIO { get; set; }
        public string LegalName { get; set; }
        public string Comments { get; set; }
        public int TurnPIN { get; set; }
    }

    [Serializable]
    public class PureDayFields
    {
        public string date { get; set; }
        public List<PureClientField> times { get; set; } 
    }

    [Serializable]
    public class PureResponse
    {
        public string TakeError { get; set; }
        public List<PureDayFields> TakeRecep { get; set; } 
    }
}
