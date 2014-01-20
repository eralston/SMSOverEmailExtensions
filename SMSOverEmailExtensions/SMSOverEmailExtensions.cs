using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net.Mime;

namespace SMSOverEmail
{
    /// <summary>
    /// Re-implements code from the original SMSOverEmail project as extensions to the MailMessage class
    /// Original project: http://smsoveremail.codeplex.com/
    /// This implementation is built from the MailExtesnsions class and is 
    /// </summary>
    public static class SMSOverEmailExtensions
    {
        /// <summary>
        /// Adds a to Address to the message, optionally setting the display name
        /// </summary>
        /// <param name="message"></param>
        /// <param name="address"></param>
        /// <param name="displayName"></param>
        public static void AddToSMS(this MailMessage message, string phoneNumber, Carrier carrier, string displayName = null)
        {
            // Map the phone number + carrier into an e-mail address
            string emailForPhoneAndCarrier = CarrierInfo.GetSMSAddress(phoneNumber,carrier);
            // Add it to the message
            message.AddTo(emailForPhoneAndCarrier, displayName);
        }

        /// <summary>
        /// Sets the "From" field as an SMS e-mail mapping
        /// </summary>
        /// <param name="message"></param>
        /// <param name="address"></param>
        /// <param name="displayName"></param>
        public static void SetFromSMS(this MailMessage message, string phoneNumber, Carrier carrier, string displayName = null)
        {
            // Map the phone number + carrier into an e-mail address
            string emailForPhoneAndCarrier = CarrierInfo.GetSMSAddress(phoneNumber, carrier);
            // Add it to the message
            message.SetFrom(emailForPhoneAndCarrier, displayName);
        }
    }
}
