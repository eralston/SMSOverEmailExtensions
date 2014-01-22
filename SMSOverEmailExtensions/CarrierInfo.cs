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
    /// An enum organizing the list of carrier supported by the API
    /// </summary>
    public enum Carrier
    {
        ATTWireless,
        VerizonWireless,
        TMobile,
        SprintPCS,
        VirginMobile,
        USCellular,
        NextelWireless,
        BoostMobile,
        Alltell,
        CBeyond,
        GeneralCommunicationsInc,
        BlueskyCommunications,
        GoldenStateCellular,
        TelusMobility,
        RogersWireless,
        CincinnatiBell,
        HawaiianTelcomWireless,
        iwirelessTMobileAffiliate,
        iwirelessSprintAffiliate,
        Claro,
        Helio,
        PocketWireless,
        Voyager,
        AirfireMobile,
        AlaskaCommunications,
        Ameritech,
        ATTEnterprisePaging,
        BellSouth,
        BluegrassCellular,
        Cellcom,
        CellularSouth,
        CharitonValleyWireless,
        Cingular,
        Cleartalk,
        Cricket,
        CSpireWireless,
        EdgeWireless,
        ElementMobile,
        Esendex,
        Kajeet,
        LongLines,
        MetroPCS,
        Nextech,
        PagePlusCellular,
        QwestWireless,
        RedPocketMobile,
        SimpleMobile,
        Southernlinc,
        SouthCentralCommunications,
        StraightTalkATTSIM,
        StraightTalkTMobileSIM,
        StraightTalkVerizonSIM,
        SyringaWireless,
        Teleflip,
        Unicel,
        USAMobility,
        Viaero,
        WestCentralWireless,
        XITCommunications,
        TracFone,
        CentennialWireless,
        PanaceaMobile,
        RoutoMessaging,
        AppalachianWireless
    }

    /// <summary>
    /// Refactors code from the original SMSOverEmail project as extensions to the MailMessage class
    /// Original project: http://smsoveremail.codeplex.com/
    /// This was re-structured for two reasons:
    /// 1) Making it an extension worked better with some existing code
    /// 2) Simplify presenting the list of options in MVC's razor
    /// This implementation is built from the MailExtesnsions class and is
    /// </summary>
    public class CarrierInfo
    {
        #region Instance Fields & Methods

        /// <summary>
        /// Constructor that sets all fields of the Carrier info object
        /// </summary>
        /// <param name="carrier"></param>
        /// <param name="name"></param>
        /// <param name="template"></param>
        public CarrierInfo(Carrier carrier, string name, string template)
        {
            this.Carrier = carrier;
            this.Name = name;
            this.Template = template;
        }

        public Carrier Carrier { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }

        #endregion

        #region Static Fields & Methods

        /// <summary>
        /// For formatting natural phone number strings into plain blocks of digits
        /// </summary>
        static Regex phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

        /// <summary>
        /// The dictionary of email addresses to use for each carrier.  The format is #@Address.com.
        /// </summary>
        static Dictionary<Carrier, CarrierInfo> _CarrierMapping = new Dictionary<Carrier, CarrierInfo>
        {
                 { Carrier.ATTWireless , new CarrierInfo(Carrier.ATTWireless, "AT&T Wireless", "#@txt.att.net") },
                 { Carrier.VerizonWireless , new CarrierInfo(Carrier.VerizonWireless, "Verizon Wireless", "#@vtext.com") },
                 { Carrier.TMobile , new CarrierInfo(Carrier.TMobile, "T-Mobile", "#@tmomail.net") },
                 { Carrier.SprintPCS , new CarrierInfo(Carrier.SprintPCS, "Sprint PCS", "#@messaging.sprintpcs.com") },
                 { Carrier.VirginMobile , new CarrierInfo(Carrier.VirginMobile, "Virgin Mobile", "#@vmobl.com") },
                 { Carrier.USCellular , new CarrierInfo(Carrier.USCellular, "US Cellular", "#@email.uscc.net") },
                 { Carrier.NextelWireless , new CarrierInfo(Carrier.NextelWireless, "Nextel Wireless", "#@messaging.nextel.com") },
                 { Carrier.BoostMobile , new CarrierInfo(Carrier.BoostMobile, "Boost Mobile", "#@myboostmobile.com") },
                 { Carrier.Alltell , new CarrierInfo(Carrier.Alltell, "Alltell", "#@message.alltel.com") },
                 { Carrier.CBeyond , new CarrierInfo(Carrier.CBeyond, "C Beyond", "#@cbeyond.sprintpcs.com") },
                 { Carrier.GeneralCommunicationsInc , new CarrierInfo(Carrier.GeneralCommunicationsInc, "General Communications Inc.", "#@mobile.gci.net") },
                 { Carrier.BlueskyCommunications , new CarrierInfo(Carrier.BlueskyCommunications, "Bluesky Communications", "#@psms.bluesky.as") },
                 { Carrier.GoldenStateCellular , new CarrierInfo(Carrier.GoldenStateCellular, "Golden State Cellular", "#@gscsms.com") },
                 { Carrier.TelusMobility , new CarrierInfo(Carrier.TelusMobility, "Telus Mobility", "#@msg.telus.com") },
                 { Carrier.RogersWireless , new CarrierInfo(Carrier.RogersWireless, "Rogers Wireless", "#@pcs.rogers.com") },
                 { Carrier.CincinnatiBell , new CarrierInfo(Carrier.CincinnatiBell, "Cincinnati Bell", "#@gocbw.com") },
                 { Carrier.HawaiianTelcomWireless , new CarrierInfo(Carrier.HawaiianTelcomWireless, "Hawaiian Telcom Wireless", "#@hawaii.sprintpcs.com") },
                 { Carrier.iwirelessTMobileAffiliate , new CarrierInfo(Carrier.iwirelessTMobileAffiliate, "i wireless (T-Mobile Affiliate)", "#.iws@iwspcs.net") },
                 { Carrier.iwirelessSprintAffiliate , new CarrierInfo(Carrier.iwirelessSprintAffiliate, "i-wireless (Sprint Affiliate)", "#@iwirelesshometext.com") },
                 { Carrier.Claro , new CarrierInfo(Carrier.Claro, "Claro", "#@vtexto.com") },
                 { Carrier.Helio , new CarrierInfo(Carrier.Helio, "Helio", "#@myhelio.com") },
                 { Carrier.PocketWireless , new CarrierInfo(Carrier.PocketWireless, "Pocket Wireless", "#@sms.pocket.com") },
                 { Carrier.Voyager , new CarrierInfo(Carrier.Voyager, "Voyager", "#@text.voyagermobile.com") },
                 { Carrier.AirfireMobile , new CarrierInfo(Carrier.AirfireMobile, "Airfire Mobile", "#@sms.airfiremobile.com") },
                 { Carrier.AlaskaCommunications , new CarrierInfo(Carrier.AlaskaCommunications, "Alaska Communications", "#@msg.acsalaska.com") },
                 { Carrier.Ameritech , new CarrierInfo(Carrier.Ameritech, "Ameritech", "#@paging.acswireless.com") },
                 { Carrier.ATTEnterprisePaging , new CarrierInfo(Carrier.ATTEnterprisePaging, "AT&T Enterprise Paging", "#@page.att.net") },
                 { Carrier.BellSouth , new CarrierInfo(Carrier.BellSouth, "BellSouth", "#@bellsouth.cl") },
                 { Carrier.BluegrassCellular , new CarrierInfo(Carrier.BluegrassCellular, "Bluegrass Cellular", "#@sms.bluecell.com") },
                 { Carrier.Cellcom , new CarrierInfo(Carrier.Cellcom, "Cellcom", "#@cellcom.quiktxt.com") },
                 { Carrier.CellularSouth , new CarrierInfo(Carrier.CellularSouth, "Cellular South", "#@csouth1.com") },
                 { Carrier.CharitonValleyWireless , new CarrierInfo(Carrier.CharitonValleyWireless, "Chariton Valley Wireless", "#@sms.cvalley.net") },
                 { Carrier.Cingular , new CarrierInfo(Carrier.Cingular, "Cingular", "#@cingular.com") },
                 { Carrier.Cleartalk , new CarrierInfo(Carrier.Cleartalk, "Cleartalk", "#@sms.cleartalk.us") },
                 { Carrier.Cricket , new CarrierInfo(Carrier.Cricket, "Cricket", "#@sms.mycricket.com") },
                 { Carrier.CSpireWireless , new CarrierInfo(Carrier.CSpireWireless, "C Spire Wireless", "#@cspire1.com") },
                 { Carrier.EdgeWireless , new CarrierInfo(Carrier.EdgeWireless, "Edge Wireless", "#@sms.edgewireless.com") },
                 { Carrier.ElementMobile , new CarrierInfo(Carrier.ElementMobile, "Element Mobile", "#@SMS.elementmobile.net") },
                 { Carrier.Esendex , new CarrierInfo(Carrier.Esendex, "Esendex", "#@echoemail.net") },
                 { Carrier.Kajeet , new CarrierInfo(Carrier.Kajeet, "Kajeet", "#@mobile.kajeet.net") },
                 { Carrier.LongLines , new CarrierInfo(Carrier.LongLines, "LongLines", "#@text.longlines.com") },
                 { Carrier.MetroPCS , new CarrierInfo(Carrier.MetroPCS, "MetroPCS", "#@mymetropcs.com") },
                 { Carrier.Nextech , new CarrierInfo(Carrier.Nextech, "Nextech", "#@sms.nextechwireless.com") },
                 { Carrier.PagePlusCellular , new CarrierInfo(Carrier.PagePlusCellular, "Page Plus Cellular", "#@vtext.com") },
                 { Carrier.QwestWireless , new CarrierInfo(Carrier.QwestWireless, "Qwest Wireless", "#@qwestmp.com") },
                 { Carrier.RedPocketMobile , new CarrierInfo(Carrier.RedPocketMobile, "Red Pocket Mobile", "#@txt.att.net") },
                 { Carrier.SimpleMobile , new CarrierInfo(Carrier.SimpleMobile, "Simple Mobile", "#@smtext.com") },
                 { Carrier.Southernlinc , new CarrierInfo(Carrier.Southernlinc, "Southernlinc", "#@page.southernlinc.com") },
                 { Carrier.SouthCentralCommunications , new CarrierInfo(Carrier.SouthCentralCommunications, "South Central Communications", "#@rinasms.com") },
                 { Carrier.StraightTalkATTSIM , new CarrierInfo(Carrier.StraightTalkATTSIM, "Straight Talk (AT&T SIM)", "#@txt.att.net") },
                 { Carrier.StraightTalkTMobileSIM , new CarrierInfo(Carrier.StraightTalkTMobileSIM, "Straight Talk (T-Mobile SIM)", "#@mmst5.tracfone.com") },
                 { Carrier.StraightTalkVerizonSIM , new CarrierInfo(Carrier.StraightTalkVerizonSIM, "Straight Talk (Verizon SIM)", "#@vtext.com") },
                 { Carrier.SyringaWireless , new CarrierInfo(Carrier.SyringaWireless, "Syringa Wireless", "#@rinasms.com") },
                 { Carrier.Teleflip , new CarrierInfo(Carrier.Teleflip, "Teleflip", "#@teleflip.com") },
                 { Carrier.Unicel , new CarrierInfo(Carrier.Unicel, "Unicel", "#@utext.com") },
                 { Carrier.USAMobility , new CarrierInfo(Carrier.USAMobility, "USA Mobility", "#@usamobility.net") },
                 { Carrier.Viaero , new CarrierInfo(Carrier.Viaero, "Viaero", "#@viaerosms.com") },
                 { Carrier.WestCentralWireless , new CarrierInfo(Carrier.WestCentralWireless, "West Central Wireless", "#@sms.wcc.net") },
                 { Carrier.XITCommunications , new CarrierInfo(Carrier.XITCommunications, "XIT Communications", "#@sms.xit.net") },
                 { Carrier.TracFone , new CarrierInfo(Carrier.TracFone, "TracFone", "#@mmst5.tracfone.com") },
                 { Carrier.CentennialWireless , new CarrierInfo(Carrier.CentennialWireless, "Centennial Wireless", "#@cwemail.com") },
                 { Carrier.PanaceaMobile , new CarrierInfo(Carrier.PanaceaMobile, "Panacea Mobile", "#@api.panaceamobile.com") },
                 { Carrier.RoutoMessaging , new CarrierInfo(Carrier.RoutoMessaging, "RoutoMessaging", "#@email2sms.routomessaging.com") },
                 { Carrier.AppalachianWireless , new CarrierInfo(Carrier.AppalachianWireless, "Appalachian Wireless", "#@awsms.com") }

        };

        /// <summary>
        /// Retrieves a read-only list of all carriers declared
        /// </summary>
        public static IEnumerable<CarrierInfo> Carriers
        {
            get { return _CarrierMapping.Values; }
        }

        /// <summary>
        /// Public getter for the name of the carrier from the enum
        /// </summary>
        /// <param name="carrier"></param>
        /// <returns></returns>
        public static string GetCarrierName(Carrier carrier)
        {
            return _CarrierMapping[carrier].Name;
        }

        /// <summary>
        /// Public getter for the address template from the enum
        /// </summary>
        /// <param name="carrier"></param>
        /// <returns></returns>
        public static string GetCarrierAddress(Carrier carrier)
        {
            return _CarrierMapping[carrier].Template;
        }

        /// <summary>
        /// Maps a carrier and phone number into an e-mail
        /// </summary>
        /// <param name="carrier"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string GetSMSAddress(string phoneNumber, Carrier carrier)
        {
            // Ensure phone is formatted without spaces or hyphens
            string formattedPhoneNumber = phoneRegex.Replace(phoneNumber, "$1$2$3");
            // Retrieve the template for the given carrier
            string carrierAddressTemplate = GetCarrierAddress(carrier);
            // Load up the template and return
            return carrierAddressTemplate.Replace("#", formattedPhoneNumber);
        }

        #endregion
    }
}
