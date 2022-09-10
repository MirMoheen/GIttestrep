using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace SAGERPNEW2018.Models
{
    public class SmsApiConnection
    {
       
            private string Activate()
            {
                var result = GetSession();
                if (result.Equals(Constants.FAIL) || result.Equals(Constants.ERROR))
                {
                    result = result.ToString();
                    if (result.Contains(Constants.UnknownMSISDN))
                    {
                        return Constants.UnknownMSISDN;

                    }

                    else if (result.Contains(Constants.FailedLogin))
                    {
                        return Constants.FailedLogin;
                    }
                    else if (result.Contains(Constants.OPERATIONALMESSAGE))
                    {
                        return Constants.OPERATIONALMESSAGE;
                    }
                    else if (result.Contains(Constants.OperationNotallowed))
                    {
                        return Constants.FailedLogin;
                    }
                    else if (result.Contains(Constants.Outofcredit))
                    {
                        return Constants.Outofcredit;
                    }
                    else if (result.Contains(Constants.InvalidSession))
                    {
                        return Constants.InvalidSession;
                    }
                    else if (result.Contains(Constants.Parametermissing))
                    {
                        return Constants.Parametermissing;
                    }
                    else if (result.Contains(Constants.InvalidMasking))
                    {
                        return Constants.InvalidMasking;
                    }
                    else if (result.Contains(Constants.SMSTEXTMISSING))
                    {
                        return Constants.SMSTEXTMISSING;
                    }
                    else if (result.Contains(Constants.Blocked))
                    {
                        return Constants.Blocked;
                    }
                    else if (result.Contains(Constants.Invalidoperator))
                    {
                        return Constants.Invalidoperator;
                    }
                    else if (result.Contains(Constants.UnknownMessage))
                    {
                        return Constants.UnknownMessage;
                    }
                    else if (result.Contains(Constants.Listnameismissing))
                    {
                        return Constants.Listnameismissing;
                    }
                    else if (result.Contains(Constants.Cannotsendmessageatthespecified))
                    {
                        return Constants.Cannotsendmessageatthespecified;
                    }
                    else if (result.Contains(Constants.Nolistselected))
                    {
                        return Constants.Nolistselected;
                    }
                    else if (result.Contains(Constants.ERROR))
                    {
                        return Constants.ERROR;
                    }
                }
                var xml = new XmlDocument();
                xml.LoadXml(result);
                var response = xml.SelectSingleNode("/corpsms/response").InnerText;
                var data = xml.SelectSingleNode("/corpsms/data").InnerText;
                return response.Equals("OK") ? data : Constants.FAIL;
            }



            public string Send(string token, string to, string text, string smsMasking)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(smsMasking))
                    {
                        smsMasking = "Test Message";
                    }


                    if (token.Equals(Constants.TOKEN))
                    {
                        var id = Activate(); //SessionId
                        if (id.Equals(Constants.FAIL))
                            id = Activate(); //Checking Once Again
                        if (!id.Equals(Constants.FAIL))
                        {
                            string result = SendSMS(id, to, text, smsMasking);
                            if (result.Equals(Constants.FAIL))
                                result = SendSMS(id, to, text, smsMasking); //Checking Once Again                 
                            if (!result.Equals(Constants.FAIL))
                            {
                                XmlDocument xml = new XmlDocument();
                                if (result.Equals("SENT"))
                                {
                                    return Constants.SUCCESS;
                                }
                                else
                                {
                                    return Constants.SMS_FAILED;
                                }
                            }
                            else
                            {
                                return Constants.FAIL;
                            }
                        }
                        else
                            return Constants.FAIL;
                    }
                    else
                        return Constants.INVALID_REQUEST;
                }
                catch (Exception ex)
                {
                    return "0";

                }

            }

            private static string SendSMS(string sessionId, string to, string text, string smsMasking)
            {
                var responseFromServer = "";
                try
                {
                    //string temp = "test";
                    //string numtemp = "923336166543";
                    var reqUrl = "https://telenorcsms.com.pk:27677/corporate_sms2/api/sendsms.jsp?session_id=" + sessionId + "&to=" + to + "&text=" + text + "&mask=" + smsMasking + "&unicode=true";
                    WebRequest request = null;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    request = WebRequest.Create(reqUrl);
                    request.ContentType = "text/xml";
                    var response = request.GetResponse();
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                    string comparexml = responseFromServer.ToString();
                    if (responseFromServer.Equals(Constants.UnknownMSISDN))
                    {
                        return Constants.UnknownMSISDN;

                    }
                    if (comparexml.Contains(Constants.FailedLogin))
                    {
                        return Constants.FailedLogin;
                    }
                    //if (comparexml.Contains(Constants.OPERATIONALMESSAGE))
                    //  {
                    //      return Constants.OPERATIONALMESSAGE;
                    //  }
                    if (comparexml.Contains(Constants.OperationNotallowed))
                    {
                        return Constants.FailedLogin;
                    }
                    if (comparexml.Contains(Constants.Outofcredit))
                    {
                        return Constants.Outofcredit;
                    }
                    if (comparexml.Contains(Constants.InvalidSession))
                    {
                        return Constants.InvalidSession;
                    }
                    if (comparexml.Contains(Constants.Parametermissing))
                    {
                        return Constants.Parametermissing;
                    }
                    if (responseFromServer.Equals(Constants.InvalidMasking))
                    {
                        return Constants.InvalidMasking;
                    }
                    if (responseFromServer.Equals(Constants.SMSTEXTMISSING))
                    {
                        return Constants.SMSTEXTMISSING;
                    }
                    if (responseFromServer.Equals(Constants.Blocked))
                    {
                        return Constants.Blocked;
                    }
                    if (responseFromServer.Equals(Constants.Invalidoperator))
                    {
                        return Constants.Invalidoperator;
                    }
                    if (responseFromServer.Equals(Constants.UnknownMessage))
                    {
                        return Constants.UnknownMessage;
                    }
                    if (responseFromServer.Equals(Constants.Listnameismissing))
                    {
                        return Constants.Listnameismissing;
                    }
                    if (responseFromServer.Equals(Constants.Cannotsendmessageatthespecified))
                    {
                        return Constants.Cannotsendmessageatthespecified;
                    }
                    if (responseFromServer.Equals(Constants.Nolistselected))
                    {
                        return Constants.Nolistselected;
                    }
                    if (responseFromServer.Equals(Constants.ERROR))
                    {
                        return Constants.ERROR;
                    }
                    else
                    {
                        return Constants.SMS_SENT;
                    }
                }
                catch (Exception)
                {
                    return Constants.FAIL;
                }

            }

            private string GetSession()
            {
                var responseFromServer = "";
                try
                {
                    //////var reqUrl =telenorcsms.com.pk:27677/corporate_sms2/api/auth.jsp?msisdn=923458498888&password=pepsiscl1";
                      var reqUrl = "";

                    //  var reqUrl = "https://telenorcsms.com.pk:27677/corporate_sms2/api/auth.jsp?msisdn=923458498888&password=pepsismsportal";
                    WebRequest request = null;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    request = WebRequest.Create(reqUrl);
                    request.ContentType = "text/xml";
                    var response = request.GetResponse();
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    response.Close();

                }
                catch (Exception ex)
                {
                    return Constants.FAIL;
                }
                return responseFromServer;
            }
        }

    


    public static class Constants
    {
        /// <summary>
        /// //////// SMS Constants ///////////////
        /// </summary>
        /// 
        public const string FAIL = "FAIL";
        public const string ERROR = "ERROR";
        public const string SUCCESS = "SUCCESS";
        public const string SMS_SENT = "SENT";
        public const string SMS_FAILED = "NOT SENT";
        public const string TOKEN = "S25P3774";
        public const string FailedLogin = "Error 200"; // Failed login
        public const string UnknownMSISDN = "Error 201"; // Unknown MSISDN
        public const string OperationNotallowed = "Error 203"; // SMS operation not allowed 
        public const string Outofcredit = "Error 100";
        public const string Parametermissing = "Error 101"; //Field or input parameter missing 
        public const string InvalidSession = "Error 102";//Invalid session ID or the session has expired. Login again. 
        public const string InvalidMasking = "Error 103"; // Invalid Mask 
        public const string Blocked = "Error 300";// Account has been blocked/suspended
        public const string SMSTEXTMISSING = "Error 502";//SMS TEXT missing
        public const string Invalidoperator = "Error 104"; // Invalid operator ID
        public const string UnknownMessage = "Error 211"; // Unknown Message ID
        public const string Listnameismissing = "Error 40"; //1 List name is missing
        public const string Nolistselected = "Error 503"; // No list selected or one of the list IDs
        public const string Cannotsendmessageatthespecified = "Error 506";// Cannot send message at the specified time.Please specify a different time

        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>


        public const string CONNECTION_STRING_KEY = "ConnectionString";
        public const string SITE_URL = "SiteUrl";
        public const string SITE_NAME = "SiteName";

        public const string UPLOADED_FILES = "UploadedFiles";
        public const string OPERATIONMESSAGE = "OperationalMessage";
        public const string PAGE_TITLE = "pageTitle";

        public const string NO_RECORD_FOUND = "No Record Found";
        public const string INVALID_TOKEN = "Invalid Token";
        public const string NOT_ACCEPTABLE = "Request Not Acceptable";
        public const string INVALID_REQUEST = "Invalid Request";
        public const string REQUEST_DONE = "Request Done Successfully";
        public const string ENTER_REQUIRED_FIELD = "Please enter required fields !";
        public const string DUPLICATE_VALUES = "Duplicate Values are not allowed !";
        public const string INVALID_DATETIME = "The date entered in the forms is not correct.";
        public const string OPERATION_DONE = "Operation Done ";
        public const string OPERATION_DONEx = "";
        public const string OPERATION_FAILED = "Operation Failed";
        public const string ATTACHEMENT_CHECK = "Atleast Add One Document";
        public const string UNIQUE_KEY_EXCEPTION = "UNIQUE KEY";
        public const string NO_DETAIL_RECORD_EXISTS = "There's no detail record. Please enter atleast one detail record.";
        public const string TRUE = "true";
        public const string ERROR_OCCURED = "Error Occured!";
        public const string VALIDATION_FAILED = "Validation failed!";

        public const string SMTP_SERVER_KEY = "";
        public const string SMTP_PORT_KEY = "25";
        public const string SMTP_SSL_KEY = "false";
        public const string SMTP_AUTH_KEY = "true";
        public const string SMTP_USER_NAME_KEY = "";
        public const string SMTP_USER_EMAIL_KEY = "";
        public const string SMTP_USER_PASS_KEY = "";

        public const string DATE_FORMAT_SERVER_SIDE = "dd-MMM-yyyy";
        public const string DATE_FORMAT_CLIENT_SIDE = "dd-M-yy";
        public const string DATE_FORMAT_MASK_CLIENT_SIDE = "99-aaa-9999";

        public const int YEAR_RANGE_START = 1980;
        public const int YEAR_RANGE_END = 2050;

        public const string LOADING_GIF = @"<div style='text-align:center'><h2><i class='fa fa-spinner fa-spin fa-2x'></i>&nbsp;&nbsp;Loading... Please wait...!</h2></div>";
        public const string REPORT_PATH = "../../SMSReports/";

        public static string OPERATIONALMESSAGE { get; internal set; }
    }
}