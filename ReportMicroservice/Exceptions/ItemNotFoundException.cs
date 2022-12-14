using System;

namespace ReportMicroservice.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        override public string Message
        {
            get { return "Item not found."; }
        }
    }
}
