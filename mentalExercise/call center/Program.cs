using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//15.1 Cracking tech interview Qn
namespace call_center
{
    class Employee
    {
        public string  Type{get;set;}
        public string EmployeeID { get; set; }
        public Employee()
        {
            Type = "Employee";
            EmployeeID = Guid.NewGuid().ToString();
        }
    }

    class TL: Employee
    {
        public TL()
        { 
            Type = "TL";
        }

    }
    class PM: Employee
    {
        public PM()
        {
             Type = "PM";
        }
    }

    class CallCenter
    {
        Queue<Employee> _freeEmployeeQ;
        Queue<Employee> _busyEmployeeQ;
        Queue<Call> _incomingCallQ;
        List<Call> _CallQ;

        Employee _PM;
        Employee _TL;
        enum _callStatus { CALL_STATUS_NEW, CALL_STATUS_FRESHER, CALL_STATUS_ESCALATIONTL, CALL_STATUS_ESCALATIONPM, CALL_STATUS_CLOSED };

        
        public CallCenter()
        {
            _freeEmployeeQ = new Queue<Employee>();
            _busyEmployeeQ = new Queue<Employee>();
            _incomingCallQ = new Queue<Call>();
            _CallQ = new List<Call>();
            _PM = null;
            _TL = null;
        }

        //Add n freshers to free Q
        public void AddEmployees(int pNumEmployees)
        {
            for (int i = 0; i < pNumEmployees; i++)
                _freeEmployeeQ.Enqueue(new Employee());
        }
 
        //Add TL or PM
        public void AddTLorPM(string pType)
        {
            if (pType == "PM")
            {
                if (_PM == null)
                _PM = new PM();
            }
            else if (pType == "TL")
            {
                if (_TL == null)
                    _TL = new TL();
            }
        }

        public void IncomingCall(int pId, int pStatus = (int)_callStatus.CALL_STATUS_NEW)
        {
            _incomingCallQ.Enqueue(new Call(pId, pStatus));
        }
          
        //TODO is this object oriented enought (alternatives toswitch statement?)
        public void getCallHandler(int pId, int pCallStatus)
        {
            switch (pCallStatus)
            {
                case (int)_callStatus.CALL_STATUS_NEW:
                    //enQ in CallQ, assign owner, deque from incomingCallQ, update status
                    break;
                case (int)_callStatus.CALL_STATUS_FRESHER:
                    //update status and assign tl or pm
                    break;
                case (int)_callStatus.CALL_STATUS_ESCALATIONTL:
                    //update status and assign pm
                    break;
                case (int)_callStatus.CALL_STATUS_ESCALATIONPM:
                    //update status and remove from list
                    break;
                case (int)_callStatus.CALL_STATUS_CLOSED:
                    //if in list, remove
                    break;

            }
            
        }
    }
    
    class Call
    {
        public int CallId {get;set;}
        int CallStatus { get; set; }
        string CallOwner { get; set; }
        public Call(int pId, int pStatus)
        {
            CallId = pId;
            CallStatus = pStatus;
            CallOwner = null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CallCenter blrCenter = new CallCenter();
            blrCenter.AddEmployees(5);
            blrCenter.AddTLorPM("PM");

            //blrCenter.getCallHandler("CALL_STATUS_NEW");
        }
    }
}
