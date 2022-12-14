using System;

namespace ContactMicroservice.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        private readonly Guid guid;

        public ItemNotFoundException(Guid guid)
        {
            this.guid = guid;
        }

        override public string Message
        {
            get { return "Item not found with guid: " + guid + "."; }
        }
    }
}
