using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamicTester.FlowActions
{
    public class EmailService
    {
        public async void SendEmail(string body,string subject, bool throwException)
        {
            if (throwException)
            {
                throw new Exception($"Send email exception. Body:{body} subject:{subject}");
            }
        } 
    }
}
