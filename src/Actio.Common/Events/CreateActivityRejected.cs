using System;

namespace Actio.Common.Events
{
    public class CreateActivityRejected: IRejectedEvent
    {
        public string Reason { get; }

        public string Code { get; }

        Guid UserId{ get; }

        protected CreateActivityRejected()
        {

        }

        public CreateActivityRejected(string reason, string code, Guid userId)
        {
            this.Reason = reason;
            this.Code = code;
            this.UserId = userId;
        }

    }
}